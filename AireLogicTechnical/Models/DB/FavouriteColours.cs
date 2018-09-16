using System;
using System.Collections.Generic;

namespace AireLogicTechnical.Models.DB
{
    public partial class FavouriteColours
    {
        public int PersonId { get; set; }
        public int ColourId { get; set; }

        public Colours Colour { get; set; }
        public People Person { get; set; }
    }
}
