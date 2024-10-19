using System;
using System.Collections.Generic;
using System.IO;

namespace VasilievHotel.Class
{
    public class HotelRooms
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public bool Freedom { get; set; }
        public decimal CoastPerDay { get; set; }

        public static List<HotelRooms> LoadHotelRooms(string filePath)
        {
            List<HotelRooms> rooms = new List<HotelRooms>();
            foreach (var line in File.ReadLines(filePath))
            {
                var data = line.Split(',');
                if (data.Length == 4)
                {
                    rooms.Add(new HotelRooms
                    {
                        Id = int.Parse(data[0].Trim()),
                        Number = int.Parse(data[1].Trim()),
                        Freedom = data[2].Trim().ToLower() == "свободно",
                        CoastPerDay = decimal.Parse(data[3].Trim())
                    });
                }
            }
            return rooms;
        }
    }
}
