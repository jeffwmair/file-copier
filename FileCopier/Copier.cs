using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FileCopier.Filesystem;
using log4net;

namespace FileCopier
    {
    public class Copier
        {
        private static ILog LOG = LogManager.GetLogger(typeof(Copier));
        private DirectoryMapping _mapping;
        private IFileSystem _fileSystem;

        public Copier(DirectoryMapping mapping, IFileSystem fileSys)
            {
            _mapping = mapping;
            _fileSystem = fileSys;
            }

        public void StartCopy()
            {
            LOG.Info($"Beginning copy for mapping:{_mapping}");
            var sw = new Stopwatch();
            sw.Start();
            IDirectory di = new DirectoryInfoWrapper(_mapping.Src);
            if (!di.Exists())
                {
                throw new FileNotFoundException($"Directory '{di.GetFullName()}' does not exist");
                }
            CopyDirectoryRecurive(di);
            LOG.Info($"Total files copied {_fileSystem.GetCopyCount()} in {sw.ElapsedMilliseconds / 1000.0}s");
            }

        private void CopyDirectoryRecurive(IDirectory di)
            {
            if (_mapping.DirectoryShouldBeExcludedFromCopy(di.GetFullName()))
                {
                LOG.Info($"Directory {di.GetFullName()} is excluded, so ignoring");
                return;
                }

            var dest = _mapping.FindDestinationDirectoryFromSource(di.GetFullName());
            var destDirectory = new DirectoryInfoWrapper(dest);
            di.GetFiles()
                .ToList()
                .FilterOutFilesThatAreToBeExcluded(_fileSystem, _mapping)
                .FilterOutFilesWithUpToDateCopies(destDirectory, _fileSystem)
                .CopyFilesToDestinations(destDirectory, _fileSystem);

            di.GetDirectories().ToList().ForEach(x => CopyDirectoryRecurive(x));
            }

        public void CancelCopy()
            {
            _fileSystem.SetEnabledStatus(enabled: false);
            }

        }
    }
