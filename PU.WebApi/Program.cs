using PU.Core.Repositories;
using PU.Core.Services.Contracts;
using PU.DataAccess;
using PU.DataAccess.Entities;
using PU.DataAccess.Repositories;
using PU.Services;
using PU.WebApi.StartupExtensions;

namespace PU.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCustomAutomapperConfiguration();

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddTransient(
                serviceProvider => new PURepositoryContextFactory().CreateDbContext(Array.Empty<string>()));
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IGroupService, GroupService>();
            builder.Services.AddTransient<IPermissionService, PermissionService>();

            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IGroupRepository, GroupRepository>();
            builder.Services.AddTransient<IPermissionRepository, PermissionRepository>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                // Seed database for assessment purposes
                using var context = new PURepositoryContextFactory().CreateDbContext(Array.Empty<string>()); ;
                SeedData(context);
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors();

            app.MapControllers();

            app.Run();
        }

        private static void SeedData(PURepositoryContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var dummyUserGroups = new List<UserGroup>();
            for (var i = 0; i <= 5; i++)
            {
                dummyUserGroups.Add(new UserGroup
                {
                    User = new User
                    {
                        ContactNumber = $"082000000{i}",
                        Email = $"{i}@sample.com",
                        FirstName = $"user{i}",
                        LastName = $"last{i}",
                        CreatedDate = DateTime.Now,
                    },
                    Group = new Group
                    {
                        Description = $"description{i}",
                        Name = $"group{i}",
                        CreatedDate = DateTime.Now,
                        GroupPermissions = new List<GroupPermission> {
                            new GroupPermission
                            {
                                Permission = new Permission
                                {
                                    Name = $"permission{i}",
                                    Description = $"{i}",
                                    CreatedDate = DateTime.Now,
                                }
                            }
                        }
                    }
                });
            }

            context.UserGroup.AddRange(dummyUserGroups);

            context.SaveChanges();
        }
    }
}