using System;
using System.IO;

namespace FruitApp
{
    [Serializable]
    public class Citrus : Fruit
    {
        private double vitaminC;

        public double VitaminC
        {
            get => vitaminC;
            set => vitaminC = value < 0 ? throw new ArgumentOutOfRangeException(nameof(value), "Value cannot be negative") : value;
        }

        public Citrus() : base()
        {
            vitaminC = 0.0;
        }

        public Citrus(string name, string color, double vitaminC) : base(name, color)
        {
            VitaminC = vitaminC;
        }

        public override void Input()
        {
            base.Input();
            Console.Write("Enter vitamin C (content in grams): ");
            if (double.TryParse(Console.ReadLine(), out double result))
            {
                VitaminC = result;
            }
            else
            {
                throw new FormatException("Invalid number format");
            }
        }

        public override void Print()
        {
            Console.WriteLine(ToString());
        }

        public override void Input(StreamReader reader)
        {
            base.Input(reader);
            if (double.TryParse(reader.ReadLine(), out double result))
            {
                VitaminC = result;
            }
            else
            {
                throw new FormatException("Invalid number format");
            }
        }

        public override void Print(StreamWriter writer)
        {
            if (writer == null) throw new ArgumentNullException(nameof(writer));
            writer.WriteLine($"Citrus: {Name}, Color: {Color}, Vitamin C: {VitaminC}g");
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Vitamin C: {VitaminC}g";
        }
    }
}
