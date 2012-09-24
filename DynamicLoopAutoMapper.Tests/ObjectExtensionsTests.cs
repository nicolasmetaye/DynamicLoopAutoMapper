using DynamicLoopAutoMapper.Data.Entities;
using DynamicLoopAutoMapper.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicLoopAutoMapper.Tests
{
    [TestClass]
    public class ObjectExtensionsTests
    {
        [TestMethod]
        public void A_Clone_Should_Have_Identital_Values()
        {
            var author = new Author()
                             {
                                 Id = 12,
                                 FirstName = "Bruce",
                                 LastName = "Wayne"
                             };
            var clone = author.Clone();

            Assert.AreEqual(12, clone.Id);
            Assert.AreEqual("Bruce", clone.FirstName);
            Assert.AreEqual("Wayne", clone.LastName);
        }

        [TestMethod]
        public void The_Original_Should_Be_Modified_Without_Changing_The_Clone()
        {
            var author = new Author()
            {
                Id = 12,
                FirstName = "Bruce",
                LastName = "Wayne"
            };
            var clone = author.Clone();

            author.Id = 13;
            author.FirstName = "Dick";
            author.LastName = "Grayson";

            Assert.AreEqual(12, clone.Id);
            Assert.AreEqual("Bruce", clone.FirstName);
            Assert.AreEqual("Wayne", clone.LastName);
        }
    }
}
