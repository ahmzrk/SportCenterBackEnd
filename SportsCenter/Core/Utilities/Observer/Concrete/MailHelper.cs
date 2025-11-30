using System.Net;
using System.Net.Mail;

public static class MailHelper
{
    public static bool SendPaymentEmail(string customerEmail, string customerName, string orderNumber, decimal amount)
    {
        try
        {
            // SMTP ayarları - Mail göndermek için gerekli server bilgileri
            string smtpServer = "smtp.gmail.com";      // SMTP sunucu adresi
            int smtpPort = 587;                        // SMTP port numarası
            string senderEmail = "ahmet.2452.123@gmail.com";
            string senderPassword = "hnbc rnji uuox pxhn";

            // Mail istemcisi oluştur
            using (SmtpClient client = new SmtpClient(smtpServer, smtpPort))
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);

                // Mail mesajını oluştur
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(senderEmail, "Şirket Adınız");
                mail.To.Add(customerEmail);
                mail.Subject = $"Ödemeniz Onaylandı - Sipariş #{orderNumber}";

                // Mail içeriği
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
                return true;
            }
        }
        catch (Exception ex)
        {
            // Hata durumunda false döndür (isteğe bağlı: hatayı logla)
            Console.WriteLine("Mail gönderimi hatası: " + ex.Message);
            return false;
        }
    }
}