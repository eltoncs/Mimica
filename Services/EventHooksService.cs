using Gma.System.MouseKeyHook;
using Mimica.Utils;

namespace Mimica.Services
{
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

        private void KeyPressHook(object? sender, KeyPressEventArgs key)
        {
            if (!KeyboardUtil.IsValidKeyboardCharacter(key.KeyChar))
            {
                return;
            }

            this.processKeyPress!(key.KeyChar.ToString());
        }

        private void KeyUpHook(object? sender, KeyEventArgs key)
        {
            if (!IsControlKey(key))
            {
                return;
            }

            var ctrlPlusKey = KeyboardUtil.GetCtrlPlusKey(key);
            if (ctrlPlusKey != string.Empty)
            {
                this.processKeyUp!($"<{ctrlPlusKey}>");
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

        private void MouseDownHook(object? sender, MouseEventExtArgs e)
        {
            this.processMouseEvents!(e.Button.ToString());
        }

        private bool IsControlKey(KeyEventArgs key)
        {
            var keyCode = key.KeyCode.ToString();
            var keyData = key.KeyData.ToString();

            return (keyCode == keyData && keyData.Length > 1) 
                || KeyboardUtil.GetCtrlPlusKey(key) != string.Empty;
        }

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
