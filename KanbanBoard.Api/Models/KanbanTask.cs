namespace KanbanBoard.Api.Models
{
    public class KanbanTask
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int BoardId { get; set; } // Foreign key to associate the task with a board
        public Board? Board { get; set; }
    }
}