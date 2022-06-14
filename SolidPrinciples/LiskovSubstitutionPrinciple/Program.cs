using LiskovSubstitutionPrinciple;

// Kodlarımızda herhangi bir değişiklik yapmaya gerek duymadan alt sınıfları, türedikleri(üst) sınıfların yerine kullanabilmeliyiz.

Instagram instagram = new Instagram();

instagram.SendMessage("message");
instagram.LiveBroadCast();

Twitter twitter = new Twitter();
twitter.SendMessage("message");