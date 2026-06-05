using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace ProjectProcedureClinic.Models
{
    public class Encryption_Decryption
    {
        public string Encryption(string text)
        {
            byte[] b = ASCIIEncoding.ASCII.GetBytes(text);
            string enc = Convert.ToBase64String(b);
            return enc;
        }
        //code for decryption data
        public string Decryption(string text)
        {
            byte[] b = Convert.FromBase64String(text);
            string dec = ASCIIEncoding.ASCII.GetString(b);
            return dec;
        }
    }
}