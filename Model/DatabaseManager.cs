using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Manager.Model
{
    public static class DatabaseManager
    {
        private static readonly SqlConnection connection;

        static DatabaseManager()
        {
            //var dialog = new CreateDbWindow();
            //dialog.ShowDialog();
            //string connectionStr = dialog.ConnectionString;

            string connectionStr = "Server=localhost\\SQLEXPRESS;Trusted_Connection=True;Encrypt=False;Database=ShopDB;";
            connection = new SqlConnection(connectionStr);
            connection.Open();
        }

        public static void ClearDb()
        {
            SqlCommand command = new SqlCommand($"DELETE FROM Orders", connection);
            command.ExecuteNonQuery();

            command = new SqlCommand($"DELETE FROM Users", connection);
            command.ExecuteNonQuery();
        }

        public static ObservableCollection<User> GetUsers()
        {
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
        public static ObservableCollection<Order> GetOrders()
        {
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
                    orders.Add(new Order() { Id = (int)id, CustomerId = (int)customerId, Summ = (decimal)sum, Date = DateTime.Parse(date.ToString() ?? "2000-12-12") });
                }
                return orders;
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
                sb.Append($"('{defaultNames[random.Next(0, defaultNames.Length - 1)]}', {random.Next(100000, 999999)})");
                if (i < amount - 1) sb.Append(", ");
            }
            sb.Append(";");

            SqlCommand command = new SqlCommand(sb.ToString(), connection);
            return command.ExecuteNonQuery();

        }
        private static int FillDefaultValuesOrders()
        {
            Random random = new Random();
            int amount = 3;
            string[] deafultDates = new string[] { "2022/12/12", "2021/01/13", "2020/05/03", "2000/10/23" };
            StringBuilder sb = new StringBuilder("INSERT INTO Orders VALUES ");
            var Users = GetUsers();
            for (int i = 0; i < amount; i++)
            {
                sb.Append($"('{Users[random.Next(0, Users.Count - 1)].Id}', {random.Next(500, 450000)}, '{deafultDates[random.Next(0, deafultDates.Length)]}')");
                if (i < amount - 1) sb.Append(", ");
            }
            sb.Append(";");

            SqlCommand command = new SqlCommand(sb.ToString(), connection);
            return command.ExecuteNonQuery();

        }

        public static int AddUser(string name, int phone)
        {
            SqlCommand command = new SqlCommand("INSERT INTO Users VALUES (@name, @phone);", connection);

            SqlParameter paramName = new SqlParameter("@name", name);
            command.Parameters.Add(paramName);

            SqlParameter paramPhone = new SqlParameter("@phone", phone);
            command.Parameters.Add(paramPhone);

            return command.ExecuteNonQuery();

        }
        public static int EditUser(int idToEdit, string newname, int newphone)
        {
            SqlCommand command = new SqlCommand("UPDATE Users SET Name = @name, Phone = @phone WHERE Id = @idToChange", connection);

            SqlParameter paramName = new SqlParameter("@name", newname);
            command.Parameters.Add(paramName);

            SqlParameter paramPhone = new SqlParameter("@phone", newphone);
            command.Parameters.Add(paramPhone);

            SqlParameter paramId = new SqlParameter("@idToChange", idToEdit);
            command.Parameters.Add(paramId);

            return command.ExecuteNonQuery();

        }
        public static int RemoveUser(int idToRemove)
        {
            int res = 0;
            SqlCommand command;
            List<Order> ordersToRemove = GetOrders().Where(n => n.CustomerId == idToRemove).ToList();
            if(ordersToRemove.Count > 0)
            {
                StringBuilder sb = new("DELETE FROM Orders WHERE Id IN(");
                for(int i  =0; i < ordersToRemove.Count; i++)
                {
                    sb.Append(ordersToRemove[i].Id);
                    if(i != ordersToRemove.Count - 1)
                        sb.Append(", ");
                }
                sb.Append(")");
                command = new SqlCommand(sb.ToString(), connection);
                res += command.ExecuteNonQuery();
            }

            command = new SqlCommand("DELETE FROM Users WHERE Id = @idToChange", connection);

            SqlParameter paramId = new SqlParameter("@idToChange", idToRemove);
            command.Parameters.Add(paramId);

            return command.ExecuteNonQuery();
        }

        public static int AddOrder(int userId, decimal sum, DateTime date)
        {
            SqlCommand command = new SqlCommand("INSERT INTO Orders VALUES (@userId, @sum, @date);", connection);

            SqlParameter paramUserId = new SqlParameter("@userId", userId);
            command.Parameters.Add(paramUserId);

            SqlParameter paramSum = new SqlParameter("@sum", sum);
            command.Parameters.Add(paramSum);

            SqlParameter paramDate = new SqlParameter("@date", date);
            command.Parameters.Add(paramDate);

            return command.ExecuteNonQuery();
        }
        public static int RemoveOrder(int idToDelete)
        {
            SqlCommand command = new SqlCommand("DELETE FROM Orders WHERE Id = @idToDelete", connection);

            SqlParameter paramId = new SqlParameter("@idToDelete", idToDelete);
            command.Parameters.Add(paramId);

            return command.ExecuteNonQuery();
        }
    }
}
