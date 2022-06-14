using SingleResponsibilityPrinciple;

// Bir sınıf yalnızca bir amaç uğruna değiştirilebilir, o da o sınıfa yüklenen sorumluluktur,
// yani bir sınıfın yapması gereken yalnızca bir işi olması gerekir.


// Single responbility prenbine uygun olmayan yaklaşım.

UserManager userManager = new UserManager();

userManager.Name = "Fatih";
userManager.Email = "fatih@gmail.com";
userManager.Password = userManager.Hash("fd61");

userManager.UserAdd(userManager);


// Single responbility prenbine uygun olan yaklaşım.

Hashing Hashing = new Hashing();
UserRepo userRepo = new UserRepo(); 

User user = new User()
{
    Name = "Fatih",
    Email = "fatih@gmail.com",
    Password = Hashing.Hash("fd61")
};

userRepo.UserAdd(user);
