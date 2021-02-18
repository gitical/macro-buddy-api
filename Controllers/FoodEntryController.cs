using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using foodTrackerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace foodTrackerApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FoodEntryController : ControllerBase
    {
        private readonly FoodContext _ctx;
        private readonly ILogger<FoodEntryController> _logger;

        public FoodEntryController(ILogger<FoodEntryController> logger, FoodContext ctx)
        {
            _logger = logger;
            _ctx = ctx;
        }

        private string GetUserId()
        {
            var identity = User.Identity as System.Security.Claims.ClaimsIdentity;
            var userId = identity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;

            return userId;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] PaginationFilter paginationFilter)
        {

            try
            {
                var queryResult = _ctx.FoodEntries.Where(e => e.UserId == Guid.Parse(GetUserId())).Include(c => c.Food).AsNoTracking();
                var paginatedResult = queryResult.Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize).Take(paginationFilter.PageSize).ToList();

                var pagination = new Pagination(queryResult.Count(), paginationFilter);
                var response = new PaginatedResponse<IEnumerable<FoodEntry>>(paginatedResult, pagination);

                return Ok(response);
            }
            catch (Exception e)
            {

                _logger.LogError(e, "error in " + e.TargetSite);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult AddEntry(FoodEntryRequest entry)
        {
            try
            {
                var food = _ctx.Foods.Where(f => f.Id == entry.FoodId).FirstOrDefault();
                var foodEntry = new FoodEntry { Food = food, Timestamp = DateTime.Now, Amount = entry.Amount, UserId = Guid.Parse(GetUserId()) };

                _ctx.FoodEntries.Add(foodEntry);
                _ctx.SaveChanges();

                return Created("api/foodentry", foodEntry);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "error in " + e.TargetSite);
                return StatusCode(500);
            }
        }

        [HttpPut]
        public IActionResult UpdateEntry(FoodEntry foodEntry)
        {
            try
            {
                //foodEntry.ModifiedDate = DateTime.Now;
                _ctx.FoodEntries.Update(foodEntry);
                _ctx.SaveChanges();

                return Accepted();
            }
            catch (Exception e)
            {

                _logger.LogError(e, "error in " + e.TargetSite);
                return StatusCode(500);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteEntry(int id)
        {
            try
            {
                var entry = _ctx.FoodEntries.Where(e => e.Id == id).FirstOrDefault();

                _ctx.FoodEntries.Remove(entry);
                _ctx.SaveChanges();

                return Accepted();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "error in " + e.TargetSite);
                return StatusCode(500);
            }
        }

    }
}