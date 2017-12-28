using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCopier.Filesystem
    {
    public interface IFileSystem
        {
        IFile GetFile(string name);
        bool DirectoryExists(string name);
        void CreateDirectory(string name);
        void EnsureDirectoryExists(string name);
        void CopyFileTo(IFile source, IFile dest);
        int GetCopyCount();
        void SetEnabledStatus(bool enabled);
        }
    }
