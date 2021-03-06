﻿using AireLogicTechnical.Business;
using AireLogicTechnical.Models.DB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AireLogicTechnical.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly PeopleBusiness peopleBusiness;

        public PeopleController(TechTestContext context)
        {
            peopleBusiness = new PeopleBusiness(context);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<People>>> GetAllPeople()
        {
            try
            {
                var peopleList = await peopleBusiness.GetAllPeople();
                return new OkObjectResult(peopleList);
            }
            catch (Exception ex)
            {
                //log error to some logging system

                //return error
                return StatusCode(500);
            }
        }

        [HttpGet("GetPerson")]
        public async Task<ActionResult<People>> GetPerson(int id)
        {
            try
            {
                var person = await peopleBusiness.GetPerson(id);
                return new OkObjectResult(person);
            }
            catch (Exception ex)
            {
                //log error to some logging system

                //return error
                return StatusCode(500);
            }
        }

        [HttpPut("UpdatePerson")]
        public async Task<ActionResult<People>> UpdatePerson(People person)
        {
            try
            {
                await peopleBusiness.UpdatePerson(person);
                var updatedPerson = await peopleBusiness.GetPerson(person.PersonId);
                return new OkObjectResult(updatedPerson);
            }
            catch (Exception ex)
            {
                //log error to some logging system

                //return error
                return StatusCode(500);
            }
        }

    }

 
}
