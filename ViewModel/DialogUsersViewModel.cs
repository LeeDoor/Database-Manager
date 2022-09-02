using Database_Manager.Model;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Database_Manager.View;

namespace Database_Manager.ViewModel
{
    public class DialogUsersViewModel : INotifyPropertyChanged
    {
        private int? _id;
        private string _name = "";
        private int _phone;
        
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public int Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        public DelegateCommand CreateUserCommand { get; set; }
        public DelegateCommand EditUserCommand { get; set; }
        public DelegateCommand CloseWindowCommand { get; set; }

        public void AddUser()
        {
            if (string.IsNullOrEmpty(Name) || Phone == 0)
            {
                MessageBox.Show("enter valid values");
                return;
            }
            DatabaseManager.AddUser(Name, Phone);
        }
        public void EditUser()
        {
            if (string.IsNullOrEmpty(Name) || Phone == 0)
            {
                MessageBox.Show("enter valid values");
                return;
            }
            if (!_id.HasValue)
                throw new InvalidOperationException("id is not selected");
            DatabaseManager.EditUser(_id.Value, Name, Phone);
        }

        public DialogUsersViewModel(DialogUserWindow win, int? id) 
        {
            _id = id;

            CreateUserCommand = new DelegateCommand(AddUser);
            EditUserCommand = new DelegateCommand(EditUser);
            CloseWindowCommand = new DelegateCommand(() => win.Close());
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
