using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPrueba.Models
{
    public class ReportDetail
    {
        public StudentDetail Student { get; set; }
        public SubjectDetail Subject { get; set; }
        public string Grade { get; set; }
    }
}
