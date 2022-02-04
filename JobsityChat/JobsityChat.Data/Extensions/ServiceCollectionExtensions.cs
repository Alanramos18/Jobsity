using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace JobsityChat.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Register all the dependencies for this component
        /// </summary>
        /// <param name="services"></param>
        public static void AddChatData(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            services.AddDbContext<ChatDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ChatConnection")));

            services.AddScoped<IChatDbContext, ChatDbContext>();
        }
    }
}
