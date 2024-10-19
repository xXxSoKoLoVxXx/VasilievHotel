using System;
using System.Collections.Generic;
using System.IO;

namespace VasilievHotel
{
    public class Administrator
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }

        public static List<Administrator> LoadAdministrators(string filePath)
        {
            List<Administrator> administrators = new List<Administrator>();
            foreach (var line in File.ReadLines(filePath))
            {
                var data = line.Split(',');
                if (data.Length == 6)
                {
                    administrators.Add(new Administrator
                    {
                        Id = int.Parse(data[0].Trim()),
                        Login = data[1].Trim(),
                        Password = data[2].Trim(),
                        FirstName = data[3].Trim(),
                        LastName = data[4].Trim(),
                        Patronymic = data[5].Trim()
                    });
                }
            }
            return administrators;
        }
    }
}
