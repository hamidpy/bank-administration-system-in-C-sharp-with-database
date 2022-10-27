using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BankAdministrationSystem
{
    public partial class BankAdministraationSyatem : Form
    {
        public BankAdministraationSyatem()
        {
            InitializeComponent();
          
            timer1.Start();
        }
   SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HAMID\Documents\BankDB.mdf;Integrated Security=True;Connect Timeout=30");
        // for balance
        int Balance;

     

        // for ever new balance
        private void GetNewBalanceForDeposit()
        {
            con.Open();
            string Query = "select * from CustomerTable where AccountNumber=" + dAccountNumberTextBox.Text + "";
            SqlCommand cmd = new SqlCommand(Query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Balance = Convert.ToInt32(dr["Balance"].ToString());
            }

            con.Close();
        }
        private void GetNewBalanceForWithdrawl()
        {
            con.Open();
            string Query = "select * from CustomerTable where AccountNumber=" + wAccountNumberTextBox.Text + "";
            SqlCommand cmd = new SqlCommand(Query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Balance = Convert.ToInt32(dr["Balance"].ToString());
            }

            con.Close();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime theDate;
            theDate = System.DateTime.UtcNow;

            string theDayOfWeek = theDate.DayOfWeek.ToString();

            theDate = System.DateTime.UtcNow;
            string custom = theDate.ToString("d");

            theDate = System.DateTime.Now;
            string custom2 = theDate.ToString("T");


            dayclockLabel.Text = theDayOfWeek.ToString();
            calclockLabel.Text = custom.ToString();
            timeclockLabel.Text = custom2.ToString();
        }

        private void cnaProfilePicBrowseButton_Click(object sender, EventArgs e)
        {
            string Chosen_File = "";

            profilePicOpen.Title = "Insert an Image";
            profilePicOpen.FileName = "";
            profilePicOpen.Filter = "JPEG Images|*.jpg|All Files|*.*";


            if (profilePicOpen.ShowDialog() != DialogResult.Cancel)
            {

                Chosen_File = profilePicOpen.FileName;
                cnaProfilepictureBox.Image = Image.FromFile(Chosen_File);

            }

        }

        private void cnaSignBrowseButton_Click(object sender, EventArgs e)
        {
            string Chosen_File = "";
            sigPicOpen.Title = "Insert an Image";
            sigPicOpen.FileName = "";
            sigPicOpen.Filter = "JPEG Images|*.jpg|All Files|*.*";
            if (sigPicOpen.ShowDialog() != DialogResult.Cancel)
            {
                Chosen_File = sigPicOpen.FileName;
                cnaSignPictureBox.Image = Image.FromFile(Chosen_File);

            }
        }

        // fo create account
        private void cnaSubmitButton_Click(object sender, EventArgs e)
        {
            if (customerNameTextBox.Text == "" || AccountNumberTextBox.Text == "" || AccountTitleTextBox.Text == "" || customerNameTextBox.Text == "" || AccountTypeComboBox.SelectedItem.ToString() == "" || genderComboBox.Text == "" || dateOfBirthDateTimePicker.Text == "" || nationalityComboBox.SelectedItem.ToString() == "" || postalAddressTextBox.Text == "" || phnNumTextBox.Text == "" || nicNumberTextBox.Text == "" || occupationTextBox.Text == "" || initialDepositTextBox.Text == "")
            {
                MessageBox.Show("Please fill all requiered felds");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into CustomerTable(AccountNumber,AccountTitle,CustomerName,AccountType,DateOfBirth,Nationality,PostalAddress,PhoneNumber,NICNumber,EmailAddress,CompanyName,Occupation,Balance)values(@AccountNumber,@AccountTitle,@CustomerName,@AccountType,@Gender,@DateOfBirth,@Nationality,@PostalAddress,@PhoneNumber,@NICNumber,@EmailAddress,@CompanyName,@Occupation,@Balance)", con);
                    cmd.Parameters.AddWithValue("@AccountNumber", AccountNumberTextBox.Text);
                    cmd.Parameters.AddWithValue("@AccountTitle", AccountTitleTextBox.Text);
                    cmd.Parameters.AddWithValue("@CustomerName", customerNameTextBox.Text);
                    cmd.Parameters.AddWithValue("@AccountType", AccountTypeComboBox.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Gender", genderComboBox.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@DateOfBirth", dateOfBirthDateTimePicker.Text);
                    cmd.Parameters.AddWithValue("@Nationality", nationalityComboBox.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PostalAddress", postalAddressTextBox.Text);
                    cmd.Parameters.AddWithValue("@PhoneNumber", phnNumTextBox.Text);
                    cmd.Parameters.AddWithValue("@NICNumber", nicNumberTextBox.Text);
                    cmd.Parameters.AddWithValue("@EmailAddress", emailAddTextBox.Text);
                    cmd.Parameters.AddWithValue("@CompanyName", companyNameTextBox.Text);
                    cmd.Parameters.AddWithValue("@Occupation", occupationTextBox.Text);
                    cmd.Parameters.AddWithValue("@Balance", initialDepositTextBox.Text);

                    
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Account Created");
                    con.Close();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
            con.Close();
        }
        // create account clear button
        private void cnaClearButton_Click(object sender, EventArgs e)
        {

        }

        // Show all account list
        private void showAccountButton_Click(object sender, EventArgs e)
        {
            con.Open();
            string Query = "select * from CustomerTable";
            SqlDataAdapter sda = new SqlDataAdapter(Query,con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            accountDataGridView.DataSource = ds.Tables[0];

            con.Close();
        }

        // search account for full details
        private void searchAccountSearchButton_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select * from CustomerTable where AccountNumber='" + SearchAccountSearchTextBox.Text+"'",con);
            
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            searchAccountDataGridView.DataSource = ds.Tables[0];
            con.Close();
            
        }
        // search account clear button
        private void searchAccountClearButton_Click(object sender, EventArgs e)
        {

        }
        // search account for deposite
        private void dSearchButton_Click(object sender, EventArgs e)
        {

            if (dAccountNumberTextBox.Text == "")
            {
                MessageBox.Show("Enter Valid Account Number");
            }
            else
            {
                try
                {
                    con.Open();
                    string Query = "select * from CustomerTable where AccountNumber=" + dAccountNumberTextBox.Text + "";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    DataTable dt = new DataTable();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        dCustomerNameTextBox.Text = dr["CustomerName"].ToString();
                        dAccountTitleTextBox2.Text = dr["AccountTitle"].ToString();
                        dCurrentBalanceTextBox.Text = dr["Balance"].ToString();
                        Balance = Convert.ToInt32(dr["Balance"].ToString());
                    }

                    con.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            
            }

            
        }
        // deposit amount
        private void dSubmitButton_Click(object sender, EventArgs e)
        {
            if(dDepositAmmountTextBox.Text == "")
            {
                MessageBox.Show("Enter Deposite Amount");
            }
            else
            {
                GetNewBalanceForDeposit();
                int newBalance = Balance + Convert.ToInt32(dDepositAmmountTextBox.Text);
                
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update CustomerTable set Balance=@AB where AccountNumber=@AN",con);
                    cmd.Parameters.AddWithValue("@AB", newBalance);
                    cmd.Parameters.AddWithValue("@AN", dAccountNumberTextBox.Text);
                    cmd.ExecuteNonQuery();
                    dSucessfullyDepositedLabel.Visible = true;
                   
                    con.Close();

                }catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }


                

            }
            
        }
        // deposit clear button
        private void dClearButton_Click(object sender, EventArgs e)
        {

        }

        // widthdrol search
        private void wSearchButton_Click(object sender, EventArgs e)
        {
            if (wAccountNumberTextBox.Text == "")
            {
                MessageBox.Show("Enter Valid Account Number");
            }
            else
            {
                try
                {
                    con.Open();
                    string Query = "select * from CustomerTable where AccountNumber=" + wAccountNumberTextBox.Text + "";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    DataTable dt = new DataTable();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        wCustomerNameTextBox.Text = dr["CustomerName"].ToString();
                        wAccountTitleTextBox2.Text = dr["AccountTitle"].ToString();
                        wCurrentBalanceTextBox.Text = dr["Balance"].ToString();
                        Balance = Convert.ToInt32(dr["Balance"].ToString());
                    }

                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
        // withdrawl sumbit
        private void wSubmitButton_Click(object sender, EventArgs e)
        {
            if (wWithdrawlAmountTextBox.Text == "")
            {
                MessageBox.Show("Enter withdrawl Amount");
            }
            else
            {
                
                GetNewBalanceForWithdrawl();
                if(Balance < Convert.ToInt32(wWithdrawlAmountTextBox.Text))
                {
                    MessageBox.Show("Insufisiant Balance");
                }
                else
                {
                    int newBalance = Balance - Convert.ToInt32(wWithdrawlAmountTextBox.Text);

                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("update CustomerTable set Balance=@AB where AccountNumber=@AN", con);
                        cmd.Parameters.AddWithValue("@AB", newBalance);
                        cmd.Parameters.AddWithValue("@AN", wAccountNumberTextBox.Text);
                        cmd.ExecuteNonQuery();
                        wSucessfullyWithdrawledLabel.Visible = true;
                        con.Close();

                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }

                    //----------
                  
                }
                
            }
        }
        // withdrawl clear button
        private void wClearButton_Click(object sender, EventArgs e)
        {

        }
        // check balance 
        private void ckSASearchButton_Click(object sender, EventArgs e)
        {
            
                try
                {
                    con.Open();
                    string Query = "select * from CustomerTable where AccountNumber=" + ckSAAccountNumberTextBox.Text + "";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    DataTable dt = new DataTable();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                if(dt.Rows.Count == 0)
                {
                    MessageBox.Show("Account Not Found");
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ckCustomerNameTextBox.Text = dr["CustomerName"].ToString();
                        ckAccountTitleTextBox.Text = dr["AccountTitle"].ToString();
                        ckCurrentBalanceTextBox.Text = dr["Balance"].ToString();
                    }
                }

                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            
        }

        private void ckClearButton_Click(object sender, EventArgs e)
        {

        }

        // Account Details
        private void adSearchAccountSearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string Query = "select * from CustomerTable where AccountNumber=" + adSAAccountNumberTextBox.Text + "";
                SqlCommand cmd = new SqlCommand(Query, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if(dt.Rows.Count == 0)
                {
                    MessageBox.Show("Account Not Found");
                }else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        adCustomerNameLbl.Text = dr["CustomerName"].ToString();
                        adAccountLbl.Text = dr["AccountNumber"].ToString();
                        adAccountTitlelbl.Text = dr["AccountTitle"].ToString();
                        adAccountTypelbl.Text = dr["AccountType"].ToString();
                        adGenderlbl.Text = dr["Gender"].ToString();
                        adDateOfBirthLbl.Text = dr["DateOfBirth"].ToString();
                        adNationalityLbl.Text = dr["Nationality"].ToString();
                        adPostalAddressLbl.Text = dr["PostalAddress"].ToString();
                        adPhoneNumberLbl.Text = dr["PhoneNumber"].ToString();
                        adNICNumberLbl.Text = dr["NICNumber"].ToString();
                        adEmailAddressLbl.Text = dr["EmailAddress"].ToString();
                        adCompanyNameLbl.Text = dr["CompanyName"].ToString();
                        adOccupationLbl.Text = dr["Occupation"].ToString();
                        adCurrentBalanceLbl.Text = dr["Balance"].ToString();
                    }
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // account deatils clear button
        private void adClearButton_Click(object sender, EventArgs e)
        {
            
        }
        // transfer from search
        private void tTransferFormButton_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string Query = "select * from CustomerTable where AccountNumber=" + tFAccountNumberTextBox.Text + "";
                SqlCommand cmd = new SqlCommand(Query, con);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Account Not Found");
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        tFCustomerNameTextBox.Text = dr["CustomerName"].ToString();
                        tFNICNumberTextBox.Text = dr["NICNumber"].ToString();
                        tFCurrentBalanceTextBox.Text = dr["Balance"].ToString();
                    }
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // to
        private void tTransferToButton_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string Query = "select * from CustomerTable where AccountNumber=" + ttAccountNumberTextBox.Text + "";
                SqlCommand cmd = new SqlCommand(Query, con);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Account Not Found");
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ttCustomerNameTextBox.Text = dr["CustomerName"].ToString();
                        ttNICNumberTextBox.Text = dr["NICNumber"].ToString();
                        ttCurrentBalanceTextBox.Text = dr["Balance"].ToString();
                    }
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Subtraction()
        {
            if (Convert.ToInt32(tFCurrentBalanceTextBox.Text) < Convert.ToInt32(tTransferAmmountTextBox.Text))
            {
                MessageBox.Show("Insufisiant Balance");
            }
            else
            {
                int newBalance = Convert.ToInt32(tFCurrentBalanceTextBox.Text) - Convert.ToInt32(tTransferAmmountTextBox.Text);

                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update CustomerTable set Balance=@AB where AccountNumber=@AN", con);
                    cmd.Parameters.AddWithValue("@AB", newBalance);
                    cmd.Parameters.AddWithValue("@AN", tFAccountNumberTextBox.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void Addition()
        {
            int newBalance = Convert.ToInt32(ttCurrentBalanceTextBox.Text) + Convert.ToInt32(tTransferAmmountTextBox.Text);

                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update CustomerTable set Balance=@AB where AccountNumber=@AN", con);
                    cmd.Parameters.AddWithValue("@AB", newBalance);
                    cmd.Parameters.AddWithValue("@AN", ttAccountNumberTextBox.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            
        }

        private void tTransferConfirmButton_Click(object sender, EventArgs e)
        {
            if (tTransferAmmountTextBox.Text == "")
            {
                MessageBox.Show("Missing Amount");
            }
            else if(tFAccountNumberTextBox.Text == ttAccountNumberTextBox.Text)
            {
                MessageBox.Show("Two Account are Same.");
            }
            else
            {
                Subtraction();
                Addition();

                //----------
                try
                {
                    DateTime theDate;
                    theDate = System.DateTime.UtcNow;
                    string todayDate = theDate.ToString("d");

                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into TransactionsTable(AccountNumber,TransactionsType,DepositAmount,WithdrawlAmount,ReceiveAmount,SendAmount,TransactionsDate)values(@AccountNumber,@TransactionsType,@DepositAmount,@WithdrawlAmount,@ReceiveAmount,@SendAmount,@TransactionsDate)", con);
                    cmd.Parameters.AddWithValue("@AccountNumber", tFAccountNumberTextBox.Text);
                    cmd.Parameters.AddWithValue("@TransactionsType", "Transfer");
                    cmd.Parameters.AddWithValue("@DepositAmount", "---");
                    cmd.Parameters.AddWithValue("@WithdrawlAmount", "---");
                    cmd.Parameters.AddWithValue("@ReceiveAmount", "---");
                    cmd.Parameters.AddWithValue("@SendAmount", tTransferAmmountTextBox.Text);
                    cmd.Parameters.AddWithValue("@TransactionsDate", todayDate);
                    cmd.ExecuteNonQuery();
                    SqlCommand cmd2 = new SqlCommand("insert into TransactionsTable(AccountNumber,TransactionsType,DepositAmount,WithdrawlAmount,ReceiveAmount,SendAmount,TransactionsDate)values(@AccountNumber,@TransactionsType,@DepositAmount,@WithdrawlAmount,@ReceiveAmount,@SendAmount,@TransactionsDate)", con);
                    cmd2.Parameters.AddWithValue("@AccountNumber", ttAccountNumberTextBox.Text);
                    cmd2.Parameters.AddWithValue("@TransactionsType", "Transfer");
                    cmd2.Parameters.AddWithValue("@DepositAmount", "---");
                    cmd2.Parameters.AddWithValue("@WithdrawlAmount", "---");
                    cmd2.Parameters.AddWithValue("@ReceiveAmount", tTransferAmmountTextBox.Text);
                    cmd2.Parameters.AddWithValue("@SendAmount", "---");
                    cmd2.Parameters.AddWithValue("@TransactionsDate", todayDate);
                    cmd2.ExecuteNonQuery();

                    con.Close();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                con.Close();



                tTransferAmmountTextBox.Text = "";
                sucessfullyTransferCompleteLabel.Visible = true;

            }
            
        }

        private void ueaSearchAccountbutton_Click(object sender, EventArgs e)
        {
            if(ueaSearchAccounttextBox.Text == "")
            {
                MessageBox.Show("Please put the Account Number");
            }
            else
            {
                try
                {
                    con.Open();
                    string Query = "select * from CustomerTable where AccountNumber=" + ueaSearchAccounttextBox.Text + "";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    DataTable dt = new DataTable();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Account Not Found");
                    }
                    else
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            ueaCustomerNameTextBox.Text = dr["CustomerName"].ToString();
                            ueaAccountTitleTextBox.Text = dr["AccountTitle"].ToString();
                            ueaAccountTypeComboBox.Text = dr["AccountType"].ToString();
                            ueaPostalAddressTextBox.Text = dr["PostalAddress"].ToString();
                            ueaPhoneNumberTextBox.Text = dr["PhoneNumber"].ToString();
                            ueaEmailAddressTextBox.Text = dr["EmailAddress"].ToString();
                            ueaCompanyNameTextBox.Text = dr["CompanyName"].ToString();
                            ueaOccupationTextBox.Text = dr["Occupation"].ToString();
                       }
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ueaSaveChangeButton_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update CustomerTable set CustomerName=@CustomerName,AccountTitle=@AccountTitle, AccountType=@AccountType, PostalAddress=@PostalAddress, PhoneNumber=@PhoneNumber, EmailAddress=@EmailAddress, CompanyName=@CompanyName, Occupation=@Occupation where AccountNumber=@AN", con);
                cmd.Parameters.AddWithValue("@CustomerName", ueaCustomerNameTextBox.Text);
                cmd.Parameters.AddWithValue("@AccountTitle", ueaAccountTitleTextBox.Text);
                cmd.Parameters.AddWithValue("@AccountType", ueaAccountTypeComboBox.Text);
                cmd.Parameters.AddWithValue("@PostalAddress", ueaPostalAddressTextBox.Text);
                cmd.Parameters.AddWithValue("@PhoneNumber", ueaPhoneNumberTextBox.Text);
                cmd.Parameters.AddWithValue("@EmailAddress", ueaEmailAddressTextBox.Text);
                cmd.Parameters.AddWithValue("@CompanyName", ueaCompanyNameTextBox.Text);
                cmd.Parameters.AddWithValue("@Occupation", ueaOccupationTextBox.Text);
                cmd.Parameters.AddWithValue("@AN", ueaSearchAccounttextBox.Text);
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void cbSearchButton_Click(object sender, EventArgs e)
        {
            if(cbAccountNumberTextBox.Text=="")
            {
                con.Open();
                string Query = "select * from TransactionsTable";
                SqlDataAdapter sda = new SqlDataAdapter(Query, con);
                var ds = new DataSet();
                sda.Fill(ds);
                transactionsDataGridView.DataSource = ds.Tables[0];
                con.Close();
            }
            else
            {
                con.Open();
                string Query = "select * from TransactionsTable where AccountNumber="+ cbAccountNumberTextBox.Text+ "";
                SqlDataAdapter sda = new SqlDataAdapter(Query, con);
                var ds = new DataSet();
                sda.Fill(ds);
                transactionsDataGridView.DataSource = ds.Tables[0];
                con.Close();
            }
            con.Close();
        }

        private void NotepadButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Notepad.exe");
        }

        private void calculatorButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Calc");
        }

    }
}
