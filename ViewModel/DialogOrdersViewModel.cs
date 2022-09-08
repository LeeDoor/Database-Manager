using Database_Manager.Model;
using Database_Manager.View;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Database_Manager.ViewModel
{
    public class DialogOrdersViewModel : INotifyPropertyChanged
    {
        private DialogOrderWindow _win;
        private int customerId;
        private decimal summ;
        private string date;

        public List<int> IdList { get; set; }

        public int CustomerId
        {
            get => customerId;
            set
            {
                customerId = value;
                OnPropertyChanged(nameof(CustomerId));
            }
        }
        public decimal Summ
        {
            get => summ;
            set
            {
                summ = value;
                OnPropertyChanged(nameof(Summ));
            }
        }
        public string Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        public DelegateCommand CloseWindowCommand { get; set; }
        public DelegateCommand CreateOrEditCommand { get; set; }


        public void CreateOrEditOrder()
        {
            if (Summ == 0 || !DateTime.TryParse(Date + " 10:00", out DateTime resdate))
            {
                MessageBox.Show($"enter valid data: summ = {Summ}");
                return;
            }
            DatabaseManager.AddOrder(CustomerId, Summ, resdate);

            _win.Close();
        }

        public DialogOrdersViewModel(DialogOrderWindow win)
        {
            _win = win;

            CreateOrEditCommand = new DelegateCommand(CreateOrEditOrder);
            CloseWindowCommand = new DelegateCommand(() => win.Close());

            IdList = DatabaseManager.GetUsers().Select(n => n.Id).ToList();

            CustomerId = IdList.First();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
