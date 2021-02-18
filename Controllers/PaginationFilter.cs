using System;
using System.Collections.Generic;
using foodTrackerApi.Models;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace foodTrackerApi.Controllers
{
    public class PaginationFilter
    {

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public PaginationFilter()
        {
            PageSize = 50;
            PageNumber = 1;
        }

    }
}