# Sınav/Quiz uygulaması
Projenin amacı kullanıcılara sisteme kayıt olup soru ekleyerek sınav hazırlama ya da hazırlanmış sınavlara girme imkanı sunmayı sağlayan bir program geliştirmektir.
Program Microsoft Visual Studio tümleşik geliştirme ortamı kullanılarak c# dilinde kodlanmıştır.

# Sınav Sorularının Belirlenme Algoritması
Öğrencilerin her gün için bir sınav hakkı vardır. Günlük sınavlarda 10 yeni soru sorulur ve daha önceki sınavlarda doğru cevapladığı sorular belli aralıklarla tekrar ettirilir.

Öğrencinin bir soruyu hakkı ile bilmesi için altı kez üst üste doğru cevabı işaretlemesi gerekir. Eğer 6 kez aynı soru için doğru cevabı vermez ise, süreç o soru için başa döner ve tekrar 6 kez aynı soru için doğru cevabı vermesi beklenir.

Bilinen bir sorunun testte öğrenciye tekrar sorulması için kullanılacak zaman aralığı varsayılan olarak 1 gün sonra, 1 hafta sonra,1 ay sonra,3 ay sonra,6 ay sonra ve 1 yıl sonradır. Öğrenci soru tekrar sıklığını program içerisinde değiştirebilir. Eğer 6 farklı zamanda da aynı soruyu doğru olarak bilirse soru kalıcı olarak doğru cevaplanmış kabul edilir.

# Programın Kullanımı ve Ekran Görüntüleri
Ana menü
![Ana menu](https://user-images.githubusercontent.com/80519936/186174214-7201b123-88a1-49e3-b937-ff698f8271f7.png)

Öğrenci sınav ekranı
![Ogrenci ekrani](https://user-images.githubusercontent.com/80519936/186170468-854bf37c-c442-48f8-b25b-e6c46b5a4ae9.png)
![Sinav ekrani](https://user-images.githubusercontent.com/80519936/186170664-cf04e84d-1eae-4b20-8820-aec7de021536.png)

Sınav sonuç ekranı
![Sinav sonuc](https://user-images.githubusercontent.com/80519936/186172351-3cc7a23e-edc9-4516-856d-64be24156693.png)

Alıştırma sınavı ekranı (Alıştırma sınavında süre yoktur ve sadece seçilen üniteden sorular cevaplanır)
![Alistirma](https://user-images.githubusercontent.com/80519936/186172051-682ffc0b-c205-41bf-92af-479e37a2c820.png)

Öğrenciler için hesap ve soru tekrar sıklığı ayarları
![Ogrenci ayarlar](https://user-images.githubusercontent.com/80519936/186174689-52f2119b-b41c-4c89-8a88-01ddd80397a9.png)

Sınav sorumluları için soru hazırlama ekranı
![Sinav sorumlusu](https://user-images.githubusercontent.com/80519936/186171181-612ba43b-bbef-4ae4-97a6-5c53a46418be.png)

Sınav sorumluları için sınav sonuçları ekranı
![Sonuc gor](https://user-images.githubusercontent.com/80519936/186175121-a4c95858-8af9-4e05-9b76-c4ac473dfb73.png)

Admin hesapları için kayıtlı kullanıcıları görme
![Admin](https://user-images.githubusercontent.com/80519936/186175387-ccc53e0b-2254-4329-bd7e-acec4661728e.png)

Admin hesapları için eklenmiş soruları görme ve güncelleme
![Admin sorular](https://user-images.githubusercontent.com/80519936/186175781-bfbfe41a-9e43-439b-8065-368cd0fe44e2.png)


 

