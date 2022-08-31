﻿using Prism.Commands;
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
        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User> 
        {
            new User() { Id = 0, Name = "Sergay", Phone = 20005 } ,
            new User() { Id = 1, Name = "Oleg", Phone = 20605 } ,
            new User() { Id = 2, Name = "gethethj", Phone = 7585 } 
        };
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
        public ObservableCollection<Order> Orders { get; set; } = new ObservableCollection<Order>
        {
            new Order() { Id = 0, CustomerId = 0, Date = DateTime.Parse("2002/12/01"), Summ = 12000m } ,
            new Order() { Id = 1, CustomerId = 1, Date = DateTime.Parse("2012/11/12"), Summ = 14050m } ,
            new Order() { Id = 2, CustomerId = 1, Date = DateTime.Parse("2003/09/14"), Summ = 15099.99m }
        };
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

        //public bool IsPersonSelected() => SelectedUser != null;
        //public bool IsOrderSelected() => SelectedOrder != null;


        public MainWindowViewModel()
        {

            //DatabaseManager.ClearDb();
            //DatabaseManager.FillDefaultValues();
            //Users = DatabaseManager.GetUsers();
            //Orders = DatabaseManager.GetOrders();

            //сдесь observes can execute не работает по непонятной причине, и кнопка не становится видимой когда появляется выбранный элемент
            //AddPersonClick = new DelegateCommand(AddPerson);//.ObservesCanExecute(() => true);
            //EditPersonClick = new DelegateCommand(EditPerson);
            //DeletePersonClick = new DelegateCommand(DeletePerson);//, IsPersonSelected);//.ObservesCanExecute(IsPersonSelected);
            //
            //AddOrderClick = new DelegateCommand(AddOrder);//, () => true);//.ObservesCanExecute(() => true);
            //DeleteOrderClick = new DelegateCommand(DeleteOrder);//, IsOrderSelected);//.ObservesCanExecute(IsOrderSelected);
        }

        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
