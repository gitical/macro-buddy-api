using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using foodTrackerApi.Models;
using Microsoft.EntityFrameworkCore;
using foodTrackerApi.Controllers;

namespace foodTrackerApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FoodController : ControllerBase
    {
        private readonly FoodContext _ctx;
        private readonly ILogger<FoodController> _logger;
        public FoodController(ILogger<FoodController> logger, FoodContext ctx)
        {
            _logger = logger;
            _ctx = ctx;
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationFilter paginationFilter)
        {
            try
            {
                var queryResult = _ctx.Foods.Include(c => c.Category).AsNoTracking();
                var paginatedResult = await queryResult.Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize).Take(paginationFilter.PageSize).ToListAsync();
                var pagination = new Pagination(queryResult.Count(), paginationFilter);
                var response = new PaginatedResponse<IEnumerable<Food>>(paginatedResult, pagination);

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "error in " + e.TargetSite);
                return StatusCode(500);
            }
        }

        [Route("Search")]
        [HttpGet]
        public async Task<IActionResult> GetFoodBySearch(string q, [FromQuery] PaginationFilter paginationFilter)
        {
            try
            {
                var queryResult = _ctx.Foods.Include(c => c.Category).AsNoTracking().Where(f => f.Name.Contains(q));
                var paginatedResult = await queryResult.Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize).Take(paginationFilter.PageSize).ToListAsync();

                var pagination = new Pagination(queryResult.Count(), paginationFilter);
                var response = new PaginatedResponse<IEnumerable<Food>>(paginatedResult, pagination);

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "error in " + e.TargetSite);
                return StatusCode(500);
            }

        }

        [HttpGet("{foodId:int}")]
        public async Task<IActionResult> GetFoodById(int foodId)
        {
            try
            {

                var food = await _ctx.Foods.Include(c => c.Category).AsNoTracking().Where(f => f.Id == foodId).FirstOrDefaultAsync();

                if (food == null)
                    return NotFound();

                var response = new Response<Food>(food);

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "error in " + e.TargetSite);
                return StatusCode(500);
            }

        }

        [Route("Category")]
        [HttpGet]
        public async Task<IActionResult> GetFoodByCategoryId(int id, [FromQuery] PaginationFilter paginationFilter)
        {
            try
            {
                var queryResult = _ctx.Foods.Include(c => c.Category).AsNoTracking().Where(f => f.Category.Id == id);
                var paginatedResult = await queryResult.Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize).Take(paginationFilter.PageSize).ToListAsync();
                var pagination = new Pagination(queryResult.Count(), paginationFilter);
                var response = new PaginatedResponse<IEnumerable<Food>>(paginatedResult, pagination);

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "error in " + e.TargetSite);
                return StatusCode(500);
            }

        }

    }
}
