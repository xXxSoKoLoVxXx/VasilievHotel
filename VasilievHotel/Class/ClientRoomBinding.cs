using System;
using System.Collections.Generic;
using System.IO;

namespace VasilievHotel.Class
{
    public class ClientRoomBinding
    {
        public int ClientId { get; set; }
        public int RoomId { get; set; }
        public DateTime EndOfTerm { get; set; }

        public static List<ClientRoomBinding> LoadBindings(string filePath)
        {
            List<ClientRoomBinding> bindings = new List<ClientRoomBinding>();
            foreach (var line in File.ReadLines(filePath))
            {
                var data = line.Split(',');
                if (data.Length == 2)
                {
                    bindings.Add(new ClientRoomBinding
                    {
                        ClientId = int.Parse(data[0].Trim()),
                        RoomId = int.Parse(data[1].Trim()),
                        EndOfTerm = DateTime.Parse(data[2].Trim())
                    });
                }
            }
            return bindings;
        }
    }
}
