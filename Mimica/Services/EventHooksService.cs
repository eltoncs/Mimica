using Gma.System.MouseKeyHook;
using Mimica.Utils;

namespace Mimica.Services
{
    /// <summary>
    /// Provide services to hook global events to capture mouse and keyboard inputs.
    /// </summary>
    public class EventHooksService : IEventHooksService
    {
        private IKeyboardMouseEvents? globalHook;
        private Action<string>? processMouseEvents;
        private Action<string>? processKeyUp;
        private Action<string>? processKeyPress;

        public void Subscribe(
            Action<string> processMouseEvents, 
            Action<string> processKeyUp,
            Action<string> processKeyPress)
        {
            this.processMouseEvents = processMouseEvents;
            this.processKeyUp = processKeyUp;
            this.processKeyPress = processKeyPress;

            globalHook = Hook.GlobalEvents();
            globalHook.MouseDownExt += MouseDownHook;
            globalHook.KeyPress += KeyPressHook;            
            globalHook.KeyUp += KeyUpHook;            
        }

        //Trigges every time a key is pressed, before being released.
        //Handles keyboard characters only.
        private void KeyPressHook(object? sender, KeyPressEventArgs key)
        {
            if (!KeyboardUtil.IsValidKeyboardCharacter(key.KeyChar))
            {
                return;
            }

            this.processKeyPress!(key.KeyChar.ToString());
        }

        //Trigges every time a key is released.
        //Handles special keys only (Enter, Del, F1, etc).
        private void KeyUpHook(object? sender, KeyEventArgs key)
        {
            if (!IsSpecialKey(key))
            {
                return;
            }

            var specialPlusKey = KeyboardUtil.GetSpecialPlusKey(key);
            if (specialPlusKey != string.Empty)
            {
                this.processKeyUp!($"<{specialPlusKey}>");
                return;
            }

            string charFromKey = KeyboardUtil.GetCaracterFromKey(key.KeyCode.ToString());
            if (!string.IsNullOrEmpty(charFromKey))
            {
                return;
            }

            this.processKeyUp!($"<{key.KeyCode.ToString()}>");
            return;
        }

        //Triggers every time a mouse button is pressed.
        private void MouseDownHook(object? sender, MouseEventExtArgs e)
        {
            this.processMouseEvents!(e.Button.ToString());
        }

        //Checks if the key pressed is a special key.
        private bool IsSpecialKey(KeyEventArgs key)
        {
            var keyCode = key.KeyCode.ToString();
            var keyData = key.KeyData.ToString();

            return (keyCode == keyData && keyData.Length > 1) 
                || KeyboardUtil.GetSpecialPlusKey(key) != string.Empty;
        }

        /// <summary>
        /// Unsubscribe from global events.
        /// </summary>
        public void Unsubscribe()
        {
            if (globalHook != null)
            {
                globalHook.MouseDownExt -= MouseDownHook;
                globalHook.KeyUp -= KeyUpHook;
                globalHook.KeyPress -= KeyPressHook;

                globalHook.Dispose();
            }
        }
    }
}
