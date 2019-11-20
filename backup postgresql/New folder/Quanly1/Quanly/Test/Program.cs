using System;


namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Please enter a string to encrypt:");
            string plaintext = Console.ReadLine();
            Console.WriteLine("");

            Console.WriteLine("Your encrypted string is:");
            StringCipher sc = new StringCipher();
            string encryptedstring = sc.Encrypt(plaintext);
            Console.WriteLine(encryptedstring);
            Console.WriteLine("");

            Console.WriteLine("Your decrypted string is:");
            string decryptedstring = sc.Decrypt(encryptedstring);
            Console.WriteLine(decryptedstring);
            Console.WriteLine("");

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}
