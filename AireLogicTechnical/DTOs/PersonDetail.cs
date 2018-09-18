using AireLogicTechnical.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AireLogicTechnical.DTOs
{
    public class PersonDetail
    {
        public People Person { get; set; }
        public List<SelectedColour> Colours { get; set; }
    }
}
