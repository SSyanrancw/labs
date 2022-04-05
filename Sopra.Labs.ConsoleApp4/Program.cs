using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;

namespace Sopra.Labs.ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ZipInfo();
            //GetStudent();
            //PostStudent();
            //UpdateStudent();
            //DeleteStudent();
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

                foreach (var i in data.places)
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

        static void GetStudent()
        {
            var http = new HttpClient();
            http.BaseAddress = new Uri("http://school.labs.com.es/api/students/");

            Console.Write("ID del estudiante a consultar: ");
            HttpResponseMessage response = http.GetAsync(Console.ReadLine()).Result;

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

        static void PostStudent()
        {
            var http = new HttpClient();
            http.BaseAddress = new Uri("http://school.labs.com.es/api/students/");

            var student = new Student()
            {
                Id = 0,
                Firstname = "Miguel",
                Lastname = "a borrar",
                DateOfBirth = new DateTime(1547, 10, 9),
                ClassId = 2
            };

            var content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");

            var response = http.PostAsync("", content).Result;

            if (response.StatusCode == HttpStatusCode.Created)
            {
                var data = JsonConvert.DeserializeObject<Student>(response.Content.ReadAsStringAsync().Result);

                Console.WriteLine($"ID: {data.Id}");
                Console.WriteLine($"Nombre completo: {data.Firstname} {data.Lastname}");
                Console.WriteLine($"Fecha de nacimiento: {data.DateOfBirth}");
                Console.WriteLine($"clase: {data.ClassId} {data.Class}");
            }
            else Console.WriteLine($"Error: {response.StatusCode}");
        }

        static void UpdateStudent()
        {
            var http = new HttpClient();
            http.BaseAddress = new Uri("http://school.labs.com.es/api/students/");

            Console.Write("Id del estudiante a modificar: ");
            var data = http.GetFromJsonAsync<Student>(Console.ReadLine()).Result;

            string temp = "";

            Console.Write("Nombre nuevo: ");
            string firstName = (temp = Console.ReadLine()) == "" ? data.Firstname : temp;
            Console.Write("Apellidos nuevos: ");
            string lastName = (temp = Console.ReadLine()) == "" ? data.Lastname : temp;
            Console.Write("Fecha de nacimiento nuevo: ");
            string dateOfBirth = (temp = Console.ReadLine()) == "" ? data.DateOfBirth.ToString() : temp;
            Console.Write("Id de clase nuevo: ");
            string classID = (temp = Console.ReadLine()) == "" ? data.ClassId.ToString() : temp;

            var student = new Student()
            {
                Id = data.Id,
                Firstname = firstName,
                Lastname = lastName,
                DateOfBirth = DateTime.Parse(dateOfBirth),
                ClassId = Int32.Parse(classID),
                Class = null
            };

            var content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");
            var response = http.PutAsync(data.Id.ToString(), content).Result;

            Console.ReadKey();

            if (response.IsSuccessStatusCode) Console.WriteLine($"Estudiante {data.Id} modificado.");
            else Console.WriteLine($"Error: {response.StatusCode}");
        }

        static void DeleteStudent()
        {
            var http = new HttpClient();
            http.BaseAddress = new Uri("http://school.labs.com.es/api/students/");

            Console.Write("Id del estudiante a borrar: ");
            string id = Console.ReadLine();

            var response = http.DeleteAsync(id).Result;
            if (response.IsSuccessStatusCode) Console.WriteLine($"Estudiante {id} eliminado.");
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
