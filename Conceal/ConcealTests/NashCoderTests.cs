using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Conceal;

namespace ConcealTests
{
    [TestFixture]
    public class NashCoderTests
    {
        private NashCoder _nashCoder;

        [SetUp]
        public void SetUp()
        {
            _nashCoder = new NashCoder();
        }

        [TestCase(new byte[] {01,02,03,04,05,06,07,08,09,10,11,12,13,14}, 
            new byte[] { 01,2,3,4,3,1,2,3,4,5,6,7,8,9,02,1})]
        public void EncryptingAndDecryptingGivesTheSameContent(byte[] content, byte[] passBytes)
        {
            var encryptedBytes = _nashCoder.Encrypt(content, passBytes);
            var decryptedBytes = _nashCoder.Decrypt(encryptedBytes, passBytes);
            CollectionAssert.AreEqual(content, decryptedBytes);
        }

        [TestCase(//This test cases should n't be changed. Changing this will break the already encrypted data
            new byte[] { 01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14 }, 
            new byte[] { 01, 2, 3, 4, 3, 1, 2, 3, 4, 5, 6, 7, 8, 9, 02, 1 },
            new byte[] { 177, 65, 153, 10, 25, 193, 94, 173, 36, 150, 69, 78, 70, 215, 119, 47 }
            )]
        public void EncryptingContentWithAPasswordGivesEncryptedBytes(byte[] content, byte[] password, byte[] encryptedContent)
        {
            var encryptedBytes = _nashCoder.Encrypt(content, password);
            //foreach (var encryptedByte in encryptedBytes)
            //{
            //    Console.Write(encryptedByte);
            //    Console.Write(",");   
            //}
            CollectionAssert.AreEqual(encryptedContent,encryptedBytes);
        }

        [TestCase(new byte[] {1,2,3,4,5,6,7,8,9})]
        public void EncryptingWithSmallPassBytesGivesException(byte[] passBytes)
        {
            var content = new byte[] {01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14};
            Assert.Throws<Exception>(() => _nashCoder.Encrypt(content, passBytes));
        }

        [TestCase(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 })]
        public void DecryptingWithSmallPassBytesGivesException(byte[] passBytes)
        {
            var content = new byte[] { 01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14 };
            Assert.Throws<Exception>(() => _nashCoder.Decrypt(content, passBytes));
        }
        
    }
}
