using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TaskBoard.Data.Entities;

namespace TaskBoard.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        [JsonIgnore]
        [BindNever]
        [ValidateNever]
        public ICollection<Board>? Boards { get; set; }
    }
}