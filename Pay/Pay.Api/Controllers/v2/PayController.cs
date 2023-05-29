using Microsoft.AspNetCore.Mvc;

namespace Pay.OvetimePolicies.Api.Controllers.v2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class PayController : v1.PayController
    {
        [MapToApiVersion("2.0")]
        [HttpGet]
        public override string Get() => ".Net Core Web API Version 2";
    }
}


