using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using log4net;

namespace FileCopier.Filesystem
    {
    internal class DirectoryInfoWrapper : IDirectory
        {
        private static ILog LOG = LogManager.GetLogger(typeof(DirectoryInfoWrapper));
        private readonly DirectoryInfo _directoryInfo;

        public DirectoryInfoWrapper(string dir)
            {
            _directoryInfo = new DirectoryInfo(dir);
            }
        public IEnumerable<IDirectory> GetDirectories()
            {
            return TryGetItems(_directoryInfo, _directoryInfo.EnumerateDirectories)
                .Select(x => new DirectoryInfoWrapper(x.FullName));
            }

        public IEnumerable<IFile> GetFiles()
            {
            return TryGetItems(_directoryInfo, _directoryInfo.EnumerateFiles)
                .Select(x => new FileInfoWrapper(x.FullName));
            }

        /// <summary>
        /// For getting enumerable items from a DirectoryInfo object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="di"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        private static IEnumerable<T> TryGetItems<T>(DirectoryInfo di, Func<IEnumerable<T>> func)
            {
            try
                {
                return func();
                }
            catch (Exception ex)
                {
                LOG.Error($"Cannot enumerate {typeof(T).Name} for directory '{di.FullName}' due to the error:{ex.Message}");
                return Enumerable.Empty<T>();
                }
            }

        public string GetFullName()
            {
            return _directoryInfo.FullName;
            }

        public bool Exists()
            {
            return _directoryInfo.Exists;
            }
        }
    }