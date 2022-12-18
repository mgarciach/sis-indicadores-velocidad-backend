using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSIPTEL.Common.Layer
{
    public class PaginateResponse<T> where T : class
    {
        public List<T> Items { get; set; }

        public int Total { get; set; }

        public PaginateResponse()
        {
            Items = new List<T>();
        }
    }
}
