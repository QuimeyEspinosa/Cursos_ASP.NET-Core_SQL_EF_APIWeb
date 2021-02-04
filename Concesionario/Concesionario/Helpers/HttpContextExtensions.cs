using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Concesionario.Helpers
{
    public static class HttpContextExtensions
    {
        public static async Task InsertAnswerPaginationParams<T>(this HttpContext context, IQueryable<T> queryable, int ItemsToShow)
        {
            if(context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / ItemsToShow);

            context.Response.Headers.Add("totalPages", totalPages.ToString());
                        
        }
    }
}
