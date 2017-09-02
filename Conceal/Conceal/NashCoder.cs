using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace Conceal
{
    public class NashCoder
    {
        public static int PassBytesMinSize = 10;
        public byte[] Encrypt(byte[] content, byte[] passBytes)
        {
            return Transform(content, passBytes, AesCryptor.Encrypt);
        }

        public byte[] Decrypt(byte[] content, byte[] passBytes)
        {
            return Transform(content, passBytes, AesCryptor.Decrypt);
        }

        private static byte[] Transform(byte[] content, byte[] passBytes, Func<byte[], byte[], byte[], int, byte[]> transformer)
        {
            if (passBytes.Length < PassBytesMinSize)
                throw new Exception("The pass bytes is has to be minimum " + PassBytesMinSize);
            int iteration = passBytes.Length;
            List<byte> mainPassBytes = new List<byte>();
            List<byte> saltBytes = new List<byte>();

            for (int i = 0; i < passBytes.Length; i++)
            {
                if (i % 2 == 0)
                    mainPassBytes.Add(passBytes[i]);
                else
                    saltBytes.Add(passBytes[i]);
            }
            return transformer(content, mainPassBytes.ToArray(), saltBytes.ToArray(), iteration);
        }        
    }
}