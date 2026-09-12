using System.IO;
using FruitApp;

namespace Fruits.Tests
{
    [TestClass]
    public class CitrusTests
    {
        [TestMethod]
        public void DefaultConstructor_SetsZeroVitaminC()
        {
            var citrus = new Citrus();

            Assert.AreEqual("Unknown", citrus.Name);
            Assert.AreEqual("Unknown", citrus.Color);
            Assert.AreEqual(0.0, citrus.VitaminC);
        }

        [TestMethod]
        public void Constructor_WithValues_SetsProperties()
        {
            var citrus = new Citrus("Lemon", "Yellow", 0.053);

            Assert.AreEqual("Lemon", citrus.Name);
            Assert.AreEqual("Yellow", citrus.Color);
            Assert.AreEqual(0.053, citrus.VitaminC);
        }

        [TestMethod]
        public void Citrus_IsAssignableToFruit()
        {
            Fruit citrus = new Citrus("Lemon", "Yellow", 0.053);

            Assert.IsInstanceOfType<Fruit>(citrus);
        }

        [TestMethod]
        public void VitaminC_SetToNegative_ThrowsArgumentOutOfRangeException()
        {
            var citrus = new Citrus();

            Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => citrus.VitaminC = -1.0);
        }

        [TestMethod]
        public void ToString_ReturnsFormattedStringIncludingBaseFruit()
        {
            var citrus = new Citrus("Lemon", "Yellow", 0.053);

            Assert.AreEqual($"Fruit Name: Lemon, Color: Yellow, Vitamin C: {0.053}g", citrus.ToString());
        }

        [TestMethod]
        public void Input_FromStreamReader_SetsAllProperties()
        {
            var citrus = new Citrus();
            using var reader = new StreamReader(new MemoryStream(System.Text.Encoding.UTF8.GetBytes($"Lemon\nYellow\n{0.053}\n")));

            citrus.Input(reader);

            Assert.AreEqual("Lemon", citrus.Name);
            Assert.AreEqual("Yellow", citrus.Color);
            Assert.AreEqual(0.053, citrus.VitaminC);
        }

        [TestMethod]
        public void Input_FromStreamReader_InvalidVitaminCFormat_ThrowsFormatException()
        {
            var citrus = new Citrus();
            using var reader = new StreamReader(new MemoryStream(System.Text.Encoding.UTF8.GetBytes("Lemon\nYellow\nnot-a-number\n")));

            Assert.ThrowsExactly<FormatException>(() => citrus.Input(reader));
        }

        [TestMethod]
        public void Print_ToStreamWriter_WritesExpectedLine()
        {
            var citrus = new Citrus("Lemon", "Yellow", 0.053);
            using var memoryStream = new MemoryStream();

            using (var writer = new StreamWriter(memoryStream, leaveOpen: true) { AutoFlush = true })
            {
                citrus.Print(writer);
            }

            memoryStream.Position = 0;
            using var reader = new StreamReader(memoryStream);
            Assert.AreEqual($"Citrus: Lemon, Color: Yellow, Vitamin C: {0.053}g", reader.ReadLine());
        }

        [TestMethod]
        public void Print_NullWriter_ThrowsArgumentNullException()
        {
            var citrus = new Citrus("Lemon", "Yellow", 0.053);

            Assert.ThrowsExactly<ArgumentNullException>(() => citrus.Print((StreamWriter)null!));
        }
    }
}
