using Gma.System.MouseKeyHook;

namespace Mimica.Services
{
    public class EventHooksService : IEventHooksService
    {
        private IKeyboardMouseEvents? globalHook;
        private Action<string>? processMouseEvents;
        private Action<string>? processKeyStrokes;

        public void Subscribe(
            Action<string> processMouseEvents, 
            Action<string> processKeyStrokes)
        {
            this.processMouseEvents = processMouseEvents;
            this.processKeyStrokes = processKeyStrokes;

            globalHook = Hook.GlobalEvents();
            globalHook.MouseDownExt += GlobalHookMouseDownExt;
            globalHook.KeyPress += GlobalHookKeyPress;            
        }

        private void GlobalHookKeyPress(object? sender, KeyPressEventArgs e)
        {
            this.processKeyStrokes!(e.KeyChar.ToString());
        }

        private void GlobalHookMouseDownExt(object? sender, MouseEventExtArgs e)
        {
            this.processMouseEvents!(e.Button.ToString());
        }

        public void Unsubscribe()
        {
            if (globalHook != null)
            {
                globalHook.MouseDownExt -= GlobalHookMouseDownExt;
                globalHook.KeyPress -= GlobalHookKeyPress;

                globalHook.Dispose();
            }
        }
    }
}
