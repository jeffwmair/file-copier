using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileCopier.Filesystem;
using log4net;

namespace FileCopier
    {
    static class CopierUtils
        {
        private static ILog LOG = LogManager.GetLogger(typeof(CopierUtils));

        public static IEnumerable<IFile> FilterOutFilesWithUpToDateCopies(this IEnumerable<IFile> sourceFiles,
            IDirectory destDirectory, IFileSystem fileSystem)
            {
            var filesCannotCopy = sourceFiles
                .Where(x => !fileSystem.GetFile($"{destDirectory.GetFullName()}\\{x.GetFileName()}").CanOverwrite(x));
            filesCannotCopy.ToList().ForEach(x => LOG.Debug($"Not updating file because already backed up: {x.GetFileName()}"));
            return sourceFiles.Except(filesCannotCopy);
            }

        public static IEnumerable<IFile> FilterOutFilesThatAreToBeExcluded(this IEnumerable<IFile> sourceFiles,
            IFileSystem fileSystem,
            DirectoryMapping mapping)
            {
            var filesCannotCopy = sourceFiles
                .Where(x => mapping.Exclusions.Any(y => x.GetFileName().EndsWith(y)));
            filesCannotCopy.ToList().ForEach(x => LOG.Info($"Not backing up file because it is part of an exclusion: {x.GetFileName()}"));
            return sourceFiles.Except(filesCannotCopy);
            }

        public static void CopyFilesToDestinations(this IEnumerable<IFile> sourceFiles, IDirectory destDirectory, IFileSystem fileSystem)
            {
            fileSystem.EnsureDirectoryExists(destDirectory.GetFullName());
            foreach (var file in sourceFiles)
                {
                fileSystem.CopyFileTo(file, fileSystem.GetFile($"{destDirectory.GetFullName()}\\{file.GetFileName()}"));
                }
            }
        }
    }
