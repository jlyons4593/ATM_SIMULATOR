using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ATM
{
    public partial class ATM : Form
    {
        // This is a reference to the account that is being used
        private Account activeAccount = null;
        //semaphore
        
        // Keeps track of which screen the ATM should be displaying
        /*
         0 - Enter account number page
         1 - Enter PIN page
         2 - Option selection page
         3 - Take out cash page
         4 - View balance page
        */
        private int _screen = 0;
        public int screen
        {
            get { return _screen; }
            set
            {
                _screen = value;
                if (_screen == 0)
                {
                    displayAccountNumberPage();
                }
                else if (_screen == 1)
                {
                    displayPINPage();
                }
                else if (_screen == 2)
                {
                    displayOptionsPage();
                }
                else if (_screen == 3)
                {
                    displayTakeOutCashPage();
                }
                else if (_screen == 4)
                {
                    displayViewBalancePage();
                }
            }
        }
        public ATM()
        {
            InitializeComponent();
        }
        public void Form1_Load(object sender, EventArgs e)
        {
            // When the form first loads set it to the account number page
            screen = 0;
        }




        private void button1_Click(object sender, EventArgs e)
        {
            if (screen < 2 || screen == 3)
            {
                ATMTextInput.Text += "1";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (screen < 2 || screen == 3)
            {
                ATMTextInput.Text += "2";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (screen < 2 || screen == 3)
            {
                ATMTextInput.Text += "3";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (screen < 2 || screen == 3)
            {
                ATMTextInput.Text += "4";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (screen < 2 || screen == 3)
            {
                ATMTextInput.Text += "5";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (screen < 2 || screen == 3)
            {
                ATMTextInput.Text += "6";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (screen < 2 || screen == 3)
            {
                ATMTextInput.Text += "7";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (screen < 2 || screen == 3)
            {
                ATMTextInput.Text += "8";
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (screen < 2 || screen == 3)
            {
                ATMTextInput.Text += "9";
            }
        }
        private void buttonCE_Click(object sender, EventArgs e)
        {
            if (screen != 4)
            {
                ATMTextInput.Text = "";
            }
            else
            {
                screen = 2;
            }
        }

        private void button0_Click(object sender, EventArgs e)
        {
            if (screen < 2 || screen == 3)
            {
                ATMTextInput.Text += "0";
            }
        }
        private void buttonEnter_Click(object sender, EventArgs e)
        {
            // If the page is the account number page, lookup the account number to see if it's valid
            // If the page is the PIN page, lookup the PIN number to see if it matches.
            if (screen == 0)
            {
                lookupAccount();
            }
            else if (screen == 1)
            {
                lookupPIN();
            }
        }

        private void L1_Click(object sender, EventArgs e)
        {
            // If the current screen is the options page go to the take out cash screen
            if (screen == 2)
            {
                screen = 3;
            }
            else if (screen == 3)
            {
                withdrawCash(10);

            }
        }

        private void L2_Click(object sender, EventArgs e)
        {
            // If the current screen is the options page go back to the account number page
            if (screen == 2)
            {
                screen = 0;
            }
            else if (screen == 3)
            {
                withdrawCash(20);

            }
        }

        private void L3_Click(object sender, EventArgs e)
        {
            if (screen == 3)
            {
                withdrawCash(40);

            }
        }

        private void R1_Click(object sender, EventArgs e)
        {
            if (screen == 2)
            {
                screen = 4;
            }
            else if (screen == 3)
            {
                withdrawCash(100);

            }
        }
        private void R2_Click(object sender, EventArgs e)
        {
            if (screen == 3)
            {
                withdrawCash(500);

            }
        }


        private void displayAccountNumberPage()
        {

            // Toggles the appropriate elements to be visible to the user for the enter account number page
            ATMPrompt.Visible = true;
            ATMPrompt.Text = "Enter your account number";
            
            ATMTextInput.Visible = true;
            ATMTextInput.Text = "";

            L1Label.Visible = false;
            L2Label.Visible = false;
            L3Label.Visible = false;
            R1Label.Visible = false;
            R2Label.Visible = false;

            ErrorLabel.Visible = false;
        }

        private void lookupAccount()
        {
            // Handles if the text box is empty
            if (ATMTextInput.Text == "")
            {
                return;
            }

            // Converts the user's input to an integer and sees if there is an account with that number
            int targetAccountNumber = Int32.Parse(ATMTextInput.Text);
            Account result = Bankacc.lookupAccountNumber(targetAccountNumber);
            // If there is a result then set the active account to that account and move to the PIN screen
            // Otherwise, display to the user that that is an invlaid account number
            if (result != null)
            {
                activeAccount = result;
                screen = 1;
            }
            else
            {
                ErrorLabel.Visible = true;
                ErrorLabel.Text = "Invalid Account Number";

                ATMTextInput.Text = "";
            }
        }

        private void displayPINPage()
        {

            // Toggles the appropriate elements to be visible to the user for the enter PIN page

            ATMPrompt.Visible = true;
            ATMPrompt.Text = "Enter your PIN";
            ATMTextInput.Visible = true;
            ATMTextInput.Text = "";
           

            L1Label.Visible = false;
            L2Label.Visible = false;
            L3Label.Visible = false;

            R1Label.Visible = false;
            R2Label.Visible = false;


            ErrorLabel.Visible = false;

            RaceConditionButton.Visible = false;
            RaceConditionStatusLabel.Visible = false;

        }

        private void lookupPIN()
        {
            // Handles if the text box is empty
            if (ATMTextInput.Text == "")
            {
                return;
            }

            // Converts the user's input to an integer and sees if it matches the PIN for the active account

            int targetPINNumber = Int32.Parse(ATMTextInput.Text);
            bool result = activeAccount.checkPin(targetPINNumber);

            // If it matches move on to the selection screen
            // Otherwise, display to the user that that is an invalid PIN number

            if (result)
            {
                screen = 2;
            }
            else
            {
                ErrorLabel.Visible = true;
                ErrorLabel.Text = "Invalid PIN Number";
                ATMTextInput.Text = "";

            }
        }

        private void displayOptionsPage()
        {

            // Toggles the appropriate elements to be visible to the user for the options page

            ATMPrompt.Visible = false;
            ATMTextInput.Visible = false;

            L1Label.Visible = true;
            L1Label.Text = "Take out cash";
            L2Label.Visible = true;
            L2Label.Text = "Exit";
            L3Label.Visible = false;
            R1Label.Visible = true;
            R1Label.Text = "View balance";
            R2Label.Visible = false;

            RaceConditionButton.Visible = true;
            RaceConditionStatusLabel.Visible = true;

            ErrorLabel.Visible = false;

        }

        private void displayTakeOutCashPage()
        {
            // Toggles the appropriate elements to be visible to the user for the take out cash page
            ATMPrompt.Visible = true;
            ATMPrompt.Text = "Enter amount to withdraw";
            ATMTextInput.Visible = false;
            ATMTextInput.Text = "";

            L1Label.Visible = true;
            L1Label.Text = "£10";
            L2Label.Visible = true;
            L2Label.Text = "£20";
            L3Label.Visible = true;
            L3Label.Text = "£40";
            R1Label.Visible = true;
            R1Label.Location = new Point(309, 40);
            R1Label.Text = "£100";
            R2Label.Visible = true;
            R2Label.Text = "£500";

            ErrorLabel.Visible = false;
        }

        private void withdrawCash(int targetAmount)
        {
            // Attemps to withdraw the desired amount
            bool result = activeAccount.decrementBalance(targetAmount);

            // If successful go back to the options menu
            // If unsuccessful tell the user via an error message
            if (result)
            {
                MessageBox.Show("You have withdrawn £" + targetAmount + "\nYou now have £" + activeAccount.getBalance(), "Withdrawal Message");
                screen = 2;
            }
            else
            {
                MessageBox.Show("You were unable to withdraw £" + targetAmount + "\nYou still have £" + activeAccount.getBalance(), "Withdrawal Error");
            }
        }

        private void displayViewBalancePage()
        {
            ATMPrompt.Visible = true;
            ATMPrompt.Text = "Your Balance";
            ATMTextInput.Visible = true;
            ATMTextInput.Text = activeAccount.getBalance().ToString();

            L1Label.Visible = false;
            L2Label.Visible = false;
            L3Label.Visible = false;
            R1Label.Visible = false;
            R2Label.Visible = false;

            ErrorLabel.Visible = false;

        }

        private void RaceConditionButton_Click(object sender, EventArgs e)
        {
            activeAccount.toggleDataRace();
            if (activeAccount.getDataRace())
            {
                RaceConditionStatusLabel.Text = "True";
                RaceConditionStatusLabel.ForeColor = Color.Red;
            }
            else
            {
                RaceConditionStatusLabel.Text = "False";
                RaceConditionStatusLabel.ForeColor = Color.Green;
            }

        }

        private void ATMTextInput_TextChanged(object sender, EventArgs e)
        {

        }
    }


}
