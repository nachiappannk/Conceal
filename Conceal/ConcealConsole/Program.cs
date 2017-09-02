using Conceal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ConcealConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            bool encrypt = true;

            if(args.Length != 7) ShowHelp();

            if (args[0].ToLower() == "encrypt") encrypt = true;
            else if (args[0].ToLower() == "decrypt") encrypt = false;
            else ShowHelp();

            if (args[1] != "-p") ShowHelp();
            var password = args[2];

            if (args[3] != "-i") ShowHelp();
            var inputFile = args[4];

            if (args[5] != "-o") ShowHelp();
            var outputFile = args[6];

            NashCoder nashCoder = new NashCoder();
            var content = File.ReadAllBytes(inputFile);
            var passBytes = Encoding.ASCII.GetBytes(password);
            byte[] output;
            if (encrypt)
            {
                output = nashCoder.Encrypt(content, passBytes);
            }
            else
            {
                output = nashCoder.Decrypt(content, passBytes);
            }
            File.WriteAllBytes(outputFile, output);
        }

        private static void ShowHelp()
        {
            Console.WriteLine("Usage");
            Console.WriteLine("encrypt -p <password> -i <input file? -o <output file>");
            Console.WriteLine("decrypt -p <password> -i <input file? -o <output file>");
            Environment.Exit(0);
        }
    }
}
