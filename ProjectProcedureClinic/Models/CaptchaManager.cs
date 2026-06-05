using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectProcedureClinic.Models
{
    public class CaptchaManager
    {
        public string CaptchaCode()
        {
            char ch1, ch2, ch3, ch4, ch5;
            Random r = new Random();
            ch1 = Convert.ToChar(r.Next(65, 92)); //A-Z
            ch2 = Convert.ToChar(r.Next(97, 122)); //a-z
            ch3 = Convert.ToChar(r.Next(50, 55));  //1-9
            ch4 = Convert.ToChar(r.Next(65, 92));//A-Z
            ch5 = Convert.ToChar(r.Next(97, 122)); //a-z
            string cph = ch1 + "" + ch2 + "" + ch3 + "" + ch4 + "" + ch5;
            return cph;
        }
    }
}