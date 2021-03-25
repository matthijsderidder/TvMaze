using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TvMaze.Api.Client;
using TvMaze.Data;
using TvMaze.Data.Extensions;
using TvMaze.Data.Models;
using TvMaze.Data.Relations;

namespace TvMaze.Web.Services
{
    public class TvMazeService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IServiceScopeFactory _factory;

        private Task _task;
        private CancellationTokenSource _cts;

        public TvMazeService(ILogger<TvMazeService> logger, IServiceScopeFactory factory)
        {
            _logger = logger;
            _factory = factory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("TvMaze Service is starting.");

            _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            _task = ExecuteAsync(_cts.Token);


            return _task.IsCompleted ? _task : Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("TvMaze Service is stopping.");

            if (_task == null)
                return;

            _cts.Cancel();

            await Task.WhenAny(_task, Task.Delay(-1, cancellationToken));

            cancellationToken.ThrowIfCancellationRequested();
        }

        protected async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using (var scope = _factory.CreateScope())
            {
                var provider = scope.ServiceProvider;

                var client = provider.GetRequiredService<TvMazeClient>();
                var database = provider.GetRequiredService<TvMazeContext>();

                while (!cancellationToken.IsCancellationRequested)
                {
                    var shows = (await client.Search.ShowSearchAsync("girls")).Select(x => x.Show).ToList();
                    foreach (var show in shows)
                    {
                        _logger.LogDebug($"Show: {show}");

                        var s = new Show { Id = show.Id, Name = show.Name };
                        database.Shows.AddIfNotExists(s, x => x.Id == show.Id);
                        database.SaveChanges();

                        var persons = (await client.Shows.GetShowCastAsync(show.Id)).Select(x => x.Person).ToList();
                        foreach (var person in persons)
                        {
                            _logger.LogDebug($"Person: {person}");
                            var p = new Person { Id = person.Id, Name = person.Name, Birthday = person.Birthday };
                            s.Cast.Add(p);
                            database.Persons.AddIfNotExists(p, x => x.Id == person.Id);
                            database.SaveChanges();
                            
                            //var rel = new ShowPerson { ShowId = show.Id, PersonId = person.Id };
                            //database.ShowPersons.AddIfNotExists(rel, x => x.ShowId == rel.ShowId && x.PersonId == rel.PersonId);
                            //database.SaveChanges();
                        }
                    }

                    try
                    {
                        database.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                    }

                    await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
                }
            }
        }
    }
}
