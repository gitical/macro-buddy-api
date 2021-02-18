using System;
using System.Collections.Generic;
using foodTrackerApi.Models;
using System.Linq;

namespace foodTrackerApi.Controllers
{
    public class Response<T>
    {
        public virtual T Data { get; set; }

        public Response(T responseData)
        {
            Data = responseData;
        }

    }



}