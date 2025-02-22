using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestDb.Model;

namespace TestDb
{
    /// <summary>
    /// Interaction logic for UpdatePerson.xaml
    /// </summary>
    public partial class UpdatePerson : Window
    {
        int userId = 0;
        int city = 0;
        CityList cityList = new CityList();
        public UpdatePerson(string username)
        {
            InitializeComponent();
            CityComboBox.ItemsSource = cityList.LoadCities();

            findPerson(username);
        }
        // פעולה לחיפוש משתמש לפי שם משתמש
        //private void SearchButton_Click(object sender, RoutedEventArgs e)
        private void findPerson(string username)//Person per)
        {
            string connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\teach\\source\\repos\\TestDb\\DB\\Hogworts1.accdb;Persist Security Info=True";
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Person WHERE UserName = ?";
                    OleDbCommand cmd = new OleDbCommand(query, conn);
                    cmd.Parameters.AddWithValue("?", username);

                    OleDbDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // מילוי השדות עם נתוני המשתמש
                        FirstNameTextBox.Text = reader["FirstName"].ToString();
                        LastNameTextBox.Text = reader["LastName"].ToString();
                        AddressTextBox.Text = reader["Address"].ToString();
                        PhoneNumTextBox.Text = reader["PhoneNum"].ToString();
                        AgeTextBox.Text = reader["Age"].ToString();
                        BirthDatePicker.SelectedDate = (DateTime)reader["birthDate"];
                        //CityTextBox.Text = reader["City"].ToString();
                        city = (int)reader["City"];
                        this.CityComboBox.SelectedIndex = city - 1;
                        UserNameTextBox.Text = reader["UserName"].ToString();
                       // PasswordTextBox.Text = reader["Password"].ToString();
                       userId = int.Parse(reader["ID"].ToString());
                    }
                    else
                    {
                        MessageBox.Show("User not found.");
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            MessageBox.Show("Got user id: " + userId);
        }

        // פעולה לעדכון המשתמש
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            string connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\teach\\source\\repos\\TestDb\\DB\\Hogworts1.accdb;Persist Security Info=True";
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Person SET FirstName = ?, LastName = ?, Address = ?, PhoneNum = ?, Age = ?, birthDate = ?, City = ?, UserName = ?  WHERE ID = " + userId;  //, Password = ?
                    OleDbCommand cmd = new OleDbCommand(query, conn);
                    cmd.Parameters.AddWithValue("?", FirstNameTextBox.Text);
                    cmd.Parameters.AddWithValue("?", LastNameTextBox.Text);
                    cmd.Parameters.AddWithValue("?", AddressTextBox.Text);
                    cmd.Parameters.AddWithValue("?", PhoneNumTextBox.Text);
                    cmd.Parameters.AddWithValue("?", AgeTextBox.Text);
                    cmd.Parameters.AddWithValue("?", BirthDatePicker.SelectedDate.Value.ToString());
                    cmd.Parameters.AddWithValue("?", CityComboBox.SelectedIndex + 1);
                 //   cmd.Parameters.AddWithValue("?", PasswordTextBox.Text);
                    cmd.Parameters.AddWithValue("?", UserNameTextBox.Text);

                    // DEBUG TESTING:
                    
                    string message = "";
                    for (int i = 0; i < cmd.Parameters.Count; i++)
                    {
                        message += cmd.Parameters[i].Value.ToString() + "\n";
                    }
                    string commandString = cmd.CommandText;
                    foreach (OleDbParameter parameter in cmd.Parameters)
                        commandString = commandString.Replace("@" + parameter.ParameterName.ToString(), parameter.Value.ToString());
                    
                    MessageBox.Show(message);
                    MessageBox.Show(commandString);
                    


                    int rows = cmd.ExecuteNonQuery();
                    MessageBox.Show("Person updated successfully.");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
