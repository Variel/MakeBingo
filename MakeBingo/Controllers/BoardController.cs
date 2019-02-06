using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakeBingo.Models;
using MakeBingo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MakeBingo.Controllers
{
    public class BoardController : Controller
    {
        private readonly DatabaseContext _database;

        public BoardController(DatabaseContext database)
        {
            _database = database;
        }

        public IActionResult Create(string parentId = null)
        {
            ViewBag.ParentId = parentId;

            return View();
        }

        public async Task<IActionResult> Play(string id)
        {
            var board = await _database.Boards
                .Include(b => b.Parent)
                .SingleOrDefaultAsync(b => b.Id == id);

            if (board == null)
                return NotFound();

            return View(board);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var board = await _database.Boards
                .Include(b => b.Parent)
                .Include(b => b.Results)
                .SingleOrDefaultAsync(b => b.Id == id);

            if (board == null)
                return NotFound();

            return View(board);
        }

        [Route("api/boards/{id}")]
        public async Task<IActionResult> GetBoard(string id)
        {
            var board = await _database.Boards.FindAsync(id);

            if (board == null)
                return NotFound();

            return Ok(board);
        }

        [HttpPost]
        [Route("api/boards/")]
        public async Task<IActionResult> PostBoard([FromBody] PostBoardViewModel model)
        {
            var board = new Board
            {
                ParentId = model.ParentId,
                Link = model.Link,
                Maker = model.Maker,
                Title = model.Title,
                Cells = model.Cells
            };

            _database.Boards.Add(board);
            await _database.SaveChangesAsync();

            return Ok(new {board.Id});
        }

        [HttpPost]
        [Route("api/boards/{id}/results")]
        public async Task<IActionResult> PostResult(string id, [FromBody] PostResultViewModel model)
        {
            var board = await _database.Boards.FindAsync(id);

            if (board == null)
                return NotFound();

            var result = new Result
            {
                Board = board,
                Cells = model.Cells
            };

            _database.Results.Add(result);
            board.Results.Add(result);

            await _database.SaveChangesAsync();

            return Ok(new {result.Id});
        }
    }
}
