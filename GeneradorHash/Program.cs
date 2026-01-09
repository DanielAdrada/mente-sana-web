using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GeneradorHash
{
    internal class Program
    {
        static void Main()
        {
            string password = "123456";   // contraseña del psicólogo
            string salt = "XyZ789";       // puedes cambiarla

            using (SHA256 sha = SHA256.Create())
            {
                string hash = BitConverter
                    .ToString(sha.ComputeHash(Encoding.UTF8.GetBytes(password + salt)))
                    .Replace("-", "")
                    .ToLower();

                Console.WriteLine("SALT: " + salt);
                Console.WriteLine("HASH: " + hash);
            }

            Console.ReadKey();
        }
    }
}
