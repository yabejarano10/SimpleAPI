using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebPrueba.Models
{
    public class SubjectDetail
    {

        [Key]
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        [JsonPropertyName("SubjectName")]
        public string SubjectName { get; set; }
        [Required]
        [JsonPropertyName("Teacher")]
        public virtual TeacherDetail Teacher { get; set; }
        [JsonPropertyName("StudentSubjects")]
        public IEnumerable<StudentSubject> StudentSubjects { get; set; }
    }
}
