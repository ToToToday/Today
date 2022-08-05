using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Cookie_Auth.Helpers
{
    public static class Encryption
    {
        public static string SHA256(string plaintext)
        {
            //(一)用內建方法 / linq 轉成byte陣列
            byte[] source = Encoding.Default.GetBytes(plaintext);
            //byte[] source = plaintext.Select(c => (byte)c).ToArray();
            //註： 一byte = 8 bit 是類似 Int8的感覺， 2^8 = 256


            //(二)雜湊加密 (三個類別差異要上網查)
            //byte[] encrypted1 = new SHA256Cng().ComputeHash(source); //新一代 .net Core好像沒有
            //byte[] encrypted2 = new SHA256CryptoServiceProvider().ComputeHash(source); //符合FIPS validates模組? .netframework3.5以上
            byte[] encrypted3 = new SHA256Managed().ComputeHash(source); //比較快，框架版本都支援




            //(三)陣列中的每個byte值，用十六進位兩位數表示 (16*16 = 256)
            string ciphertext = string.Concat(
                encrypted3.Select(
                    x => x.ToString("X2")// X代表十六進位 ， 2代表一次看兩位
                )
            );
            //string ciphertext = encrypted.Aggregate("", (accumulator, next) => accumulator + next.ToString("X2"));
            
            return ciphertext.ToUpper();  //看要不要轉大寫
        }
    }
}
