using Prism.Commands;
using Database_Manager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using Microsoft.Data.SqlClient;

namespace Database_Manager.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        #region grids
        private User _selectedUser;
        public ObservableCollection<User> Users { get; set; }
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }


        private Order _selectedOrder;
        public ObservableCollection<Order> Orders { get; set; }
        public Order SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                OnPropertyChanged(nameof(SelectedOrder));
            }
        }
        #endregion

        private SqlConnection connection;
        public DelegateCommand AddPersonClick { get; set; }
        public DelegateCommand EditPersonClick { get; set; }
        public DelegateCommand DeletePersonClick { get; set; }
        public DelegateCommand AddOrderClick { get; set; }
        public DelegateCommand DeleteOrderClick { get; set; }

        public void AddPerson()
        {
            DatabaseManager.AddUser("ignat", 2224444);
            UpdateUsers();
        }
        public void EditPerson()
        {

        }
        public void DeletePerson()
        {

        }

        public void AddOrder()
        {

        }
        public void DeleteOrder()
        {

        }

        public bool IsPersonSelected() => SelectedUser != null;
        public bool IsOrderSelected() => SelectedOrder != null;

        private void UpdateUsers()
        {
            Users = new ObservableCollection<User>();
            SqlCommand command = new SqlCommand("SELECT * FROM Users", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                object id = reader.GetValue(0);
                object name = reader.GetValue(1);
                object phone = reader.GetValue(2);

                Users.Add(new User() { Id = (int)id, Name = name.ToString(), Phone = (int)phone });
            }
            reader.Close();
        }
        private void UpdateOrders()
        {
            Orders = new ObservableCollection<Order>();
            SqlCommand command = new SqlCommand("SELECT * FROM Orders", connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                object id = reader.GetValue(0);
                object customerId = reader.GetValue(1);
                object summ = reader.GetValue(2);
                object date = reader.GetValue(3);

                Orders.Add(new Order() { Id = (int)id, CustomerId = (int)customerId, Summ = (decimal)summ, Date = date.ToString() });
            }
            reader.Close();
        }

        private void DeleteAllData()
        {
            SqlCommand command = new SqlCommand($"DELETE FROM Orders", connection);
            command.ExecuteNonQuery();

            command = new SqlCommand($"DELETE FROM Users", connection);
            command.ExecuteNonQuery();
        }

        private void FillDefaultValuesUsers()
        {
            string[] names = { "oleg nikolaevich", "sereja sergeevich", "kolya" };
            Random random = new Random();
            foreach (var name in names)
            {
                SqlCommand command = new SqlCommand($"INSERT INTO Users VALUES ('{name}', {random.Next(10000000, 99999999)});", connection);
                int val = command.ExecuteNonQuery();
            }
        }
        private void FillDefaultValuesOrders()
        {
            SqlCommand command = new SqlCommand($"DELETE FROM Orders", connection);
            command.ExecuteNonQuery();

            string[] dates = { "2000-12-12", "2000-12-31", "2001-4-12" };
            Random random = new Random();
            foreach (var date in dates)
            {
                command = new SqlCommand($"INSERT INTO Orders VALUES ({Users[random.Next(0, Users.Count - 1)].Id}, {random.Next(100, 200000)}, '{date}');", connection);
                int val = command.ExecuteNonQuery();
            }
        }

        public MainWindowViewModel()
        {
            //var dialog = new CreateDbWindow();
            //dialog.ShowDialog();
            //string connectionstr = dialog.ConnectionString;
            //

            string connectionstr = "Server=localhost\\SQLEXPRESS;Trusted_Connection=True;Encrypt=False;Database=ShopDB;";// MultipleActiveResultSets=True;";

            connection = new SqlConnection(connectionstr);
            connection.Open();

            DeleteAllData();

            FillDefaultValuesUsers();
            Users = DatabaseManager.GetUsers();
            //UpdateUsers();
            FillDefaultValuesOrders();
            UpdateOrders();

            //сдесь observes can execute не работает по непонятной причине, и кнопка не становится видимой когда появляется выбранный элемент
            AddPersonClick = new DelegateCommand(AddPerson);//.ObservesCanExecute(() => true);
            EditPersonClick = new DelegateCommand(EditPerson);
            DeletePersonClick = new DelegateCommand(DeletePerson);//, IsPersonSelected);//.ObservesCanExecute(IsPersonSelected);

            AddOrderClick = new DelegateCommand(AddOrder);//, () => true);//.ObservesCanExecute(() => true);
            DeleteOrderClick = new DelegateCommand(DeleteOrder);//, IsOrderSelected);//.ObservesCanExecute(IsOrderSelected);
        }

        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
