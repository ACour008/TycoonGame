using System;
using NUnit.Framework;

namespace Tests
{
    public class structure_factory_tests
    {
        [Test]
        public void structure_factory_returns_Attraction_object()
        {
            // Arrange
            StructureProducer producer = new StructureProducer();

            // Act
            Structure attraction = producer.GetStructure("Attraction");

            // Assert
            Assert.AreEqual(typeof(Attraction), attraction.GetType());
        }

        [Test]
        public void structure_factory_returns_null_when_structure_doesnt_exist()
        {
            // Arrange
            StructureProducer producer = new StructureProducer();

            // Act
            Structure attraction = producer.GetStructure("Random");

            // Assert
            Assert.AreEqual(null, attraction);
        }
    }

}
