using Pay.OvetimePolicies.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pay.OvetimePolicies.Application.Services
{
    public class CalculatorA : ICalculator
    {
        public long OverTimeCalculator(PayDTO payDTO)
        {
            var pay= payDTO.BasicSalary + payDTO.Allowance + payDTO.Transportation - ((long)(0.1 * (payDTO.BasicSalary + payDTO.Allowance)));
           return pay;
        }
    }
}
