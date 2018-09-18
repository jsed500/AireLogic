using AireLogicTechnical.DAL;
using AireLogicTechnical.DTOs;
using AireLogicTechnical.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AireLogicTechnical.Business
{
    public class PeopleBusiness
    {
        private readonly PeopleDAL peopleDAL;

        public PeopleBusiness(TechTestContext context)
        {
            peopleDAL = new PeopleDAL(context);
        }

        public async Task<List<People>> GetAllPeople()
        {
            return await peopleDAL.GetAllPeopleAsync();
        }

        public async Task<PersonDetail> GetPersonDetail(int personId)
        {
            var personDetail = new PersonDetail()
            {
                Person = await peopleDAL.GetPersonByIdAsync(personId),
                Colours = new List<SelectedColour>()
            };

            var allColours = await peopleDAL.GetAllEnabledColours();

            foreach (var colour in allColours)
            {
                SelectedColour selectedColour = new SelectedColour()
                {
                    ColourId = colour.ColourId,
                    IsEnabled = colour.IsEnabled,
                    Name = colour.Name,
                    Hidden = false //show all at start by default
                };

                selectedColour.IsSelected = personDetail.Person.FavouriteColours.Select(x => x.ColourId).Contains(colour.ColourId);

                personDetail.Colours.Add(selectedColour);
            }

            return personDetail;
        }

        public async Task UpdatePerson(PersonDetail personDetail)
        {
            //process favourite colours
            personDetail.Person.FavouriteColours = new List<FavouriteColours>();

            foreach (var colour in personDetail.Colours)
            {
                if (colour.IsSelected)
                {
                    personDetail.Person.FavouriteColours.Add(new FavouriteColours()
                    {
                        ColourId = colour.ColourId,
                        PersonId = personDetail.Person.PersonId
                    });
                }
            }

            await peopleDAL.UpdatePerson(personDetail.Person);
            await peopleDAL.UpdateColours(personDetail.Person.PersonId, personDetail.Person.FavouriteColours.ToList());
        }
    }
}
