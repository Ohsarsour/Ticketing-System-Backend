/*his file defines the KanbanBoardContext, 
which is the DbContext for Entity Framework Core. 
It manages the connection to the database and the Boards table.*/

using Microsoft.EntityFrameworkCore;
using KanbanBoard.Api.Models;

namespace KanbanBoard.Api.Data
{
    public class KanbanBoardContext : DbContext
    {
        // Constructor that accepts DbContextOptions to configure the context
        public KanbanBoardContext(DbContextOptions<KanbanBoardContext> options) : base(options) { }

        // DbSet representing the Boards table in the database
        public DbSet<Board> Boards { get; set; }
        public DbSet<KanbanTask> Tasks { get; set; }
    }
}
