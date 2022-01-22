using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;
namespace Server.BOL
{
    public class CryptographyBOL
    {
        private readonly Random _random = new Random();

        //Create and return 
        public static String getSha256Hash(String str)
        {
            try
            {
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    StringBuilder builder = new StringBuilder();
                    byte[] hashBytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(str));

                    for (int i = 0; i < hashBytes.Length; i++)
                    {
                        builder.Append(hashBytes[i].ToString("x2"));
                    }
                    return builder.ToString();
                }
            }
            catch
            {
                return null;
            }
        }

        public String RandomString(int size, bool lowerCase = true)
        {
            
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):   
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length = 26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

    }
}
