using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Manager
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        
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
        public MainWindowViewModel()
        {
            Users = new ObservableCollection<User>()
            {
                new User {Id = 0, Name = "Oleg", Phone = 22555 },
                new User {Id = 1, Name = "Sergay", Phone = 44455 }
            };

            Orders = new ObservableCollection<Order>()
            {
                new Order{ Id = 0, CustomerId = 0, Summ = 150.0m, Date = "2000-12-12" },
                new Order{ Id = 1, CustomerId = 0, Summ = 2000.0m, Date = "2000-12-13" }
            };
        }

        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
