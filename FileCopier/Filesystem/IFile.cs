using System;

namespace FileCopier.Filesystem
    {
    public interface IFile
        {
        void CopyTo(IFile dest);
        string GetFileName();
        string GetFilePathWithName();
        DateTime GetLastUpdateDate();
        bool CanOverwrite(IFile source);
        bool Exists();
        }
    }