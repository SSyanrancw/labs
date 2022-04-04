using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace Sopra.Labs.ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ZipInfo();
            Estudiante();
        }

        static void ZipInfo()
        {
            // GET: http://api.zippopotam.us/es/28013

            var http = new HttpClient();
            http.BaseAddress = new Uri("http://api.zippopotam.us/es/");

            Console.Write("Zip: ");
            string zip = Console.ReadLine();

            HttpResponseMessage response = http.GetAsync(zip).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = response.Content.ReadAsStringAsync().Result;

                Console.WriteLine($"Contenido en JSON:");
                Console.WriteLine(content);

                var data = JsonConvert.DeserializeObject<InfoZip>(content);
                Console.WriteLine($"Código postal: {data.postCode}");
                Console.WriteLine($"País: {data.country} ({data.countryAbbreviation})");

                foreach(var i in data.places)
                {
                    Console.WriteLine($"Lugar: {i.placeName}");
                    Console.WriteLine($"Ciudad: {i.state} ({i.stateAbbreviation})");
                    Console.WriteLine($"Posición: {i.longitude}, {i.latitude}");
                    Console.WriteLine();
                }
                
                Console.WriteLine(Environment.NewLine);
            }
            else Console.WriteLine($"Error: {response.StatusCode}");
        }

        static void Estudiante()
        {
            var http = new HttpClient();
            http.BaseAddress = new Uri("http://school.labs.com.es/api/students/");

            Console.Write("Estudiante: ");
            string estudiante = Console.ReadLine();

            HttpResponseMessage response = http.GetAsync(estudiante).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content = response.Content.ReadAsStringAsync().Result;

                Console.WriteLine($"Contenido en JSON:");
                Console.WriteLine(content);

                var data = JsonConvert.DeserializeObject<Student>(content);
                Console.WriteLine($"ID: {data.Id}");
                Console.WriteLine($"Nombre completo: {data.Firstname} {data.Lastname}");
                Console.WriteLine($"Fecha de nacimiento: {data.DateOfBirth}");
                Console.WriteLine($"Clase: ({data.ClassId}) {data.Class}");

                Console.WriteLine(Environment.NewLine);
            }
            else Console.WriteLine($"Error: {response.StatusCode}");
        }
    }

    public class InfoZip
    {
        [JsonProperty("post code")]
        public string postCode { get; set; }

        public string country { get; set; }

        [JsonProperty("country abbreviation")]
        public string countryAbbreviation { get; set; }

        public List<Places> places { get; set; }

        
    }

    public class Places
    {
        [JsonProperty("place name")]
        public string placeName { get; set; }

        public string longitude { get; set; }

        public string latitude { get; set; }

        public string state { get; set; }

        [JsonProperty("state abbreviation")]
        public string stateAbbreviation { get; set; }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int ClassId { get; set; }
        public string Class { get; set; }
    }

}
