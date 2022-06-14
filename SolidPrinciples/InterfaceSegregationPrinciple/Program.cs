// Sorumlulukların hepsini tek bir arayüze toplamak yerine daha özelleştirilmiş birden fazla arayüz oluşturmalıyız.

using InterfaceSegregationPrinciple;

ITelephone iphone = new IPhone();

iphone = (IPhone) iphone;

((IPhone)iphone).TakeVideo();
((IPhone)iphone).TakePhoto();
((IPhone)iphone).TakePortrait();
iphone.MakeCall();
iphone.ConnectToInternet();

ITelephone nokia = new Nokia();
nokia.MakeCall();
nokia.ConnectToInternet();