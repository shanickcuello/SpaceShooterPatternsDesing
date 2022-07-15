namespace UseCases.Services
{
    public interface IObserver
    {
        void Notify(EActions action);
    }

    public enum EActions
    {
        TargetHit
    }
}