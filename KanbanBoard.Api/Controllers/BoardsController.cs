/*This controller handles HTTP requests related to the Board model, 
providing endpoints for CRUD operations.*/

using KanbanBoard.Api.Data;
using KanbanBoard.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KanbanBoard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardsController : ControllerBase
    {
        private readonly KanbanBoardContext _context;

        // Constructor that accepts the DbContext via dependency injection
        public BoardsController(KanbanBoardContext context)
        {
            _context = context;
        }

        // GET: api/Boards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Board>>> GetBoards()
        {
            // Retrieve all boards from the database asynchronously
            return await _context.Boards.ToListAsync();
        }
        

        // GET: api/Boards/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Board>> GetBoard(int id)
        {
            // Retrieve a specific board by ID
            var board = await _context.Boards.FindAsync(id);

            if (board == null)
            {
                return NotFound();
            }

            return board;
        }

        // POST: api/Boards
        [HttpPost]
        public async Task<ActionResult<Board>> PostBoard(Board board)
        {
            // Add a new board to the database
            _context.Boards.Add(board);
            await _context.SaveChangesAsync();

            // Return the created board with the appropriate status code
            return CreatedAtAction(nameof(GetBoard), new { id = board.Id }, board);
        }

        // PUT: api/Boards/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoard(int id, Board board)
        {
            if (id != board.Id)
            {
                return BadRequest();
            }

            // Mark the board as modified
            _context.Entry(board).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoardExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Boards/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoard(int id)
        {
            // Find the board by ID
            var board = await _context.Boards.FindAsync(id);
            if (board == null)
            {
                return NotFound();
            }

            // Remove the board from the database
            _context.Boards.Remove(board);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Helper method to check if a board exists by ID
        private bool BoardExists(int id)
        {
            return _context.Boards.Any(e => e.Id == id);
        }
    }
}
