using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace WebApplication1.Models
{
    public class Document
    {
        public int Id {  get; set; }

        [Display(Name = "Document subject")]
        public string DocSubject { get; set; }

        [Display(Name = "Document title")]
        public string? DocTitle { get; set; }

        public string EmployeeName { get; set; }

        public DateTime DocTime { get; set; } = DateTime.Now;
    }
}
