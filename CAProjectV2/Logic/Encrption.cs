using System.Text;
using System.Security.Cryptography;

namespace CAProjectV2.Logic
{
    public class Encrption
    {

        public static string Encrypt(string input) {

            byte[] data = Encoding.ASCII.GetBytes(input);
            data = new SHA256Managed().ComputeHash(data);

            return Encoding.ASCII.GetString(data);
        }
    }
}
