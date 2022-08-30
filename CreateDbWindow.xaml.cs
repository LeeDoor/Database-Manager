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

namespace Database_Manager
{
    /// <summary>
    /// Логика взаимодействия для CreateDbWindow.xaml
    /// </summary>
    public partial class CreateDbWindow : Window
    {
        public string ConnectionString { get; set; }

        public CreateDbWindow()
        {
            InitializeComponent();
        }


        public void OnClickGetData(object sender, object a)
        {
            ConnectionString = ConnectionLineText.Text;
            DialogResult = true;
            Close();
        }

    }
}
