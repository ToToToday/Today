using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class Program
    {
        public Program()
        {
            ProgramCantUseDates = new HashSet<ProgramCantUseDate>();
            ProgramSpecifications = new HashSet<ProgramSpecification>();
        }

        public int ProgramId { get; set; }
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Context { get; set; }
        public int Status { get; set; }

        public virtual Product Product { get; set; }
        public virtual ICollection<ProgramCantUseDate> ProgramCantUseDates { get; set; }
        public virtual ICollection<ProgramSpecification> ProgramSpecifications { get; set; }
    }
}
