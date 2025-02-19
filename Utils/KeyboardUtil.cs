namespace Mimica.Utils
{
    public static class KeyboardUtil
    {
        private static Dictionary<string, string> keyMappings = new Dictionary<string,string>()
        {
            { "Oem3", "`" },
            { "D1", "1" },
            { "D2", "2" },
            { "D3", "3" },
            { "D4", "4" },
            { "D5", "5" },
            { "D6", "6" },
            { "D7", "7" },
            { "D8", "8" },
            { "D9", "9" },
            { "D0", "0" },
            { "OemMinus", "-" },
            { "Oemplus", "=" },
            { "Oem4", "[" },
            { "Oem6", "]" },
            { "OemPipe", "\\" },
            { "Oem7", "\'"  },
            { "Oem2", "/" },
            { "OemPeriod", "." },
            { "Oemcomma", "," },
            { "NumPad0", "0" },
            { "NumPad1", "1" },
            { "NumPad2", "2" },
            { "NumPad3", "3" },
            { "NumPad4", "4" },
            { "NumPad5", "5" },
            { "NumPad6", "6" },
            { "NumPad7", "7" },
            { "NumPad8", "8" },
            { "NumPad9", "9" },
            { "Divide", "/" },
            { "Multiply", "*" },
            { "Subtract", "-" },
            { "Add", "+" },
        };

        public static string GetCaracterFromKey(string keyCode)
        {
            if (keyMappings.ContainsKey(keyCode))
            {
                return keyMappings[keyCode];
            }
            return string.Empty;
        }
    }
}
