using Mimica.Entities;
using Mimica.Extensions;
using System.Collections.Concurrent;

namespace Mimica.Tests.Extensions
{
    [TestClass]
    public class EventExtensionTests
    {
        [TestMethod]
        public void GetCSVLines_ShouldReturnCorrectCSVLines()
        {
            // Arrange
            var eventQueue = new ConcurrentQueue<Event>();
            eventQueue.Enqueue(new Event { TimeStamp = 1, EventType = EventType.MouseClickLeft, KeyPressed = "A", ScreenShotPath = "path1" });
            eventQueue.Enqueue(new Event { TimeStamp = 2, EventType = EventType.KeyboardKeyPressed, KeyPressed = "B", ScreenShotPath = "path2" });

            // Act
            var csvLines = eventQueue.GetCSVLines().ToList();

            // Assert
            Assert.AreEqual(2, csvLines.Count);
            Assert.AreEqual("1,MouseClickLeft,A,path1", csvLines[0]);
            Assert.AreEqual("2,KeyboardKeyPressed,B,path2", csvLines[1]);
        }

        [TestMethod]
        public void GetCSVLines_ShouldSkipEventsWithoutScreenShotPath()
        {
            // Arrange
            var eventQueue = new ConcurrentQueue<Event>();
            eventQueue.Enqueue(new Event { TimeStamp = 1, EventType = EventType.MouseClickLeft, KeyPressed = "A", ScreenShotPath = null });
            eventQueue.Enqueue(new Event { TimeStamp = 2, EventType = EventType.KeyboardKeyPressed, KeyPressed = "B", ScreenShotPath = "path2" });

            // Act
            var csvLines = eventQueue.GetCSVLines().ToList();

            // Assert
            Assert.AreEqual(1, csvLines.Count);
            Assert.AreEqual("2,KeyboardKeyPressed,B,path2", csvLines[0]);
        }
    }
}
