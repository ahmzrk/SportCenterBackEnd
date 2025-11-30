namespace Core.Utilities.Observer.Abstract
{
    public interface IObserver
    {
        void Update(ISubject subject);
    }
}
