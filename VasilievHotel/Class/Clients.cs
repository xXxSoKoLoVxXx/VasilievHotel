using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VasilievHotel.Class;

namespace VasilievHotel
{
    public class Clients
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public int? RoomId { get; set; }

        public static List<Clients> LoadClients(string filePath, List<ClientRoomBinding> bindings)
        {
            List<Clients> clients = new List<Clients>();
            foreach (var line in File.ReadLines(filePath))
            {
                var data = line.Split(',');
                if (data.Length == 6)
                {
                    var clientId = int.Parse(data[0].Trim());
                    var roomBinding = bindings.FirstOrDefault(b => b.ClientId == clientId);

                    clients.Add(new Clients
                    {
                        Id = clientId,
                        Login = data[1].Trim(),
                        Password = data[2].Trim(),
                        FirstName = data[3].Trim(),
                        LastName = data[4].Trim(),
                        Patronymic = data[5].Trim(),
                        RoomId = roomBinding?.RoomId
                    });
                }
            }
            return clients;
        }
    }
}
