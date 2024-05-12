using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiClient
{
    public class Weather
    {
        public Location location { get; set; }
        public Current current { get; set; }
    }
}
