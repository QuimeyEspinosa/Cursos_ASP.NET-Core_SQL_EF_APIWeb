using Concesionario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Concesionario.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable, Paginacion p)
        {
            return queryable
                .Skip((p.Page - 1) * p.ItemsToShow)
                .Take(p.ItemsToShow);
        }
    }
}
