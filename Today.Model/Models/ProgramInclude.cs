using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class ProgramInclude
    {
        public int ProgramIncludeId { get; set; }
        public int ProgramId { get; set; }
        public string Text { get; set; }
        public bool? IsInclude { get; set; }

        public virtual Program Program { get; set; }
    }
}
