using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Manager.Model
{
    public class Order : INotifyPropertyChanged
    {
        private int id;
        private int customerId;
        private decimal summ;
        private DateTime date;

        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
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
        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged(nameof(Date));
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
