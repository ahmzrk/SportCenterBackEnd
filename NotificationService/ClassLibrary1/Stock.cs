// Observer Kalıbı Örneği
// Observer Pattern, bir nesnenin (Subject) durumunda bir değişiklik olduğunda,
// ona bağımlı olan diğer nesnelerin (Observer) otomatik olarak bilgilendirilmesini sağlar.
// Bu, gevşek bağlı (loosely coupled) bir mimari oluşturur.

// ---------------------------------------------------------------------------------------
// Adım 3: Somut Subject Sınıfını (Concrete Subject) Oluşturma
//
// Bu sınıf, gözlemcilerin abone olacağı ve durumunu değiştireceği nesnedir.
// Gözlemcileri bir liste içinde tutar ve durum değiştiğinde onları bilgilendirir.
// ---------------------------------------------------------------------------------------
public class Stock : ISubject
{
    // Gözlemcileri saklamak için bir liste
    private List<IObserver> _observers = new List<IObserver>();

    // Hisse senedi fiyatı
    private double _price;

    // Fiyata erişim için property.
    // Fiyat değiştiğinde, tüm gözlemcilere bildirim gönderilir.
    public double Price
    {
        get { return _price; }
        set
        {
            if (_price != value)
            {
                _price = value;
                // Fiyat değiştiğinde tüm gözlemcilere haber ver.
                Notify();
            }
        }
    }

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

    // Gözlemcilere bildirim gönderme metodunun uygulaması
    public void Notify()
    {
        Console.WriteLine("\nStock: Fiyat değişti, gözlemcileri bilgilendiriyorum...");

        // Abone olan her bir gözlemciye güncelleme gönderilir.
        foreach (var observer in _observers)
        {
            observer.Update(this);
        }
    }
}
