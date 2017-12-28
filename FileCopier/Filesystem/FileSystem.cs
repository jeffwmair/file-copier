using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace FileCopier.Filesystem
    {
    class FileSystem : IFileSystem
        {
        private static ILog LOG = LogManager.GetLogger(typeof(FileSystem));
        private object _lockObject = new object();
        private int _copyCount = 0;
        private bool _isEnabled = true;

        public void SetEnabledStatus(bool enabled)
            {
            _isEnabled = enabled;
            }

        public void CopyFileTo(IFile source, IFile dest)
            {
            if (!_isEnabled)
                {
                return;
                }

            lock (_lockObject)
                {
                try
                    {
                    source.CopyTo(dest);
                    LOG.Debug($"Copied file: {source.GetFileName()}");
                    _copyCount++;
                    }
                catch (Exception ex)
                    {
                    LOG.Error($"Error trying to copy the file '{source.GetFileName()}'", ex);
                    }
                }
            }

        public void CreateDirectory(string name)
            {
            if (!_isEnabled)
                {
                return;
                }

            lock (_lockObject)
                {
                Directory.CreateDirectory(name);
                LOG.Info($"Created directory {name}");
                }
            }

        public bool DirectoryExists(string name)
            {
            return Directory.Exists(name);
            }

        public void EnsureDirectoryExists(string name)
            {
            lock (_lockObject)
                {
                if (!DirectoryExists(name))
                    {
                    CreateDirectory(name);
                    }
                }
            }

        public int GetCopyCount()
            {
            return _copyCount;
            }

        public IFile GetFile(string name)
            {
            return new FileInfoWrapper(name);
            }

        }
    }
