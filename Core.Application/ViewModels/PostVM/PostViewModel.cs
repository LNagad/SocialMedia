using Core.Application.ViewModels.UserVM;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.PostVM
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string? Tittle { get; set; }
        public string? Body { get; set; }
        public string? ImagePost { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string? Created { get; set; }
        public string? LastModified { get; set; }
        public ICollection<Comments>? Comments { get; set; }
    }
}
