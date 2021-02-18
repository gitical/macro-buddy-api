using System;
using System.Collections.Generic;
using foodTrackerApi.Models;
using System.Linq;

namespace foodTrackerApi.Controllers
{
    public class Pagination
    {
        public int TotalRows { get; }
        public double TotalPages
        {
            get
            {
                double pageCount = ((double)TotalRows / (double)PageSize);
                return Math.Ceiling(pageCount);
            }
        }
        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public Pagination(int totalRows, PaginationFilter paginationFilter)
        {
            TotalRows = totalRows;
            PageNumber = paginationFilter.PageNumber;
            PageSize = paginationFilter.PageSize;
        }

    }

}