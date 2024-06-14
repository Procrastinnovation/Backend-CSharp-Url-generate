using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JLimUrl
{
    public class ShortenedUrl
    {
        public Guid Id {get; set;}

        public string RealUrl {get; set;} = string.Empty;
        public string ShortUrl {get; set;} = string.Empty;
        public string Code {get; set;} = string.Empty;
        public DateTime CreatedOnUtc {get; set;}
        

    }
}