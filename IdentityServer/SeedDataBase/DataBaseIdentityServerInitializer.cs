using IdentityModel;
using IdentityServer.Configuration;
using IdentityServer.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IdentityServer.SeedDataBase;


    public class DataBaseIdentityServerInitializer : IDataBaseSeedInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DataBaseIdentityServerInitializer(UserManager<ApplicationUser> user,
            RoleManager<IdentityRole> role)
        {
            _userManager = user;
            _roleManager = role;
        }

        public void InitializeSeedRoles()
        {
            //Se o Perfil Admin não existir então cria o perfil 
            if (!_roleManager.RoleExistsAsync(IdentityConfiguration.Admin).Result)
            {
                //cria o perfil Admin
                IdentityRole roleAdmin = new IdentityRole();
                roleAdmin.Name = IdentityConfiguration.Admin;
                roleAdmin.NormalizedName = IdentityConfiguration.Admin.ToUpper();
                _roleManager.CreateAsync(roleAdmin).Wait();
            }

            // se o perfil Client não existir então cria o perfil
            if (!_roleManager.RoleExistsAsync(IdentityConfiguration.Client).Result)
            {
                //cria o perfil Client
                IdentityRole roleClient = new IdentityRole();
                roleClient.Name = IdentityConfiguration.Client;
                roleClient.NormalizedName = IdentityConfiguration.Client.ToUpper();
                _roleManager.CreateAsync(roleClient).Wait();
            }
        }

        public void InitializeSeedUsers()
        {
            //se o usuario admin não existir cria o usuario , define a senha e atribui ao perfil
            if (_userManager.FindByEmailAsync("brandao@brandaosoft.com.br").Result == null)
            {
                //define os dados do usuário admin
                ApplicationUser admin = new ApplicationUser()
                {
                    UserName = "brandao",
                    NormalizedUserName = "BRANDAO",
                    Email = "brandao@brandaosoft.com.br",
                    NormalizedEmail = "BRANDAO@BRANDAOSOFT.COM.BR",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    PhoneNumber = "+55 (11) 12345-6789",
                    FirstName = "Felipe",
                    LastName = "Brandao",
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                //cria o usuário Admin e atribui a senha
                IdentityResult resultAdmin = _userManager.CreateAsync(admin, "Numsey#2024").Result;
                if (resultAdmin.Succeeded)
                {
                    //inclui o usuário admin ao perfil admin
                    _userManager.AddToRoleAsync(admin, IdentityConfiguration.Admin).Wait();

                    //inclui as claims do usuário admin
                    var adminClaims = _userManager.AddClaimsAsync(admin, new Claim[]
                    {
                    new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin)
                    }).Result;
                }
            }

            //se o usuario client não existir cria o usuario , define a senha e atribui ao perfil
            if (_userManager.FindByEmailAsync("user@brandaosoft.com").Result == null)
            {
                //define os dados do usuário client
                ApplicationUser client = new ApplicationUser()
                {
                    UserName = "user",
                    NormalizedUserName = "USER",
                    Email = "user@brandaosoft.com",
                    NormalizedEmail = "USER@BRANDAOSOFT.COM",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    PhoneNumber = "+55 (11) 12345-6789",
                    FirstName = "User",
                    LastName = "ClientUser",
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                //cria o usuário Client e atribui a senha
                IdentityResult resultClient = _userManager.CreateAsync(client, "Numsey#2022").Result;
                //inclui o usuário Client ao perfil Client
                if (resultClient.Succeeded)
                {
                    _userManager.AddToRoleAsync(client, IdentityConfiguration.Client).Wait();

                    //adiciona as claims do usuário Client
                    var clientClaims = _userManager.AddClaimsAsync(client, new Claim[]
                    {
                    new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, client.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, client.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client)
                    }).Result;
                }
            }
        }
    }
