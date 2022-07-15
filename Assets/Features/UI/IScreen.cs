namespace Features.UI
{
    public interface IScreen
    {
        void Activate();

        void Deactivate();

        string Free();
    }
}
