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
    /// Interaction logic for AddPersonWindow.xaml
    /// </summary>
    public partial class AddPersonWindow : Window
    {
        CityList cityList = new CityList();
        public AddPersonWindow()
        {
                InitializeComponent();
                CityComboBox.ItemsSource = cityList.LoadCities();
                CityComboBox.SelectedIndex = 0;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\teach\\source\\repos\\TestDb\\DB\\Hogworts1.accdb;Persist Security Info=True";
        
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                try
                {
                    conn.Open();
                  
                    string query = "INSERT INTO Person (FirstName, LastName, Address, PhoneNum, Age, birthDate, City, UserName, [Password]) " +
                        "VALUES (@name, @lastName, @address, @phone, @age, @date, @city, @username, @pass)";
                   
                    OleDbCommand cmd = new OleDbCommand(query, conn);
                    
                    cmd.Parameters.Add("@name", OleDbType.VarChar).Value = FirstNameTextBox.Text;
                    cmd.Parameters.Add("@LastName", OleDbType.VarChar).Value = LastNameTextBox.Text;
                    cmd.Parameters.AddWithValue("@address", AddressTextBox.Text);
                    cmd.Parameters.AddWithValue("@phone", PhoneNumTextBox.Text);
                    cmd.Parameters.AddWithValue("@age", int.Parse(AgeTextBox.Text));
                    cmd.Parameters.AddWithValue("@date", BirthDatePicker.SelectedDate.Value.ToString());
                    cmd.Parameters.AddWithValue("@city", CityComboBox.SelectedIndex + 1);
                    cmd.Parameters.AddWithValue("@username", UserNameTextBox.Text);
                    cmd.Parameters.AddWithValue("@pass", PasswordBox.Password);
                    // DEBUG TESTING:
                    /*
                    string message = "";
                    for (int i = 0; i < cmd.Parameters.Count; i++)
                    {
                        message += cmd.Parameters[i].Value.ToString() + "\n";
                    }
                    string commandString = cmd.CommandText;
                    foreach (OleDbParameter parameter in cmd.Parameters)
                        commandString = commandString.Replace("@" + parameter.ParameterName.ToString(), parameter.Value.ToString());
                    
                   // MessageBox.Show(message);
                   // MessageBox.Show(commandString);
                    */

                    int rows = cmd.ExecuteNonQuery();
                    MessageBox.Show("Person added successfully" + rows);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
