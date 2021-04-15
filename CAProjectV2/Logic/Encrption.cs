using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAProjectV2.Logic
{
    public class Encrption
    {

        public static string Encrypt(string input) {

            byte[] data = System.Text.Encoding.ASCII.GetBytes(input);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            String hash = System.Text.Encoding.ASCII.GetString(data);

            return hash;
        }
    }
}
