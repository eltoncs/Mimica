using Mimica.Extensions;

namespace Mimica.Tests
{
    [TestClass]
    public class DateTimeExtensionTests
    {
        [TestMethod]
        public void ToUnixTimeStamp_ShouldReturnCorrectUnixTimeStamp()
        {
            var dateTime = new DateTime(2023, 10, 1, 0, 0, 0, DateTimeKind.Utc);
            var expectedUnixTimeStamp = 1696118400000;

            var actualUnixTimeStamp = dateTime.ToUnixTimeStamp();

            Assert.AreEqual(expectedUnixTimeStamp, actualUnixTimeStamp);
        }

        [TestMethod]
        public void ToUnixDateStamp_ShouldReturnCorrectUnixDateStamp()
        {
            var dateTime = new DateTime(2023, 10, 1, 0, 0, 0, DateTimeKind.Utc);
            var expectedUnixDateStamp = 19631;

            var actualUnixDateStamp = dateTime.ToUnixDateStamp();

            Assert.AreEqual(expectedUnixDateStamp, actualUnixDateStamp);
        }

    }
}
