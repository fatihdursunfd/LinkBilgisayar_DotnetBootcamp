using DependencyInversionPrinciple;

// Sınıflar arası bağımlılıklar olabildiğince az olmalıdır özellikle üst seviye sınıflar alt seviye sınıflara bağımlı olmamalıdır.


IUserRepo _userRepoSql = new UserRepoSql();
UserService _userServiceSql = new UserService(_userRepoSql);
var usersSql = _userServiceSql.GetUsers();
Console.WriteLine("Users from sql : ");
foreach (var user in usersSql)
    Console.WriteLine(user);


IUserRepo _userRepoMongo = new UserRepoMongo();
UserService _userServiceMongo = new UserService(_userRepoMongo);
var usersMongo = _userServiceMongo.GetUsers();
Console.WriteLine("Users from mongo : ");
foreach (var user in usersMongo)
    Console.WriteLine(user);
