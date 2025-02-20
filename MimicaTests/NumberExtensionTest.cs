namespace Mimica.Extensions.Tests
{
    [TestClass]
    public class NumberExtensionsTests
    {
        [TestMethod]
        public void ToDateTime_ValidUnixTimestamp_ReturnsCorrectDateTime()
        {
            long timestamp = 1638316800000; // Equivalent to 2021-12-01T00:00:00Z
            DateTime expectedDateTime = new DateTime(2021, 12, 1, 0, 0, 0, DateTimeKind.Utc);

            // Act
            DateTime result = timestamp.ToDateTime();

            // Assert
            Assert.AreEqual(expectedDateTime, result);
        }
    }
}
