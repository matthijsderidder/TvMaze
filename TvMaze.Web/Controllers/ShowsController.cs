using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TvMaze.Data.Models;
using TvMaze.Data;
using Microsoft.EntityFrameworkCore;

namespace TvMaze.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShowsController : ControllerBase
    {
        private readonly ILogger<ShowsController> _logger;
        private readonly TvMazeContext _database;

        public ShowsController(ILogger<ShowsController> logger, TvMazeContext database)
        {
            _logger = logger;
            _database = database;
        }

        [HttpGet]
        public IEnumerable<Show> Get(int page = 0, int size = 10)
        {
            var query = _database.Shows
                .Include(x => x.Cast)
                .OrderBy(x => x.Id)
                .ThenBy(x => x.Cast.OrderByDescending(y => y.Birthday).FirstOrDefault())
                .Skip(page * size)
                .Take(size);
                
            return query.ToList();
        }
    }
}
