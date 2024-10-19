using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using VasilievHotel.Class;
using System.IO;

namespace VasilievHotel.Pages
{
    public partial class AdministratorPage : Page
    {
        private Administrator admin;
        private List<HotelRooms> hotelRooms;
        private List<Clients> clients;
        private List<ClientRoomBinding> clientRoomBindings;
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        public AdministratorPage(Administrator admin)
        {
            InitializeComponent();
            this.admin = admin;
            clientRoomBindings = ClientRoomBinding.LoadBindings(FilePaths.clientRoomBindingFilePath);
            clients = Clients.LoadClients(FilePaths.clientsFilePath, clientRoomBindings);
            hotelRooms = HotelRooms.LoadHotelRooms(FilePaths.hotelRoomsFilePath);

            // Заполняем данные для отображения в таблице, фильтруя только занятые номера.
            var roomData = new List<RoomData>();
            foreach (var room in hotelRooms)
            {
                var binding = clientRoomBindings.FirstOrDefault(b => b.RoomId == room.Id);
                var client = clients.FirstOrDefault(c => c.Id == binding?.ClientId);

                if (client != null) // Добавляем только номера с клиентами.
                {
                    roomData.Add(new RoomData
                    {
                        Id = room.Id,
                        Number = room.Number,
                        Status = room.Freedom ? "Свободно" : "Занято",
                        ClientFullName = $"{client.LastName} {client.FirstName} {client.Patronymic}",
                        CoastPerDay = room.CoastPerDay
                    });
                }
            }

            RoomsDataGrid.ItemsSource = roomData;
        }

        private void GenerateContractButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedRoom = RoomsDataGrid.SelectedItem as RoomData;
            if (selectedRoom == null || selectedRoom.Status == "Свободно")
            {
                MessageBox.Show("Пожалуйста, выберите занятый номер.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            GenerateContract(selectedRoom);
        }

        private void GenerateContract(RoomData selectedRoom)
        {
            try
            {
                string contractText = "Договор на оплату\n\n";
                contractText += "Дата: " + DateTime.Now.ToString("dd.MM.yyyy") + "\n";
                contractText += "Информация по номеру:\n";
                contractText += $"Номер: {selectedRoom.Number}\nКлиент: {selectedRoom.ClientFullName}\nЦена за день: {selectedRoom.CoastPerDay} руб.\n";

                string filePath = System.IO.Path.Combine(baseDirectory, @"..\..\DataBase\Contract.txt");
                File.WriteAllText(filePath, contractText);

                MessageBox.Show($"Договор успешно сохранен по пути: {filePath}", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при генерации договора: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RoomsDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var selectedRoom = RoomsDataGrid.SelectedItem as RoomData;
            if (selectedRoom != null)
            {
                MessageBox.Show($"Номер: {selectedRoom.Number}\nКлиент: {selectedRoom.ClientFullName}\nСтатус: {selectedRoom.Status}\nЦена за день: {selectedRoom.CoastPerDay} руб.", "Информация о номере", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private class RoomData
        {
            public int Id { get; set; }
            public int Number { get; set; }
            public string Status { get; set; }
            public string ClientFullName { get; set; }
            public decimal CoastPerDay { get; set; }
        }
    }
}
