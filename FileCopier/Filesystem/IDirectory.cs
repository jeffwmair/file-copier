using System.Collections.Generic;

namespace FileCopier.Filesystem
    {
    internal interface IDirectory
        {
        IEnumerable<IDirectory> GetDirectories();
        IEnumerable<IFile> GetFiles();
        string GetFullName();
        bool Exists();
        }
    }