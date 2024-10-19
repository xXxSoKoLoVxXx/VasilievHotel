using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using VasilievHotel.Class;

namespace VasilievHotel.Pages
{
    public partial class Authorization : Page
    {
        private List<Clients> clients;
        private List<Administrator> administrators;
        private List<ClientRoomBinding> clientRoomBindings;

        public Authorization()
        {
            InitializeComponent();
            clientRoomBindings = ClientRoomBinding.LoadBindings(FilePaths.clientRoomBindingFilePath); 
            clients = Clients.LoadClients(FilePaths.clientsFilePath, clientRoomBindings); 
            administrators = Administrator.LoadAdministrators(FilePaths.administratorsFilePath); 
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = UsernameTextBox.Text.Trim();
            string password = PasswordBox.Password.Trim();

            var client = clients.FirstOrDefault(c => c.Login == login && c.Password == password);
            if (client != null)
            {
                NavigationService.Navigate(new ClientPage(client));
                return;
            }
            var admin = administrators.FirstOrDefault(a => a.Login == login && a.Password == password);
            if (admin != null)
            {
                NavigationService.Navigate(new AdministratorPage(admin));
                return;
            }
            MessageBox.Show("Неверный логин или пароль. Попробуйте снова.", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
