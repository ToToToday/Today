using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class ProgramDatePicker
    {
        public int ProgramDateId { get; set; }
        public string DatimePickerConfigurationJson { get; set; }
        public int ProgramId { get; set; }

        public virtual Program Program { get; set; }
    }
}
