using Microsoft.Extensions.Logging;

namespace SiteVisitka.loggers
{
    public static class MylogEventId
    {
        public static EventId MyEventIdException = new(404, "MyException");
        public static EventId MyEventIdChangeDB = new(100, "MyChangeDB");
    }
}
