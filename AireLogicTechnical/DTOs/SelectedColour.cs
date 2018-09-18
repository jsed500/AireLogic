using AireLogicTechnical.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AireLogicTechnical.DTOs
{
    public class SelectedColour : Colours
    {
        public bool IsSelected { get; set; }
        public bool Hidden { get; set; }
    }
}
