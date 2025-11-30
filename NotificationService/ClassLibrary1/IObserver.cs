// Observer Kalıbı Örneği
// Observer Pattern, bir nesnenin (Subject) durumunda bir değişiklik olduğunda,
// ona bağımlı olan diğer nesnelerin (Observer) otomatik olarak bilgilendirilmesini sağlar.
// Bu, gevşek bağlı (loosely coupled) bir mimari oluşturur.

// ---------------------------------------------------------------------------------------
// Adım 1: Observer Arayüzünü (Interface) Tanımlama
//
// Bu arayüz, tüm gözlemcilerin (Observer) uygulaması gereken
// metodu tanımlar. Subject, bu arayüz üzerinden gözlemcilere bildirim gönderir.
// ---------------------------------------------------------------------------------------
public interface IObserver
{
    // Subject'ten gelen bildirimle tetiklenen metot.
    void Update(ISubject subject);
}
