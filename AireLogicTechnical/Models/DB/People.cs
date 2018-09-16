using System;
using System.Collections.Generic;

namespace AireLogicTechnical.Models.DB
{
    public partial class People
    {
        public People()
        {
            FavouriteColours = new HashSet<FavouriteColours>();
        }

        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAuthorised { get; set; }
        public bool IsValid { get; set; }
        public bool IsEnabled { get; set; }

        public ICollection<FavouriteColours> FavouriteColours { get; set; }
    }
}
