using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TaskBoard.Data.Entities;

namespace TaskBoard.Data.Entities
{
    public class Board
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        [BindNever]
        [ValidateNever]
        public User User { get; set; }
        [JsonIgnore]
        [BindNever]
        [ValidateNever]
        public ICollection<TaskItem> Tasks { get; set; }
    }
}



