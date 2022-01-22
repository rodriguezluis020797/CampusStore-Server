using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;
namespace Server.BOL
{
    public class CryptographyBOL
    {
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
    }
}
