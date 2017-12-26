using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCopier
    {
    public class DirectoryMapping
        {
        public DirectoryMapping()
            {
            Exclusions = new List<string>();
            }
        public DirectoryMapping(string name, string src, string dest)
            {
            Name = name;
            Src = src;
            Dest = dest;
            }

        public string Name { get; set; }
        public string Src { get; set; }
        public string Dest { get; set; }
        public List<string> Exclusions { get; set; }
        public string FindDestinationDirectoryFromSource(string sourceDirectory)
            {
            return $"{Dest}{sourceDirectory.Replace(Src, "")}";
            }
        public override bool Equals(object obj)
            {
            var other = obj as DirectoryMapping;
            if (other == null)
                {
                return false;
                }

            return Src == other.Src && Dest == other.Dest;
            }
        public override int GetHashCode()
            {
            int hash = 13;
            hash = (hash * 7) + Src.GetHashCode();
            hash = (hash * 7) + Dest.GetHashCode();
            return hash;
            }
        public override string ToString()
            {
            return $"'{Name}':SRC:{Src}, DEST{Dest}";
            }
        }
    }
