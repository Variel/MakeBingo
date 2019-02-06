using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MakeBingo.Models;
using MakeBingo.Services;
using Microsoft.EntityFrameworkCore;

namespace MakeBingo.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext _database;

        public HomeController(DatabaseContext database)
        {
            _database = database;
        }

        public async Task<IActionResult> Index()
        {
            var boards = await _database.Boards.Include(b => b.Results).OrderByDescending(b => b.Results.Count).Take(9).ToArrayAsync();

            return View(boards);
        }
    }
}
