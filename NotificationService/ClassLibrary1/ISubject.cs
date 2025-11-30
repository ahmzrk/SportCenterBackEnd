// Observer Kalıbı Örneği
// Observer Pattern, bir nesnenin (Subject) durumunda bir değişiklik olduğunda,
// ona bağımlı olan diğer nesnelerin (Observer) otomatik olarak bilgilendirilmesini sağlar.
// Bu, gevşek bağlı (loosely coupled) bir mimari oluşturur.

// ---------------------------------------------------------------------------------------
// Adım 2: Subject Arayüzünü Tanımlama
//
// Bu arayüz, gözlemcileri yönetmek için gerekli metotları tanımlar.
// Gözlemciler bu arayüzü kullanarak Subject'e abone olabilir veya abonelikten çıkabilir.
// ---------------------------------------------------------------------------------------
public interface ISubject
{
    // Bir gözlemciyi listeye ekler.
    void Attach(IObserver observer);

    // Bir gözlemciyi listeden çıkarır.
    void Detach(IObserver observer);

    // Tüm gözlemcilere durum değişikliğini bildirir.
    void Notify();
}
