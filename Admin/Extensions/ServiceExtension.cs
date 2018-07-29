using Admin.Business;
using Admin.Configuration;
using Admin.Dal.Repository.Abstraction;
using Admin.Dal.Repository.Implementation;
using System;


namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddModuleAdmin(this IServiceCollection services, Action<AdminOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }



            //string connectionString = @"Server=DESKTOP-LCV6O88\SQLEXPRESS;Database=AlzaLegoDatabase;User Id=sa;Password=master";
            //services.AddDbContext<EFLocalizationDbContext>(options => options.UseSqlServer(connectionString));




            //registruje nastaveni modulu
             services.Configure(setupAction);

            //connectionString si vezme sam DbContext z IOptions<>

            //REPOSITORY
            services.AddScoped<IInsertAdminRepository, InsertAdminRepository>();


            //SERVICES - zapouzdreni vsechn repositories pod jeden objekt
            //Tyto services pak budou pouzivat ostatni tridy/objetky
            services.AddScoped<AdminService, AdminService>();


            return services;
        }
    }
}
