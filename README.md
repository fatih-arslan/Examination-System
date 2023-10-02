# Sınav/Quiz uygulaması
Projenin amacı kullanıcılara sisteme kayıt olup soru ekleyerek sınav hazırlama ya da hazırlanmış sınavlara girme imkanı sunan bir program geliştirmektir.
Program Microsoft Visual Studio kullanılarak c# dilinde kodlanmıştır.

# Sınav Sorularının Belirlenme Algoritması
Öğrencilerin her gün için bir sınav hakkı vardır. Günlük sınavlarda 10 yeni soru sorulur ve daha önceki sınavlarda doğru cevapladığı sorular belli aralıklarla tekrar ettirilir.

Öğrencinin bir soruyu hakkı ile bilmesi için altı kez üst üste doğru cevabı işaretlemesi gerekir. Eğer 6 kez aynı soru için doğru cevabı vermez ise, süreç o soru için başa döner ve tekrar 6 kez aynı soru için doğru cevabı vermesi beklenir.

Bilinen bir sorunun testte öğrenciye tekrar sorulması için kullanılacak zaman aralığı varsayılan olarak 1 gün sonra, 1 hafta sonra, 1 ay sonra, 3 ay sonra, 6 ay sonra ve 1 yıl sonradır. Öğrenci soru tekrar sıklığını program içerisinde değiştirebilir. Eğer 6 farklı zamanda da aynı soruyu doğru olarak bilirse soru kalıcı olarak doğru cevaplanmış kabul edilir.


 

