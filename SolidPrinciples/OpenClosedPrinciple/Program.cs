using OpenClosedPrinciple;

// Bir sınıf ya da fonksiyon halihazırda var olan özellikleri korumalı ve değişikliğe izin vermemelidir.
// Yani davranışını değiştirmiyor olmalı ve yeni özellikler kazanabiliyor olmalıdır.

Footbal footbal1 = new Footbal();
footbal1.Win = 23;
footbal1.Lose = 3;
footbal1.Draw = 12;

Footbal footbal2 = new Footbal();
footbal2.Win = 21;
footbal2.Lose = 10;
footbal2.Draw = 7;

Basketball basketball1 = new Basketball();
basketball1.Win = 25;
basketball1.Lose = 5;

List<Sport> sports = new List<Sport>();
sports.Add(basketball1);
sports.Add(footbal1); 
sports.Add(footbal2);

CalculatePoint cp = new CalculatePoint();
var point = cp.Point(sports);
Console.WriteLine(point);