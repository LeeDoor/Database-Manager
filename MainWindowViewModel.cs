using Prism.Commands;
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

        public DelegateCommand AddPersonClick { get; set; }
        public DelegateCommand EditPersonClick { get; set; }
        public DelegateCommand DeletePersonClick { get; set; }
        public DelegateCommand AddOrderClick { get; set; }
        public DelegateCommand DeleteOrderClick { get; set; }

        public void AddPerson()
        {

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
            //сдесь observes can execute не работает по непонятной причине, и кнопка не становится видимой когда появляется выбранный элемент
            AddPersonClick = new DelegateCommand(AddPerson);//, () => true);//.ObservesCanExecute(() => true);
            EditPersonClick = new DelegateCommand(EditPerson);//, IsPersonSelected);//.ObservesCanExecute(IsPersonSelected);
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
