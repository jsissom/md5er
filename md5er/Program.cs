using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

/*
    Copyright 2014, Jay Sissom
 
    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, version 2 of the License, and only
    version 2.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>. */

namespace md5er
{
    class Program
    {
        static void Main(string[] args)
        {
            if ( args.Length != 1 )
            {
                Console.WriteLine("Must pass path on command line");
                Environment.Exit(1);
            }
            recurseDirectories(args[0]);
            Environment.Exit(0);
        }

        static void recurseDirectories(string path)
        {
            foreach (string f in Directory.GetFiles(path))
            {
                printMd5(f);
            }

            foreach (string d in Directory.GetDirectories(path))
            {
                recurseDirectories(d);
            }
        }

        static void printMd5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    Console.WriteLine("MD5 (" + filename.Replace("\\","/") + ") = " + BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower());
                }
            }
        }
    }
}
