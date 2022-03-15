using Nis.Core.Models.Diagnosis;
using System.Collections.Generic;

namespace Nis.Core.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Diagnose> Diagnoses{ get; set; } = new List<Diagnose>();
    }
}
