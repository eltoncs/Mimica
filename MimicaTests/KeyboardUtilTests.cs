using System.Windows.Forms;

namespace Mimica.Utils.Tests
{
    [TestClass]
    public class KeyboardUtilTests
    {
        [TestMethod]
        public void GetCaracterFromKey_ValidKeyCode_ReturnsCorrectCharacter()
        {
            var keyCode = "D1";
            var expectedCharacter = "1";

            var result = KeyboardUtil.GetCaracterFromKey(keyCode);

            Assert.AreEqual(expectedCharacter, result);
        }

        [TestMethod]
        public void GetCaracterFromKey_InvalidKeyCode_ReturnsEmptyString()
        {
            var keyCode = "InvalidKey";

            var result = KeyboardUtil.GetCaracterFromKey(keyCode);

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void IsValidKeyboardCharacter_ValidCharacter_ReturnsTrue()
        {
            var character = 'A';

            var result = KeyboardUtil.IsValidKeyboardCharacter(character);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValidKeyboardCharacter_InvalidCharacter_ReturnsFalse()
        {
            var character = '\n';

            var result = KeyboardUtil.IsValidKeyboardCharacter(character);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetSpecialPlusKey_ValidKeyCombination_ReturnsCorrectString()
        {
            var keyEventArgs = new KeyEventArgs(Keys.Control | Keys.C);
            var expectedCombination = "Control+C";

            var result = KeyboardUtil.GetSpecialPlusKey(keyEventArgs);

            Assert.AreEqual(expectedCombination, result);
        }

        [TestMethod]
        public void GetSpecialPlusKey_InvalidKeyCombination_ReturnsEmptyString()
        {
            var keyEventArgs = new KeyEventArgs(Keys.A);

            var result = KeyboardUtil.GetSpecialPlusKey(keyEventArgs);

            Assert.AreEqual(string.Empty, result);
        }
    }
}
