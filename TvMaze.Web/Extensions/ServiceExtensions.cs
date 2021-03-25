using Microsoft.Extensions.DependencyInjection;
using TvMaze.Api.Client;
using TvMaze.Data;
using TvMaze.Web.Services;

namespace TvMaze.Web.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddTvMaze(this IServiceCollection services)
        {
            return services
                .AddDbContext<TvMazeContext>()
                .AddSingleton<TvMazeClient>()
                .AddHostedService<TvMazeService>();
        }
    }
}
