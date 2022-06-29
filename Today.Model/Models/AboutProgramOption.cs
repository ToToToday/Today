using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class AboutProgramOption
    {
        public AboutProgramOption()
        {
            AboutPrograms = new HashSet<AboutProgram>();
        }

        public int AboutProgramOptionsId { get; set; }
        public string Context { get; set; }
        public string IconClass { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
        public virtual ICollection<AboutProgram> AboutPrograms { get; set; }
    }
}
