using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.FriendsVM
{
    public class SaveFriendsViewModel
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? FriendId { get; set; }

        [Required(ErrorMessage = "Debe incluir el nombre de usuario")]
        public string UserName { get; set; }
    }
}
