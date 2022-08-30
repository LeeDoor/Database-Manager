using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Manager.Model
{
    public static class DatabaseManager
    {
        private static readonly string connectionStr;

        static DatabaseManager()
        {
            //var dialog = new CreateDbWindow();
            //dialog.ShowDialog();
            //string connectionStr = dialog.ConnectionString;

            connectionStr = "Server=localhost\\SQLEXPRESS;Trusted_Connection=True;Encrypt=False;Database=ShopDB;";// MultipleActiveResultSets=True;";

        }

        public static ObservableCollection<User> GetUsers()
        {
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Users", connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    ObservableCollection<User> users = new ObservableCollection<User>();

                    while (reader.Read())
                    {
                        object id = reader.GetValue(0);
                        object name = reader.GetValue(1);
                        object phone = reader.GetValue(2);

                        users.Add(new User() { Id = (int)id, Name = name.ToString() ?? "", Phone = (int)phone });
                    }
                    return users;
                }
            }
        }
        public static ObservableCollection<Order> GetOrders()
        {
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Orders", connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    ObservableCollection<Order> orders = new ObservableCollection<Order>();

                    while (reader.Read())
                    {
                        object id = reader.GetValue(0);
                        object customerId = reader.GetValue(1);
                        object sum = reader.GetValue(2);
                        object date = reader.GetValue(3);

                        orders.Add(new Order() { Id = (int)id, CustomerId = (int)customerId, Summ = (decimal)sum, Date = date.ToString() ?? "" });
                    }
                    return orders;
                }
            }
        }

        public static int FillDefaultValues()
        {
            int res = 0;
            res += FillDefaultValuesUsers();
            res += FillDefaultValuesOrders();
            return res;
        }

        private static int FillDefaultValuesUsers()
        {
            Random random = new Random();
            int amount = 5;
            string[] defaultNames = new string[] { "Alexandr", "Evgeniy", "Oleg", "Nikolay", "Boris", "Ann", "Kate", "Lada", "Ella" };
            StringBuilder sb = new StringBuilder("INSERT INTO Users VALUES ");
            for (int i = 0; i < amount; i++)
            {
                sb.Append($"({defaultNames[random.Next(0, defaultNames.Length - 1)]}, {random.Next(100000, 999999)})");
                if (i < amount - 1) sb.Append(", ");
            }
            sb.Append(";");

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sb.ToString(), connection);
                return command.ExecuteNonQuery();
            }
        }

        private static int FillDefaultValuesOrders()
        {
            // finish this 
        }

        public static int AddUser(string name, int phone)
        {
            using(SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("INSERT INTO Users VALUES (@name, @phone);", connection);

                SqlParameter paramName = new SqlParameter("@name", name);
                command.Parameters.Add(paramName);

                SqlParameter paramPhone = new SqlParameter("@phone", phone);
                command.Parameters.Add(paramPhone);

                return command.ExecuteNonQuery();
            }
        }
    }
}
