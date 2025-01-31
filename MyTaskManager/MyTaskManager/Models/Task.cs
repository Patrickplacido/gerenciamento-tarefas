using System.ComponentModel.DataAnnotations;

namespace MyTaskManager.Models
{
    public class Task
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data de vencimento é obrigatória.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Vencimento")]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "O status é obrigatório.")]
        public string Status { get; set; } = "Pendente";

        public string UserId { get; set; } = string.Empty;
    }
}
