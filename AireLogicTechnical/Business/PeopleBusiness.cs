using AireLogicTechnical.DAL;
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

        public async Task<People> GetPerson(int personId)
        {
            return await peopleDAL.GetPersonByIdAsync(personId);
        }

        public async Task UpdatePerson(People person)
        {
            var colours = person.FavouriteColours;
            await peopleDAL.UpdateColours(person.PersonId, colours.ToList());

            await peopleDAL.UpdatePerson(person);
        }
    }
}
