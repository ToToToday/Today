using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class ProgramCantUseDate
    {
        public int ProgramDateId { get; set; }
        public DateTime Date { get; set; }
        public int ProgramId { get; set; }

        public virtual Program Program { get; set; }
    }
}
