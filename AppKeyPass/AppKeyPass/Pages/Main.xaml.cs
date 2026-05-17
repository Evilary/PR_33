using System.Windows.Controls;
using AppKeyPass.Context;
using AppKeyPass.Models;

namespace AppKeyPass.Pages
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
            GetStorage();
        }

        public async Task GetStorage()
        {
            List<Storage> Storages = await StorageContext.Get();
            StorageList.Children.Clear();
            foreach (Storage Storage in Storages)
            {
                StorageList.Children.Add(new Elements.Item(Storage, this));
            }
        }

        private void OpenPageAdd(object sender, System.Windows.RoutedEventArgs e) =>
            MainWindow.Init.OpenPages(new Pages.Add());
    }
}
