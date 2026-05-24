# InvoiceApp

InvoiceApp, ASP.NET Core Razor Pages kullanılarak geliştirilmiş bir fatura yönetim uygulamasıdır.  
Projede Entity Framework Core ve Microsoft SQL Server kullanılmıştır.

## Kullanılan Teknolojiler

- ASP.NET Core Razor Pages
- C#
- Entity Framework Core
- Microsoft SQL Server
- SQL Server Management Studio
- Bootstrap
- Git / GitHub

## Proje Özellikleri

- Fatura listeleme
- Yeni fatura oluşturma
- Fatura düzenleme
- Fatura detay görüntüleme
- Fatura silme
- Çok satırlı fatura oluşturma
- Fatura satırı ekleme ve silme
- Arama ve filtreleme
- Sayfalama
- 10 / 25 / 50 kayıt gösterme seçimi
- Paid / Pending durum renklendirmesi
- Silme işleminde modal onay ekranı
- Silinecek faturanın önce kullanıcıya gösterilmesi
- Details / Edit / Delete işlemlerinin tek Edit sayfasında mode yapısıyla birleştirilmesi
- Para alanlarının küsüratlı formatta gösterilmesi
- Veritabanı scriptinin proje içerisine eklenmesi

## Veritabanı

Veritabanı scripti proje içinde aşağıdaki konumdadır:

```text
Database/InvoiceAppDb_FullScript.sql
```

Bu script içerisinde tablo yapıları ve örnek veriler bulunmaktadır.

Projede kullanılan ana tablolar:

- Invoices
- InvoiceItems

`Invoices` tablosu fatura üst bilgilerini tutar.  
`InvoiceItems` tablosu ise faturaya ait birden fazla satırı tutar.

## Projeyi Çalıştırma

1. Proje Visual Studio ile açılır.
2. Proje `InvoiceApp.csproj` dosyası üzerinden çalıştırılabilir.
3. SQL Server Management Studio açılır.
4. `Database/InvoiceAppDb_FullScript.sql` dosyası çalıştırılarak veritabanı oluşturulur.
5. `appsettings.json` içindeki bağlantı cümlesi kontrol edilir.

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=InvoiceAppDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

6. SQL Server bağlantısı farklıysa `Server=localhost` kısmı kendi SQL Server adına göre değiştirilir.
7. Proje Visual Studio üzerinden başlatılır.
8. Menüden `Invoices` sayfasına gidilerek fatura işlemleri kullanılabilir.

## İyileştirmeler

Hocanın istediği iyileştirmeler kapsamında aşağıdaki geliştirmeler yapılmıştır:

- Index sayfasına sayfalama eklendi.
- Kullanıcının 10 / 25 / 50 kayıt seçebilmesi sağlandı.
- Status alanı Paid ve Pending durumuna göre renklendirildi.
- Silme işleminde kullanıcıya modal onay penceresi gösterildi.
- Silinecek kayıt kullanıcıya gösterilerek doğru kaydı silip silmediğini kontrol etmesi sağlandı.
- Details, Edit ve Delete işlemleri tek Edit sayfasında mode yapısıyla birleştirildi.
- Birden fazla satıra sahip fatura oluşturma özelliği eklendi.
- Fatura satırı ekleme ve silme işlemleri yapıldı.
- Arama ve filtreleme özellikleri eklendi.
- Ana sayfa daha profesyonel hale getirildi.

## Not

Solution dosyası gerekirse Visual Studio tarafından yeniden oluşturulabilir.  
Proje `InvoiceApp.csproj` dosyası üzerinden açılabilir.

## GitHub

Proje GitHub bağlantısı:

```text
https://github.com/abdulselam-code/InvoiceApp
```