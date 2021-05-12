using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteVisitka.loggers
{
    public static class MylogEventId
    {
        public static EventId MyEventIdException = new(404, "MyException");
        public static EventId MyEventIdChangeDB  = new(100, "MyChangeDB");
    }
}
