                                                                                                               **Proje Özeti**
                                                                                                               
         Bir alışveriş uygulaması. Kullanıcı giriş yapar ve yetkisi doğrultusunda işlem yapabilir. Müşteri yetkisinde iken ürün ekleme güncelleme gibi özellikleri yapamaz. Bunu sadece admin girişine sahip kullanıcılar yapabilir.
         Bir sipariş verildiğinde yeni bir sipariş-ürün tablosu oluşur aynı zamanda da ürün tablosundaki stok değerleri değişir. Middleware kullanarak uygulamayı bakım durumuna sadece admin getirebiliyor ve admin bu 
         durumdan çıkarabiliyor. Bir tane de action filter örneği projemde mevcut. Sadece belli saat aralığında ürün güncellemesi yaptırabiliyor. Projemin temel iş mantığı bu şekildedir. Proje aynı zamanda katmanlı mimari 
         ile yapılmıştır. Data katmanı veri tabanıyla, business katmanı temel iş mantığıyla sorumludur. 


                                                                                                             **Nasıl Çalışır**
                                                                                                             
         Yeni bir migration oluşturup kendi bilgisayarınıza bağlayıp database nizi güncellemeniz gerekiyor. Json.development kısmında ayarları özelleştirebilirsiniz. Ardından proje sorunsuzca çalışacaktır. Bu işlemleri
         data kısmında yapmayı unutmayınız.



                                                                                                          **Kullanılan Teknolojiler**
                                                                                                          
        Visual Studio 2022, sql manager, asp.net Api, c# 

         
