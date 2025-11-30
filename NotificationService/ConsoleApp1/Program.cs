// Observer Kalıbı Örneği
// Observer Pattern, bir nesnenin (Subject) durumunda bir değişiklik olduğunda,
// ona bağımlı olan diğer nesnelerin (Observer) otomatik olarak bilgilendirilmesini sağlar.
// Bu, gevşek bağlı (loosely coupled) bir mimari oluşturur.

using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;

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

// ---------------------------------------------------------------------------------------
// Adım 5: Mail Servisi için bir Gözlemci Sınıfı Ekleme
//
// Observer arayüzünü uygulayan bir e-posta servisi sınıfı.
// ---------------------------------------------------------------------------------------
public class MailService : IObserver
{
    private readonly string _recipientEmail;

    public MailService(string recipientEmail)
    {
        _recipientEmail = recipientEmail;
    }

    public void Update(ISubject subject)
    {
        if (subject is Stock stock)
        {
            // Gerçek bir senaryoda bu kısımda mail gönderme işlemleri yapılır.
            Console.WriteLine($"MailService: {_recipientEmail} adresine e-posta gönderildi. Yeni fiyat: {stock.Price:C}");

            // E-posta gönderme metodunu çağır.
            // Bu örnekte, dummy veriler kullanıyoruz.
            SendPaymentEmail(_recipientEmail, "Ahmet Yılmaz", "12345", (decimal)stock.Price);
        }
    }

    // E-posta gönderme metodu
    public static bool SendPaymentEmail(string customerEmail, string customerName, string orderNumber, decimal amount)
    {
        try
        {
            // SMTP ayarları - Mail göndermek için gerekli server bilgileri
            string smtpServer = "smtp.gmail.com";
            int smtpPort = 587;
            string senderEmail = "ahmet.2452.123@gmail.com";
            string senderPassword = "hnbc rnji uuox pxhn"; // Gmail app password

            // Mail istemcisi oluştur
            using (SmtpClient client = new SmtpClient(smtpServer, smtpPort))
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);

                // Mail mesajını oluştur
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(senderEmail, "Şirket Adınız");
                    mail.To.Add(customerEmail);
                    mail.Subject = $"Ödemeniz Onaylandı - Sipariş #{orderNumber}";
                    mail.Body = $@"
                    <html>
                    <body style='font-family: Arial, sans-serif;'>
                        <div style='background-color: #4CAF50; color: white; padding: 20px; text-align: center;'>
                            <h2>✓ Ödemeniz Başarıyla Tamamlandı!</h2>
                        </div>
                        
                        <div style='padding: 20px;'>
                            <p>Sayın <strong>{customerName}</strong>,</p>
                            
                            <p>Ödemeniz başarıyla alınmıştır.</p>
                            
                            <div style='background-color: #f0f0f0; padding: 15px; margin: 15px 0;'>
                                <p><strong>Sipariş No:</strong> {orderNumber}</p>
                                <p><strong>Tutar:</strong> {amount:C} TL</p>
                                <p><strong>Tarih:</strong> {DateTime.Now:dd.MM.yyyy HH:mm}</p>
                            </div>
                            
                            <p>Teşekkür ederiz!</p>
                        </div>
                    </body>
                    </html>";
                    mail.IsBodyHtml = true;

                    // Mail gönder
                    client.Send(mail);
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            // Hata durumunda false döndür (isteğe bağlı: hatayı logla)
            Console.WriteLine("Mail gönderimi hatası: " + ex.Message);
            return false;
        }
    }
}

// ---------------------------------------------------------------------------------------
// Adım 6: Uygulamayı Çalıştırma
//
// Ana programda Subject ve Observer nesnelerini oluşturup,
// kalıbın nasıl çalıştığını gözlemleyelim.
// ---------------------------------------------------------------------------------------
public class Program
{
    public static void Main(string[] args)
    {
        // Subject nesnesini (hisse senedi) oluşturun.
        var stock = new Stock();

        // Gözlemci nesnelerini (fiyat göstergesi) oluşturun.
        var priceDisplay1 = new PriceDisplay();
        var priceDisplay2 = new PriceDisplay();
        var mailService = new MailService("redoes123@hotmail.com");

        // Gözlemcileri Subject'e ekleyin (abone olun).
        stock.Attach(priceDisplay1);
        stock.Attach(priceDisplay2);
        stock.Attach(mailService);

        // Hisse senedi fiyatını değiştirin. Bu, otomatik olarak
        // gözlemcilere bildirim gönderecektir.
        stock.Price = 10.50;
        stock.Price = 12.75;

        // Bir gözlemciyi listeden çıkarın.
        stock.Detach(priceDisplay2);

        // Fiyatı tekrar değiştirin. Sadece kalan gözlemci bilgilendirilecektir.
        stock.Price = 15.00;
    }
}
