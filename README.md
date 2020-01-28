# CreditConfirmationSystem

1. Sistemde 3 ana servis bulunmaktadır.
	1.1. CreditConfirmation.Web.API
		.Net Core MVC ile yazılan bu uygulamadan kullanıcı bilgilerini ekrandan alarak kredi onaylaması sonucunu göstermektedir.
		
	1.2. CreditConfirmation.API
		Bu uygulamada arayüzden alınan kullanıcı bilgileri için kredi onaylaması yapılmaktadır. 
		CreditScore.API dan random hesaplanan bir skor ile kullanıcının kredi onaylaması veya reddi yapılmaktadır.
		Limit hesaplama için Strategy Pattern kullanılmıştır.
		Hesaplama sonucunun MongoDb' ye yazılması için Factory Pattern ve Repository Pattern kullanılmıştır.
		
	1.3. CreditScore.API
		Kullanıcıya ait rastgele bir skor gönderilmektedir. Her gönderimde rastgele bir skor üretilmektedir. Skor değerlerinin anlamlı olması açısından öncesinde 0-500,
		500-1000 veya 1000-int.max arasından hangi aralıkta üretileceği random karar verilmekte sonrasında ilgili aralık değerlerinde rastgele bir skor üretilmektedir.
	
2. Uygulamalar Docker Container ile ayağa kaldırılmıştır. Her uygulama için swagger oluşturulmuştur.
http://localhost:51222/			     - CreditScore.API
http://localhost:52536/swagger/index.html    - UI
http://localhost:59767/swagger/index.html    - CreditConfirmation.API

3. Eklenebilir diğer konular
	- GlobalErrorHandling eklenmemiştir. Bu yüzden ErrorHandling yapılabilir.
	- Loglama yapılmamıştır.
	
	
