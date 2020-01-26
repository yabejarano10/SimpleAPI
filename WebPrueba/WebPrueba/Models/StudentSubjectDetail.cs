using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPrueba.Models
{
    public class StudentSubjectDetail
    {
        public StudentDetail Student { get; set; }
        public ICollection<SubjectDetail> Subjects { get; set; }    
    }
}
