using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATM
{
    public class Account
    {
        //the attributes for the account
        private int balance;
        private int pin;
        private int accountNum;

        // Attributes for dealing with race conditions
        private bool dataRace = true;
        private bool semaphore = true;
        Semaphore _gate = new Semaphore(initialCount: 2, maximumCount: 2);
        // a constructor that takes initial values for each of the attributes (balance, pin, accountNumber)
        public Account(int balance, int pin, int accountNum)
        {
            this.balance = balance;
            this.pin = pin;
            this.accountNum = accountNum;
        }

        //getter and setter functions for balance
        public int getBalance()
        {
            return balance;
        }
        public void setBalance(int newBalance)
        {
            this.balance = newBalance;
        }

        /*
         *   This funciton allows us to decrement the balance of an account
         *   it perfomes a simple check to ensure the balance is greater tha
         *   the amount being debeted
         *   
         *   reurns:
         *   true if the transactions if possible
         *   false if there are insufficent funds in the account
         */
        public Boolean decrementBalance(int amount)
        {
            if (this.balance > amount)
            {
                // To demonstrate the data race
                if (dataRace)
                {
                    int oldBalance = this.balance;
                    Thread.Sleep(5000);
                    int newBalance = oldBalance - amount;
                    this.balance = newBalance;
                    return true;
                }
                // Fixed program
                else
                {
                    // If the semaphore is set to false wait until the queue is cleared
                    /*while (!semaphore)
                    {
                        Console.WriteLine("Waiting for semaphore to clear");
                    }*/
                    _gate.WaitOne();
                    //semaphore = false;
                    int oldBalance = this.balance;
                    Thread.Sleep(5000);
                    int newBalance = oldBalance - amount;
                    this.balance = newBalance;
                    //semaphore = true;
                    _gate.Release();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        /*
         * This funciton check the account pin against the argument passed to it
         *
         * returns:
         * true if they match
         * false if they do not
         */
        public Boolean checkPin(int pinEntered)
        {
            if (pinEntered == pin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int getAccountNum()
        {
            return accountNum;
        }

        public void toggleDataRace()
        {
            dataRace = !dataRace;
        }

        public bool getDataRace()
        {
            return dataRace;
        }
    }

}
