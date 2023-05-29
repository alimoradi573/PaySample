using Pay.OvetimePolicies.Application.DTOs;

namespace Pay.OvetimePolicies.Application.Services
{
    public interface ICalculator
    {
        long OverTimeCalculator(PayDTO payDTO);
        
    }
}
