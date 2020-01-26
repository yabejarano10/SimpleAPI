using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebPrueba.Models
{
    public class StudentSubject
    {

        [JsonPropertyName("StudentId")]
        public int StudentId { get; set; }

        [JsonPropertyName("Student")]
        public StudentDetail Student { get; set; }

        [JsonPropertyName("SubjectId")]
        public int SubjectId { get; set; }

        [JsonPropertyName("Subject")]
        public SubjectDetail Subject { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(3)")]
        [JsonPropertyName("Grade")]
        public string Grade { get; set; }
    }
}
