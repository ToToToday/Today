using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class AboutProgram
    {
        public int AboutProgramId { get; set; }
        public int ProgramId { get; set; }
        public int AboutProgramOptionsId { get; set; }

        public virtual AboutProgramOption AboutProgramOptions { get; set; }
        public virtual Program Program { get; set; }
    }
}
