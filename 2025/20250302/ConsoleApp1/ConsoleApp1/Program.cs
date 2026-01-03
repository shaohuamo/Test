using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace ConsoleApp1
{

    internal class Program
    {
        static void Main()
        {
            OriginalType original = new OriginalType { Name = "Alice", Age = 30 };

            // Serialize OriginalType as SerializedType
            string json = JsonSerializer.Serialize(original);
            Console.WriteLine("Serialized JSON: " + json);

            // Deserialize as a different object
            SerializedType deserialized = JsonConvert.DeserializeObject<SerializedType>(json);
            Console.WriteLine($"Deserialized as SerializedType: {deserialized.FullName}, {deserialized.YearsOld}");
        }
    }

    public class OriginalType
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class SerializedType
    {
        public string FullName { get; set; }
        public int YearsOld { get; set; }
    }
}
