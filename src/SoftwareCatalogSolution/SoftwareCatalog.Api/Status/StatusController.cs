using Microsoft.AspNetCore.Mvc;

namespace SoftwareCatalog.Api.Status;


// Have to be public classes. And they have to extend* ControllerBase
public class StatusController(TimeProvider systemTime) : ControllerBase
{
    //private IProvideTheSystemTime _systemTime;

    //public StatusController(IProvideTheSystemTime systemTime)
    //{
    //    _systemTime = systemTime;
    //}


    // GET /status
    [HttpGet("/status")]
    public ActionResult GetTheStatus()
    {
        // 
        // this is fake...
        var response = new StatusResponse(systemTime.GetUtcNow(), "Looks Good!");
        return Ok(response);
    }
}

//public interface IProvideTheSystemTime
//{
//    DateTimeOffset GetSystemTime();
//}

//public class RealSystemTime : IProvideTheSystemTime
//{
//    public DateTimeOffset GetSystemTime()
//    {
//        return DateTimeOffset.Now;
//    }
//}

public record StatusResponse(DateTimeOffset LastChecked, string Message);