namespace Game
{
    public class Controls
    {
        public InputActions Actions { get; }

        public Controls()
        {
            Actions = new InputActions();
            Actions.Enable();
        }

        ~Controls()
        {
            Actions.Disable();
        }
    }
}