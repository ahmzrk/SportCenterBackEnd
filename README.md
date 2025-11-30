SportsCenter Backend

Mikroservis Mimarisi + Katmanlı Mimari + Modern .NET Ekosistemi

Bu proje, bir spor salonu yönetim sistemi için geliştirilmiş; katmanlı mimari ve mikroservis mimarisinin bir arada kullanıldığı, ölçeklenebilir ve kurumsal standartlarda bir backend uygulamasıdır.
Servisler, Docker üzerinde konteynerize edilmiştir. API yönlendirmeleri Ocelot, servisler arası iletişim ise RabbitMQ ile sağlanmaktadır.

🚀 Kullanılan Teknolojiler

C# / .NET 8

Entity Framework Core

SQL Server

Redis Cache

JWT Authentication

Hashing & Encryption

Transaction Management

AOP (Aspect Oriented Programming)

Docker

Ocelot API Gateway

RabbitMQ

Iyzico (Ödeme Servisi)

Mail Service (Bildirim)

🏗️ Katmanlı Mimari Yapısı
📂 Entities Layer

Veritabanı varlıkları (Entities)

Model tanımları

DTO yapıları

📂 Data Access Layer (DAL)

Entity Framework Core ile veritabanı işlemleri

Veri yönetimi ve sorgular

📂 Business Layer

İş kuralları

Validation süreçleri

AOP ile:

Caching

Logging

Transaction yönetimi

Validation

📂 Core Layer

JWT helper

Hashing & Encryption araçları

AOP altyapısı

Redis cache yöneticisi

Ortak servisler ve yardımcı sınıflar

📂 API Layer

REST API uç noktaları

JWT tabanlı kimlik doğrulama

Role-based Authorization

🧩 Mikroservis Mimarisi

Katmanlı yapı üzerine eklenen mikroservisler:

🔧 Eklenen Mikroservisler
Servis	Açıklama
PDFService =	Üye bilgilerini PDF formatında oluşturan servis

PaymentService = Iyzico ile ödeme işlemlerinin yönetildiği servis

Notification Service	Mail gönderimi yapan servis
🔀 Servis İletişimi

Servisler arası haberleşme RabbitMQ ile sağlanmıştır.

API yönlendirmeleri Ocelot API Gateway üzerinden yapılmaktadır.

🔒 Güvenlik

JWT tabanlı kimlik doğrulama

Role-based authorization

Güçlü hashing & encryption

oken kontrolüT

AOP ile merkezi güvenlik ve doğrulama işlemleri

📌 Özet

Bu backend çözümü:

✔ Katmanlı mimari temellidir
✔ Mikroservis yapısıyla ölçeklenebilir hale getirilmiştir
✔ Güvenli, modüler ve genişletilebilir bir altyapıya sahiptir
✔ Redis, Docker, RabbitMQ ve API Gateway yapılarıyla modern uygulama standartlarını karşılar
