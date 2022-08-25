using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace Database_Manager
{
    public class CreateDbWindowViewModel : INotifyPropertyChanged
    {
        private string dbName;
        private string connectionString;

        public string DbName
        {
            get => dbName;
            set
            {
                dbName = value;
                OnPropertyChanged(nameof(DbName));
            }
        }
        public string ConnectionString
        {
            get => connectionString;
            set
            {
                connectionString = value;
                OnPropertyChanged(nameof(ConnectionString));
            }
        }

        public DelegateCommand CreateConnect { get; set; }

        public async void CreateAndConnectAsync()
        {
            if (DbName == null) return;
            using(SqlConnection connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();

                //защита от иньекции не работает
                SqlCommand command = new SqlCommand($"CREATE DATABASE {DbName}"/*injection will be fixed*/ , connection);
                //SqlParameter paramName = new SqlParameter("@name", DbName);
                //command.Parameters.Add(paramName);
                try
                {
                    command.ExecuteNonQuery();

                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        public CreateDbWindowViewModel()
        {
            CreateConnect = new DelegateCommand(CreateAndConnectAsync);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
