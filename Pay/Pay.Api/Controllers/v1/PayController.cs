using Microsoft.AspNetCore.Mvc;
using Pay.OvetimePolicies.Api.Models;
using Pay.OvetimePolicies.Application.DTOs;
using Pay.OvetimePolicies.Application.Services;

namespace Pay.OvetimePolicies.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class PayController : ControllerBase
    {
        private readonly IPayService _payService;

        public PayController(IPayService payService)
        {
            this._payService = payService;
        }
        [HttpGet]
        [Route("[action]")]
        public virtual string Alive() => ".Net Core Web API Version 1";

        [HttpPost]
        [Route("{datatype}")]
        public virtual async Task<IActionResult> Calculate(string datatype, PayDTO payDTO)
        {
            var result = await _payService.CreatePayAsync(payDTO);
            return Ok(result);
        }

        
        [HttpPost]
        [Route("action")]
        public virtual async Task<IActionResult> Calculate2([ModelBinder(binderType: typeof(SeparatorModelBinder))] PayDTO payDTO)//, string datatype
        {
            /*
             {"data": "0/string/string/0/0/0/2023-05-29"}
             */
            var result = await _payService.CreatePayAsync(payDTO);
            return Ok(result);
        }


        [HttpPut]
        [Route("[action]")]
        public virtual async Task Update(PayDTO payDTO)
        {
            await _payService.UpdatePayAsync(payDTO);
        }
        [HttpDelete]
        [Route("[action]")]
        public virtual async Task Delete(int id)
        {
             await _payService.DeletePayAsync(id);
        }
        [HttpGet]
        [Route("[action]")]
        public virtual async Task<IActionResult> GetRangeAsync([FromQuery] PayFilterDTO filter)
        {
            var result = await _payService.GetRangeAsync(filter);
            return Ok(result);
        }
        [HttpGet]
        [Route("[action]")]
        public virtual async Task<IActionResult> Get(int id)
        {
            var result = await _payService.GetPayAsync(id);
            return Ok(result);
        }
    }
}
