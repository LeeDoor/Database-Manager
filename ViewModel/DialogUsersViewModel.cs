using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Manager.ViewModel
{
    public class DialogUsersViewModel : INotifyPropertyChanged
    {
        private string _name;
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

        public void CreateUser()
        {

        }
        public void EditUser()
        {

        }

        public DialogUsersViewModel() 
        {
            CreateUserCommand = new DelegateCommand(CreateUser);
            EditUserCommand = new DelegateCommand(EditUser);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
