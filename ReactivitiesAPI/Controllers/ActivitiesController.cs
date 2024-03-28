using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reactivities.Persistence;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ReactivitiesAPI.Controllers
{

    public class ActivitiesController : BaseApiController
    {
        private readonly DataContext _dataContext;
        public ActivitiesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetActivities()
        {
            return Ok(await _dataContext.Activities.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivityById(Guid id)
        {
            return Ok(await _dataContext.Activities.FindAsync(id));
        }
        
    }
}
