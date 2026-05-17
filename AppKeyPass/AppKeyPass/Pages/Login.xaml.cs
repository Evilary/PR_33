using System.Windows;
using System.Windows.Controls;
using AppKeyPass.Contexts;

namespace AppKeyPass.Pages
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        public async Task Auth(string login, string password)
        {
            string? Token = await UserContext.Login(login, password);
            if (Token == null)
            {
                MessageBox.Show("Логин и пароль указаны не верно");
            }
            else
            {
                MainWindow.Token = Token;
                MainWindow.Init.OpenPages(new Pages.Main());
            }
        }

        private void BthAuth(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbLogin.Text))
            {
                MessageBox.Show("Необходимо указать логин пользователя");
                return;
            }

            if (string.IsNullOrEmpty(tbPassword.Password))
            {
                MessageBox.Show("Необходимо указать пароль пользователя");
                return;
            }

            Auth(tbLogin.Text, tbPassword.Password);
        }
    }
}
