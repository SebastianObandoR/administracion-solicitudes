using System;
using System.Collections.Generic;
using System.Text;

namespace AdministracionSolicitudes.Services.Models
{
    public class ServiceResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; }
    }
}