using AireLogicTechnical.Models.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AireLogicTechnical.DAL
{
    public class PeopleDAL
    {
        //DAL file, use for CRUD operations with People data
        private readonly TechTestContext _context;

        public PeopleDAL(TechTestContext context)
        {
            _context = context;
        }

        public async Task <List<People>> GetAllPeopleAsync()
        {
            return await _context.People.Include(x => x.FavouriteColours).ThenInclude(c => c.Colour).ToListAsync();
        }

        public async Task<People> GetPersonByIdAsync(int personId)
        {
            return await _context.People.Where(y => y.PersonId == personId).Include(x => x.FavouriteColours).ThenInclude(c => c.Colour).FirstOrDefaultAsync();
        }

        public async Task<List<Colours>> GetAllEnabledColours()
        {
            return await _context.Colours.Where(y => y.IsEnabled).ToListAsync();
        }

        public async Task<List<FavouriteColours>> GetColoursByPersonIdAsync(int personId)
        {
            return await _context.FavouriteColours.Where(y => y.PersonId == personId).ToListAsync();
        }

        public async Task UpdatePerson(People person)
        {
            try
            {
                //_context.Entry(person).State = EntityState.Modified;
                //_context.Entry(person).Collection(x => x.FavouriteColours).IsModified = false;
                _context.People.Update(person);

                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateColours(int personId, List<FavouriteColours> colours)
        {
            try
            {
                var originalColours = await GetColoursByPersonIdAsync(personId);

                //Delete children
                foreach (var existingChild in originalColours.ToList())
                {
                    if (!colours.Any(c => c.ColourId == existingChild.ColourId))
                    {
                        _context.FavouriteColours.Remove(existingChild);
                    }
                }

                //Insert children
                foreach (var colour in colours)
                {
                    var existingChild = originalColours
                        .Where(c => c.ColourId == colour.ColourId)
                        .SingleOrDefault();

                    //only insert if it didnt exist
                    if (existingChild == null)
                    {
                        //Insert
                        _context.FavouriteColours.Add(colour);
                    }
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
