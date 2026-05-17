using AppKeyPass.Context;
using AppKeyPass.Models;
using System.Windows;
using System.Windows.Controls;

namespace AppKeyPass.Pages
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        Storage ChangeStorage;
        public Add(Storage storage = null)
        {
            InitializeComponent();

            ChangeStorage = storage;
            if (ChangeStorage != null)
            {
                tbName.Text = ChangeStorage.Name;
                tbUrl.Text = ChangeStorage.Url;
                tbLogin.Text = ChangeStorage.Login;
                tbPassword.Text = ChangeStorage.Password;
            }
        }

        private async void Save(object sender, RoutedEventArgs e)
        {
            if (ChangeStorage == null)
            {
                Storage storage = new Storage()
                {
                    Name = tbName.Text,
                    Url = tbUrl.Text,
                    Login = tbLogin.Text,
                    Password = tbPassword.Text,
                };
                await StorageContext.Add(storage);
            }
            else
            {
                ChangeStorage.Name = tbName.Text;
                ChangeStorage.Url = tbUrl.Text;
                ChangeStorage.Login = tbLogin.Text;
                ChangeStorage.Password = tbPassword.Text;

                await StorageContext.Update(ChangeStorage);
            }

            MessageBox.Show("Данные сохранены");
            MainWindow.Init.OpenPages(new Pages.Main());
        }

        private void Back(object sender, RoutedEventArgs e) =>
            MainWindow.Init.OpenPages(new Pages.Main());
    }
}
