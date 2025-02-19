using Gma.System.MouseKeyHook;
using Mimica.Utils;

namespace Mimica.Services
{
    public class EventHooksService : IEventHooksService
    {
        private IKeyboardMouseEvents? globalHook;
        private Action<string>? processMouseEvents;
        private Action<string>? processKeyStroke;

        private string lastKeyPressed = string.Empty;

        public void Subscribe(
            Action<string> processMouseEvents, 
            Action<string> processKeyStrokes)
        {
            this.processMouseEvents = processMouseEvents;
            this.processKeyStroke = processKeyStrokes;

            globalHook = Hook.GlobalEvents();
            globalHook.MouseDownExt += GlobalHookMouseDownExt;
            //globalHook.KeyPress += GlobalHookKeyPress;            
            globalHook.KeyUp += GlobalHookKeyUp;            
        }

        //private void GlobalHookKeyPress(object? sender, KeyPressEventArgs e)
        //{
        //    this.lastKeyPressed = e.KeyChar.ToString();            
        //}

        private void GlobalHookKeyUp(object? sender, KeyEventArgs e)
        {
            if (IsControlKey(e))
            {
                string charFromKey = KeyboardUtil.GetCaracterFromKey(e.KeyCode.ToString());

                if (!string.IsNullOrEmpty(charFromKey))
                {
                    this.processKeyStroke!(charFromKey);
                    return;
                }

                this.processKeyStroke!($"<{e.KeyCode.ToString()}>");
                return;
            }

            this.processKeyStroke!(e.KeyData.ToString());
        }

        private void GlobalHookMouseDownExt(object? sender, MouseEventExtArgs e)
        {
            this.processMouseEvents!(e.Button.ToString());
        }

        private bool IsControlKey(KeyEventArgs e)
        {
            var keyCode = e.KeyCode.ToString();
            var keyData = e.KeyData.ToString().Split(",").First();

            return keyCode == keyData && keyData.Length > 1;
        }

        public void Unsubscribe()
        {
            if (globalHook != null)
            {
                globalHook.MouseDownExt -= GlobalHookMouseDownExt;
                globalHook.KeyUp -= GlobalHookKeyUp;

                globalHook.Dispose();
            }
        }
    }
}
