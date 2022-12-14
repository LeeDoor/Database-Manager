using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Database_Manager.ViewModel;

namespace Database_Manager
{
    /// <summary>
    /// Логика взаимодействия для CreateUserWindow.xaml
    /// </summary>
    public partial class DialogUserWindow : Window
    {
        public DialogUserWindow(int? id)
        {
            InitializeComponent();
            DataContext = new DialogUsersViewModel(this, id);
        }
    }
}
