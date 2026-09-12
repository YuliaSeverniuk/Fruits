using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml.Serialization;

namespace FruitApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                List<Fruit> fruits = new List<Fruit>
                {
                    new Fruit("Apple", "Red"),
                    new Citrus("Lemon", "Yellow", 0.053),
                    new Fruit("Banana", "Yellow"),
                    new Citrus("Orange", "Orange", 0.050),
                    new Citrus("Pomelo", "Yellow", 0.038)
                };

                Console.WriteLine("Yellow Fruits");
                var yellowFruits = fruits.Where(f => f.Color.Equals("Yellow", StringComparison.OrdinalIgnoreCase));
                foreach (var fruit in yellowFruits)
                {
                    fruit.Print();
                }
                Console.WriteLine();

                string sortedFilePath = "sorted_fruits.txt";
                var sortedFruits = fruits.OrderBy(f => f.Name).ToList();

                try
                {
                    using (StreamWriter writer = new StreamWriter(sortedFilePath))
                    {
                        foreach (var fruit in sortedFruits)
                        {
                            fruit.Print(writer);
                        }
                    }
                    Console.WriteLine($"Sorted fruits saved to '{sortedFilePath}'.\n");
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Error writing sorted fruits file: {ex.Message}");
                }

                SerializeAndDeserializeXml(fruits, "fruits.xml");
                SerializeAndDeserializeJson(fruits, "fruits.json");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Argument error: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Format error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void SerializeAndDeserializeXml(List<Fruit> fruits, string filePath)
        {
            Console.WriteLine("Serialized to XML");
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Fruit>));

                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    xmlSerializer.Serialize(fs, fruits);
                }
                Console.WriteLine($"{filePath}");

                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    List<Fruit> deserializedFruits = xmlSerializer.Deserialize(fs) as List<Fruit>
                        ?? throw new InvalidOperationException("Deserialized XML list was null");
                    Console.WriteLine("Deserialized from XML");
                    foreach (var item in deserializedFruits)
                    {
                        Console.WriteLine($"{item}");
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"XML serialization error: {ex.Message}");
            }
            Console.WriteLine();
        }

        private static void SerializeAndDeserializeJson(List<Fruit> fruits, string filePath)
        {
            Console.WriteLine("JSON Serialization");
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };

                string jsonString = JsonSerializer.Serialize(fruits, options);
                File.WriteAllText(filePath, jsonString);
                Console.WriteLine($"{filePath}");

                string readJson = File.ReadAllText(filePath);
                List<Fruit> deserializedFruits = JsonSerializer.Deserialize<List<Fruit>>(readJson, options)
                    ?? throw new JsonException("Deserialized JSON list was null");
                Console.WriteLine("Deserialized from JSON");
                foreach (var item in deserializedFruits)
                {
                    Console.WriteLine($"{item}");
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON serialization error: {ex.Message}");
            }
            Console.WriteLine();
        }
    }
}