using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VasilievHotel.Class
{
    public static class FilePaths
    {
        private static readonly string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        public static string clientsFilePath => Path.Combine(baseDirectory, @"..\..\DataBase\Clients.txt");
        public static string administratorsFilePath => Path.Combine(baseDirectory, @"..\..\DataBase\Administrator.txt");
        public static string hotelRoomsFilePath => Path.Combine(baseDirectory, @"..\..\DataBase\HotelRooms.txt");
        public static string contractsFilePath => Path.Combine(baseDirectory, @"..\..\DataBase\Contracts.txt");
        public static string clientRoomBindingFilePath => Path.Combine(baseDirectory, @"..\..\DataBase\ClientRoomBinding.txt");
    }
}
