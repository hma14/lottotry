using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessTier
{
    public class CaesarCipher
    {
        const int MAX_APHABET = 26;

        public static string caesar_cipher(string txtToCipher, int k)
        {
            char[] buf = new char[txtToCipher.Length];
            if (txtToCipher.Length < 1 || (k < 0))
                return null;

            for (int i = 0; i < txtToCipher.Length; i++)
            {
                char c = txtToCipher[i];
                if (Char.IsDigit(c))
                {
                    buf[i] = c;
                }
                else
                {
                    if (Convert.ToInt16(c) + k > (Char.IsLower(txtToCipher, i) ? Convert.ToInt16('z') : Convert.ToInt16('Z')))
                        buf[i] = Convert.ToChar((Char.IsLower(txtToCipher, i) ? Convert.ToInt16('a') : Convert.ToInt16('A')) + Convert.ToInt16(c) + k - (Char.IsLower(txtToCipher, i) ? Convert.ToInt16('z') : Convert.ToInt16('Z')) - 1);
                    else
                        buf[i] = Convert.ToChar(Convert.ToInt16(txtToCipher[i]) + k);
                }
            }
            return new string(buf);
        }

        //public static void caesar_cipher(ref string txtToCipher, int k)
        //{
        //    char[] lcase = new char[MAX_APHABET];
        //    char[] ucase = new char[MAX_APHABET];

        //    for (int i = 0; i < MAX_APHABET; ++i)
        //    {
        //        lcase[i] = Convert.ToChar(Convert.ToInt16('a') + i);
        //        ucase[i] = Convert.ToChar(Convert.ToInt16('A') + i);
        //    }

        //    for (int i = 0; i < txtToCipher.Length; ++i)
        //    {
        //        if (Char.IsLower(txtToCipher[i]))
        //        {
        //            txtToCipher = txtToCipher.Replace(txtToCipher[i], lcase[(Convert.ToInt16(txtToCipher[i]) - Convert.ToInt16('a') + k) % MAX_APHABET]);
        //        }
        //        else
        //        {
        //            txtToCipher = txtToCipher.Replace(txtToCipher[i], ucase[(Convert.ToChar(Convert.ToInt16(txtToCipher[i]) - Convert.ToInt16('A') + k)) % MAX_APHABET]);
        //        }
        //    }                      
        //}
    }


}
