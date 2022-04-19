using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    public static class Bankacc
    {
        private static Account[] ac = { new Account(300, 1111, 111111), new Account(750, 2222, 222222), new Account(3000, 3333, 333333) };

        public static Account lookupAccountNumber(int accountNumber)
        {
            for (int i = 0; i < ac.Length; i++)
            {
                if (ac[i].getAccountNum() == accountNumber)
                {
                    return ac[i];
                }
            }
            return null;
        }
    }
}
