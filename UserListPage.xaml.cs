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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestDb
{
    /// <summary>
    /// Interaction logic for UserListPage.xaml
    /// </summary>
    public partial class UserListPage : Page
    {
        string connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\teach\source\repos\TestDb\DB\Hogworts1.accdb;Persist Security Info=True";

        public UserListPage()
        {
            InitializeComponent();
            LoadPersons();
        }

        private void LoadPersons()
        {
            List<Person> persons = new List<Person>();
            using (OleDbConnection connection = new OleDbConnection(connStr))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("SELECT * FROM Person", connection);
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Person per =
                    new Person
                    {
                        ID = reader.GetInt32(0),
                        FirstName = reader["FirstName"] as string,
                        LastName = reader["LastName"] as string,
                        Address = reader["Address"] as string,
                        PhoneNum = reader["PhoneNum"] as string,
                        Age = (int)(reader["Age"]),
                        BirthDate = Convert.ToDateTime(reader["BirthDate"]),
                        City = reader.GetInt32(7),
                        UserName = reader["UserName"] as string,
                        Password = reader["Password"] as string
                    };
                    /*
                    ID = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Address = reader.GetString(3),
                            PhoneNum = reader.GetString(4),
                            Age = reader.GetInt32(5),
                            BirthDate = reader.GetDateTime(6),
                            City = reader.GetInt32(7),
                            UserName = reader.GetString(8),
                            Password = reader.GetString(9) */
                    persons.Add(per);
                }
            }
            UserGrid.ItemsSource = persons;
        }

        private void AddPerson_Click(object sender, RoutedEventArgs e)
        {
            var newPersonWindow = new AddPersonWindow();
            newPersonWindow.ShowDialog();
            LoadPersons();
        }

        private void DeletePerson_Click(object sender, RoutedEventArgs e)
        {
            if (UserGrid.SelectedItem is Person selectedPerson)
            {
                using (OleDbConnection connection = new OleDbConnection(connStr))
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand($"DELETE FROM Person WHERE ID = {selectedPerson.ID}", connection);
                    int answer = command.ExecuteNonQuery();
                    if (answer > 0)
                        MessageBox.Show("Deleted successfully");
                    else
                        MessageBox.Show("Cannot delete.");
                }
                LoadPersons();
            }
        }

        private void UpdatePerson_Click(object sender, RoutedEventArgs e)
        {
            if (UserGrid.SelectedItem is Person selectedPerson)
            {
                var updateWindow = new UpdatePerson(selectedPerson.UserName);
                updateWindow.ShowDialog();
                LoadPersons();
            }
        }
    }
}
