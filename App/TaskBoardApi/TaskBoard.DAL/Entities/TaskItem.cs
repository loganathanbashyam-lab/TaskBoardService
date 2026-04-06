using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TaskBoard.Data.Entities;

namespace TaskBoard.Data.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string AssignedTo { get; set; }
        public string Owner { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public int BoardId { get; set; }
        [JsonIgnore]
        [BindNever]
        [ValidateNever]
        public Board Board { get; set; }
    }
}