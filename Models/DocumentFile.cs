using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class DocumentFile
    {
        public int Id { get; set; }
        public int DocumentID { get; set; }
        [ForeignKey("DocumentID")]
        public string? FileName { get; set; }
        [Display(Name = "Uploader")]
        public string FileUploader { get; set; }

        public DateTime FileTime { get; set; } = DateTime.Now;
        public byte[]? FileContent { get; set; }
        [NotMapped]
        [Display(Name = "Upload file")]
        public IFormFile FormFile { get; set; }

        public string? FileMIMEType { get; set; }
    }
}
