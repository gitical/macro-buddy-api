using System;
using System.Collections.Generic;
using foodTrackerApi.Models;
using System.Linq;

namespace foodTrackerApi.Controllers
{
    public class PaginatedResponse<T> : Response<T>
    {

        public Pagination Paging { get; set; }


        public PaginatedResponse(T responseData, Pagination paging) : base(responseData)
        {
            Paging = paging;

        }

    }

}