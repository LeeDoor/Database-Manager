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
        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }
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
        private ObservableCollection<Order> _orders;
        public ObservableCollection<Order> Orders {
            get => _orders;
            set
            {
                _orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }

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

        public DelegateCommand AddUserClick { get; set; }
        public DelegateCommand EditUserClick { get; set; }
        public DelegateCommand DeleteUserClick { get; set; }
        public DelegateCommand AddOrderClick { get; set; }
        public DelegateCommand DeleteOrderClick { get; set; }

        public void AddUser()
        {
            DialogUserWindow window = new DialogUserWindow(null);
            window.ShowDialog();
            UpdateValues();
        }
        public void EditUser()
        {
            DialogUserWindow window = new DialogUserWindow(SelectedUser.Id);
            window.ShowDialog();
            UpdateValues();
        }
        public void DeleteUser()
        {
            DatabaseManager.RemoveUser(SelectedUser.Id);
            UpdateValues();
        }
        public void AddOrder()
        {
            MessageBox.Show("ADD ORDER BUTTON CLICKED");
        }
        public void DeleteOrder()
        {
            MessageBox.Show("DELETE ORDER BUTTON CLICKED");
        }
        private void UpdateValues()
        {
            Users = DatabaseManager.GetUsers();
            Orders = DatabaseManager.GetOrders();
        }

        public MainWindowViewModel()
        {
            DatabaseManager.ClearDb();
            DatabaseManager.FillDefaultValues();
            UpdateValues();

            AddUserClick = new DelegateCommand(AddUser);
            EditUserClick = new DelegateCommand(EditUser);
            DeleteUserClick = new DelegateCommand(DeleteUser);
            
            AddOrderClick = new DelegateCommand(AddOrder);
            DeleteOrderClick = new DelegateCommand(DeleteOrder);
        }

        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
