using System;
using FileCopier;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileCopierTest
    {
    [TestClass]
    public class DirectoryMappingTest
        {
        [TestMethod]
        public void FindDestinationDir()
            {
            var mapping = new DirectoryMapping("test", "C:\\Foo", "E:\\FooBackup");
            var result = mapping.FindDestinationDirectoryFromSource("C:\\Foo\\a\\b");
            Assert.AreEqual("E:\\FooBackup\\a\\b", result);
            }
        }
    }
