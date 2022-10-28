using Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.PostVM
{
    public class SavePostViewModel
    {
        public int Id { get; set; }
        public string? Tittle { get; set; }
        public string? Body { get; set; }
        public string? ImagePost { get; set; }
        public int UserId { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }

    }
}
