using Shopping.Data.Entities;
using Shopping.Enum;
using Shopping.Helpers;

namespace Shopping.Data
{
    //Clase alimentadora para la Base de Datos
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }
        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCategoriesAsync();
            await CheckCountriesAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Manu", "Diaz", "manu@yopmail.com", "3815556397", "Av Calchaqui 159", UserType.Admin);
            await CheckUserAsync("2020", "Juan", "Perez", "juan@yopmail.com", "3815556300", "Av Siempre Viva 123", UserType.User);
        }

        private async Task<User> CheckUserAsync(
             string document,
             string firstName,
             string lastName,
             string email,
             string phone,
             string address,
             UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    City = _context.Cities.FirstOrDefault(),
                    UserType = userType,
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }

            return user;

        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "Tecnología" });
                _context.Categories.Add(new Category { Name = "Ropa" });
                _context.Categories.Add(new Category { Name = "Gamer" });
                _context.Categories.Add(new Category { Name = "Belleza" });
                _context.Categories.Add(new Category { Name = "Nutrición" });
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckCategoriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country
                {
                    Name = "Argentina",
                    States = new List<State>()
                    {
                        new State()
                        {
                            Name = "Tucumán",
                            Cities = new List<City>()
                            {
                                new City() { Name = "Famaillá" },
                                new City() { Name = "Lules" },
                                new City() { Name = "Monteros" },
                                new City() { Name = "San Miguel de Tucumán" },
                                new City() { Name = "Tafí del Valle" },
                                new City() { Name = "Simoca" }
                            }
                        },
                        new State()
                        {
                            Name = "Jujuy",
                            Cities = new List<City>()
                            {
                                new City(){ Name = "San Salvador de Jujuy"},
                                new City() {Name="Humahuaca"},
                                new City(){Name = "Tilcara"},
                                new City(){Name = "Purmamarca"},
                                new City() {Name= "Maimará"}
                            }
                        },
                        new State()
                        {
                            Name = "Salta",
                            Cities = new List<City>()
                            {
                                new City(){ Name = "Salta"},
                                new City() {Name="Orán"},
                                new City(){Name = "Irúya"},
                                new City(){Name = "Rivadavia"},
                                new City() {Name= "Cachi"}
                            }
                        },
                    }
                });
                _context.Countries.Add(new Country
                {
                    Name = "Colombia",
                    States = new List<State>()
                    {
                        new State()
                        {
                            Name = "Bogotá",
                            Cities = new List<City>()
                            {
                                new City(){Name="Usaquen"},
                                new City() {Name = "Champinero"},
                                new City() {Name="Santa fe"},
                                new City(){Name="Useme"},
                                new City(){Name="Bosa"}
                            }
                        },
                        new State()
                        {
                            Name = "Antioquia",
                            Cities = new List<City>()
                            {
                                new City(){Name = "Medellín"},
                                new City() { Name = "Itagüí" },
                                new City() { Name = "Envigado" },
                                new City() { Name = "Bello" },
                                new City() { Name = "Rionegro" }
                            }
                        }
                    }
                });
            }
            await _context.SaveChangesAsync();
        }
    }
}
