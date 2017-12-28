using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace FileCopier.Filesystem
    {
    class FileInfoWrapper : IFile
        {
        private static ILog LOG = LogManager.GetLogger(typeof(FileInfoWrapper));
        private readonly FileInfo _file;
        public FileInfoWrapper(string filename)
            {
            _file = new FileInfo(filename);
            }

        public bool CanOverwrite(IFile source)
            {
            return !Exists() || GetLastUpdateDate() < source.GetLastUpdateDate();
            }

        public void CopyTo(IFile dest)
            {
            _file.CopyTo(dest.GetFilePathWithName(), overwrite: true);
            LOG.Info($"Copied file:{_file.FullName}");
            }

        public bool Exists()
            {
            return _file.Exists;
            }

        public string GetFileName()
            {
            return _file.Name;
            }

        public string GetFilePathWithName()
            {
            return _file.FullName;
            }

        public DateTime GetLastUpdateDate()
            {
            return _file.LastWriteTimeUtc;
            }
        }
    }
