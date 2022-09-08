using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniProject.Service.Interface.Service;
using MiniProject.Service.Service;

namespace MiniProject.Service
{
    public class ServiceDepedencyProfile
    {
        public static void Register(IConfiguration configuration, IServiceCollection services)
        {
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IDBService, DBService>();
        }
    }
}
