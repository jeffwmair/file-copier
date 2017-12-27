using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;

namespace FileCopier
    {
    class Copier
        {

        private static ILog LOG = LogManager.GetLogger(typeof(Copier));

        private static object _lockObject = new object();

        public DirectoryMapping Mapping { get; }

        public Copier(DirectoryMapping mapping) => Mapping = mapping;

        private int _copyCount = 0;

        public void StartCopy()
            {
            LOG.Info($"Beginning copy for mapping:{Mapping}");
            var sw = new Stopwatch();
            sw.Start();
            var di = new DirectoryInfo(Mapping.Src);
            if (!di.Exists)
                {
                throw new FileNotFoundException($"Directory '{di.FullName}' does not exist");
                }
            CopyDirectoryRecurive(di);
            LOG.Info($"Total files backed up:{_copyCount} in {sw.ElapsedMilliseconds / 1000.0}s");
            _copyCount = 0;
            }

        private void CopyDirectoryRecurive(DirectoryInfo di)
            {
            if (Mapping.Exclusions.Any(x => di.FullName.EndsWith(x)))
                {
                LOG.Info($"Directory {di.FullName} is excluded, so ignoring");
                return;
                }

            var dest = Mapping.FindDestinationDirectoryFromSource(di.FullName);
            if (!Directory.Exists(dest))
                {
                try
                    {
                    Directory.CreateDirectory(dest);
                    }
                catch (DirectoryNotFoundException ex)
                    {
                    throw new DirectoryNotFoundException($"Unable to find part of the destination directory.  Check that the USB memory stick is plugged in.  Error:{ex.Message}");
                    }
                }
            try
                {
                foreach (var file in di.EnumerateFiles())
                    {
                    // if the file already exists and is newer, don't copy
                    FileInfo destFile = new FileInfo($"{dest}\\{file.Name}");
                    if (destFile.Exists && destFile.LastWriteTimeUtc >= file.LastWriteTimeUtc)
                        {
                        // don't overwrite newer files
                        LOG.Debug($"Not updating file because already backed up: {file.FullName}");
                        continue;
                        }
                    try
                        {
                        if (Mapping.Exclusions.Any(x => file.FullName.EndsWith(x)))
                            {
                            LOG.Info($"Not backing up file because it is part of an exclusion: {file.FullName}");
                            continue;
                            }
                        CopyFileTo(file, destFile);
                        LOG.Debug($"Backed up file: {file.FullName}");
                        _copyCount++;
                        }
                    catch (Exception ex)
                        {
                        LOG.Error($"Error trying to copy the file '{file.FullName}'", ex);
                        }
                    }
                }
            catch (Exception ex)
                {
                LOG.Error(ex);
                }

            try
                {
                di.EnumerateDirectories().ToList().ForEach(x => CopyDirectoryRecurive(x));
                }
            catch (Exception ex)
                {
                LOG.Error(ex);
                }
            }

        public static void CopyFileTo(FileInfo file, FileInfo destFile)
            {
            lock (_lockObject)
                {
                file.CopyTo(destFile.FullName, overwrite: true);
                }
            }

        public static void CancelCopy()
            {
            }
        }
    }
