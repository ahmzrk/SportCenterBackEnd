using Core.Utilities.Observer.Abstract;

namespace Core.Utilities.Observer.Concrete
{
    public class Observe : ISubject
    {
        private List<IObserver> _observers = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            Console.WriteLine("Stock: Yeni bir gözlemci eklendi.");
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
            Console.WriteLine("Stock: Bir gözlemci çıkarıldı.");
        }
         
        public void Notify()
        {
            Console.WriteLine("\nStock: Fiyat değişti, gözlemcileri bilgilendiriyorum...");

            foreach (var observer in _observers)
            {
                observer.Update(this);
            }
        }
    }
    }

