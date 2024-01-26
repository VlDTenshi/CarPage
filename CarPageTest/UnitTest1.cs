using CarPage.ApplicationServices.Services;
using CarPage.Core.ServiceInterface;
using CarPage.Data;
using CarPageTest.Macros;
using CarPageTest.Mock;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CarPageTest
{
    public abstract class UnitTest1
    {
        protected IServiceProvider serviceProvider { get; }

        protected UnitTest1()
        {
            var services = new ServiceCollection();
            SetupServices(services);
            serviceProvider = services.BuildServiceProvider();

        }
        public void Dispose()
        {

        }
        protected T Svc<T>()
        {
            return serviceProvider.GetService<T>();
        }
        protected T Macro<T>() where T : IMacros
        {
            return serviceProvider.GetService<T>();
        }

        public virtual void SetupServices(IServiceCollection services)
        {
            services.AddScoped<ICarItemServices, CarItemServices>();
            services.AddScoped<IFileServices, FileServices>();
            services.AddScoped<IHostEnvironment, MockHostEnvironment>();

            services.AddDbContext<CarContext>(x =>
            {
                x.UseInMemoryDatabase("TEST");
                x.ConfigureWarnings(e => e.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });
            RegisterMacros(services);
        }
        private void RegisterMacros(IServiceCollection services)
        {
            var macroBaseType = typeof(IMacros);

            var macros = macroBaseType.Assembly.GetTypes()
                .Where(x => macroBaseType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);
            foreach (var macro in macros)
            {
                services.AddSingleton(macro);
            }

        }
    }
}
