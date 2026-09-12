using System.IO;
using FruitApp;

namespace Fruits.Tests
{
    [TestClass]
    public class FruitTests
    {
        [TestMethod]
        public void DefaultConstructor_SetsUnknownValues()
        {
            var fruit = new Fruit();

            Assert.AreEqual("Unknown", fruit.Name);
            Assert.AreEqual("Unknown", fruit.Color);
        }

        [TestMethod]
        public void Constructor_WithValues_SetsProperties()
        {
            var fruit = new Fruit("Apple", "Red");

            Assert.AreEqual("Apple", fruit.Name);
            Assert.AreEqual("Red", fruit.Color);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        public void Name_SetToNullOrEmpty_Throws(string? invalidName)
        {
            var fruit = new Fruit();

            Assert.ThrowsExactly<ArgumentException>(() => fruit.Name = invalidName!);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        public void Color_SetToNullOrEmpty_Throws(string? invalidColor)
        {
            var fruit = new Fruit();

            Assert.ThrowsExactly<ArgumentException>(() => fruit.Color = invalidColor!);
        }

        [TestMethod]
        public void ToString_ReturnsFormattedString()
        {
            var fruit = new Fruit("Apple", "Red");

            Assert.AreEqual("Fruit Name: Apple, Color: Red", fruit.ToString());
        }

        [TestMethod]
        public void Input_FromStreamReader_SetsNameAndColor()
        {
            var fruit = new Fruit();
            using var reader = new StreamReader(new MemoryStream(System.Text.Encoding.UTF8.GetBytes("Apple\nRed\n")));

            fruit.Input(reader);

            Assert.AreEqual("Apple", fruit.Name);
            Assert.AreEqual("Red", fruit.Color);
        }

        [TestMethod]
        public void Input_FromStreamReader_MissingLines_ThrowsArgumentException()
        {
            var fruit = new Fruit();
            using var reader = new StreamReader(new MemoryStream());

            Assert.ThrowsExactly<ArgumentException>(() => fruit.Input(reader));
        }

        [TestMethod]
        public void Input_NullReader_ThrowsArgumentNullException()
        {
            var fruit = new Fruit();

            Assert.ThrowsExactly<ArgumentNullException>(() => fruit.Input((StreamReader)null!));
        }

        [TestMethod]
        public void Print_ToStreamWriter_WritesExpectedLine()
        {
            var fruit = new Fruit("Apple", "Red");
            using var memoryStream = new MemoryStream();

            using (var writer = new StreamWriter(memoryStream, leaveOpen: true) { AutoFlush = true })
            {
                fruit.Print(writer);
            }

            memoryStream.Position = 0;
            using var reader = new StreamReader(memoryStream);
            Assert.AreEqual("Fruit: Apple, Color: Red", reader.ReadLine());
        }

        [TestMethod]
        public void Print_NullWriter_ThrowsArgumentNullException()
        {
            var fruit = new Fruit("Apple", "Red");

            Assert.ThrowsExactly<ArgumentNullException>(() => fruit.Print((StreamWriter)null!));
        }
    }
}
