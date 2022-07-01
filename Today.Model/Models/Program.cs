using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class Program
    {
        public Program()
        {
            AboutPrograms = new HashSet<AboutProgram>();
            ProgramCantUseDates = new HashSet<ProgramCantUseDate>();
            ProgramIncludes = new HashSet<ProgramInclude>();
            ProgramSpecifications = new HashSet<ProgramSpecification>();
        }

        public int ProgramId { get; set; }
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Context { get; set; }
        public bool Isdeleted { get; set; }

        public virtual Product Product { get; set; }
        public virtual ICollection<AboutProgram> AboutPrograms { get; set; }
        public virtual ICollection<ProgramCantUseDate> ProgramCantUseDates { get; set; }
        public virtual ICollection<ProgramInclude> ProgramIncludes { get; set; }
        public virtual ICollection<ProgramSpecification> ProgramSpecifications { get; set; }
    }
}
