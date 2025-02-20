namespace Mimica.Services
{
    public interface IEventHooksService
    {
        void Subscribe(
            Action<string> processMouseEvents,
            Action<string> processKeyUp,
            Action<string> processKeyPress);

        void Unsubscribe();
    }
}
