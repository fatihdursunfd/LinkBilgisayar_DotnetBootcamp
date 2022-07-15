 Proje n-katmanlı mirariye göre best pratices'lere uyularak geliştirilmiştir.<br/>
 Projede temel olarak API, Core, Data ve Service katmanları bulunur.<br/>
 Bunun dışında RabbitMQ ile işlemleri yapmak için bir RabbitMQ katmanı,<br/>
 Resimlere watermark eklemek için kullanılan bir background service olan WatermarkService katmanı,<br/>
 Quartz kütüpjanesi kullanılarak haftalık ve aylık raporlar göndermek için kullanılan yine bir bakground service olan Report katmanı vardır.<br/>
 
 Proje çalıştırıldığında oluşturulan haftalık ve aylık raporlar excel olarak Attechments dosyasına eklenmiştir.<br/>
 Attechments dosyasında ayrıca gönderilen bir mail örneği, watermark eklenmiş bir resim ve veritabanının backup'ı vardır
