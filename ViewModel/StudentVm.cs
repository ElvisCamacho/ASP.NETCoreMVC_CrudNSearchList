using CrudOperation.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CrudOperation.ViewModel
{
    public class StudentVm
    {
        public Dictionary<int, Student> Students { get; set; }
        
        public int TotalCount { get; set; }


        // for Upload the image
        
        [Range(0, 1000)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name of Event is required"), MaxLength(30)]
        public string Name { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Image is required")]
        [Display(Name = "Choose Image")]
        public IFormFile UploadImage { get; set; }


        [Required(ErrorMessage = "Profession is required")]
        public Profession Profession { get; set; }
    }



}
