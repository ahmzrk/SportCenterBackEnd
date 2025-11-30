// Observer Kalıbı Örneği
// Observer Pattern, bir nesnenin (Subject) durumunda bir değişiklik olduğunda,
// ona bağımlı olan diğer nesnelerin (Observer) otomatik olarak bilgilendirilmesini sağlar.
// Bu, gevşek bağlı (loosely coupled) bir mimari oluşturur.

// ---------------------------------------------------------------------------------------
// Adım 4: Somut Observer Sınıfını (Concrete Observer) Oluşturma
//
// Bu sınıf, Subject'ten gelen bildirimleri işleyecek olan nesnedir.
// ---------------------------------------------------------------------------------------
public class PriceDisplay : IObserver
{
    public void Update(ISubject subject)
    {
        // Subject'i kendi tipimize dönüştürerek, onun verilerine erişebiliriz.
        if (subject is Stock stock)
        {
            Console.WriteLine($"PriceDisplay: Hisse senedi fiyatı güncellendi: {stock.Price:C}");
        }
    }
}
