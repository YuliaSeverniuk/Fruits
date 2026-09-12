using System;
using System.IO;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace FruitApp
{
    [XmlInclude(typeof(Citrus))]
    [Serializable]
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(Fruit), "fruit")]
    [JsonDerivedType(typeof(Citrus), "citrus")]
    public class Fruit
    {
        private string name = "Unknown";
        private string color = "Unknown";

        public string Name
        {
            get => name;
            set => name = string.IsNullOrEmpty(value) ? throw new ArgumentException("Name cannot be empty") : value;
        }

        public string Color
        {
            get => color;
            set => color = string.IsNullOrEmpty(value) ? throw new ArgumentException("Color cannot be empty") : value;
        }

        public Fruit()
        {
            name = "Unknown";
            color = "Unknown";
        }

        public Fruit(string name, string color)
        {
            Name = name;
            Color = color;
        }

        public virtual void Input()
        {
            Console.Write("Enter fruit name: ");
            Name = Console.ReadLine() ?? throw new ArgumentException("Name cannot be empty");

            Console.Write("Enter fruit color: ");
            Color = Console.ReadLine() ?? throw new ArgumentException("Color cannot be empty");
        }

        public virtual void Print()
        {
            Console.WriteLine(ToString());
        }

        public virtual void Input(StreamReader reader)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            Name = reader.ReadLine() ?? throw new ArgumentException("Name cannot be empty");
            Color = reader.ReadLine() ?? throw new ArgumentException("Color cannot be empty");
        }

        public virtual void Print(StreamWriter writer)
        {
            if (writer == null) throw new ArgumentNullException(nameof(writer));
            writer.WriteLine($"Fruit: {Name}, Color: {Color}");
        }

        public override string ToString()
        {
            return $"Fruit Name: {Name}, Color: {Color}";
        }
    }
}
