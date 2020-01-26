using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebPrueba.Models
{
    public class TeacherDetail
    {
        [Key]
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [JsonPropertyName("Name")]
        public string Name { get; set; }

    }
}
