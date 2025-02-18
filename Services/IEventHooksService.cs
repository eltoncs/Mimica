namespace Mimica.Services
{
    public interface IEventHooksService
    {
        void Subscribe(
            Action<string> processMouseEvents,
            Action<string> processKeyStrokes);

        void Unsubscribe();
    }
}
