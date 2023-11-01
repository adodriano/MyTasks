using System.ComponentModel.DataAnnotations;

namespace MyTasks.ViewModels
{
    public class CreateToDoViewModel
    {
        [Required]
        public string Title { get; set; }
    }
}
