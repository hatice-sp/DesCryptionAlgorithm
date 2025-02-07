using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics;
namespace des2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Program encryptionClass = new Program();
                string originalText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. \n";
                Console.WriteLine("Şifrelenecek metin: " + originalText);

                Console.WriteLine(originalText);

                
                Stopwatch encryptionStopwatch = Stopwatch.StartNew();
                string encryptedString = encryptionClass.DESencry(originalText);
                encryptionStopwatch.Stop();
                Console.WriteLine("Şifrelenmiş Metin: " + encryptedString);
                Console.WriteLine(" \n Şifreleme Süresi: " + encryptionStopwatch.ElapsedMilliseconds + " milisaniye");

                
                Stopwatch decryptionStopwatch = Stopwatch.StartNew();
                string decryptedString = encryptionClass.DESdecry(encryptedString);
                decryptionStopwatch.Stop();
                Console.WriteLine("Çözülen Metin: " + decryptedString);
                Console.WriteLine("Şifre Çözme Süresi: " + decryptionStopwatch.ElapsedMilliseconds + " milisaniye");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }
            Console.ReadLine();
        }


        public static byte[] ByteDonustur(string value)
        {
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            return ByteConverter.GetBytes(value);
        }

        public static byte[] Byte8(string value)
        {
            char[] arrayChar = value.ToCharArray();
            byte[] arrayByte = new byte[arrayChar.Length];
            for (int i = 0; i < arrayByte.Length; i++)
            {
                arrayByte[i] = Convert.ToByte(arrayChar[i]);
            }
            return arrayByte;
        }

        public string DESencry(string Giris)
        {
            string sonuc = "";
            if (Giris == "" || Giris == null)
            {
                throw new ArgumentNullException("Şifrelenecek veri yok");
            }
            else
            {
                byte[] aryKey = Byte8("12345678"); 
                byte[] aryIV = Byte8("12345678"); 
                using (DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider())
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateEncryptor(aryKey, aryIV), CryptoStreamMode.Write))
                        {
                            using (StreamWriter writer = new StreamWriter(cs))
                            {
                                writer.Write(Giris);
                            }
                        }

                        sonuc = Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            return sonuc;
        }

        public string DESdecry(string strGiris)
        {
            string Sonuc = "";
            if (strGiris == "" || strGiris == null)
            {
                throw new ArgumentNullException("Şifrelenecek veri yok.");
            }
            else
            {
                byte[] aryKey = Byte8("12345678");
                byte[] aryIV = Byte8("12345678");
                using (DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider())
                {
                    using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(strGiris)))
                    {
                        using (CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateDecryptor(aryKey, aryIV), CryptoStreamMode.Read))
                        {
                            using (StreamReader reader = new StreamReader(cs))
                            {
                                Sonuc = reader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            return Sonuc;
        }
    }
}
