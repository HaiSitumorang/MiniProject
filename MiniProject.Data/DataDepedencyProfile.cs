using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniProject.Data.Interface.Repositories;
using MiniProject.Data.Repositories;

namespace MiniProject.Data
{
    public class DataDepedencyProfile
    {
        public static void Register(IConfiguration configuration, IServiceCollection services)
        {
            services.AddScoped<IGameRepository, GameRepository>();
        }
    }
}
