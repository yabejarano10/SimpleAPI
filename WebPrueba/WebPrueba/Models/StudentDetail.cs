using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebPrueba.Models
{
    public class StudentDetail
    {
        [Key]
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [Required]
        [Column(TypeName ="nvarchar(50)")]
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        [JsonPropertyName("StudentSubjects")]
        public virtual IEnumerable<StudentSubject> StudentSubjects {get; set;}
    }
}
