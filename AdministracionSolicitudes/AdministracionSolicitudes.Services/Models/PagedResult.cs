using System;
using System.Collections.Generic;
using System.Text;

namespace AdministracionSolicitudes.Services.Models
{
    public class PagedResult<T>
    {
        public IReadOnlyList<T> Items { get; set; } = new List<T>();
        public int Page { get; set; } = 1;
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; } = 1;
    }
}