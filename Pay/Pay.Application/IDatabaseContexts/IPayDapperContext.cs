using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;

namespace Pay.OvetimePolicies.Persistence.DatabaseContexts
{
    public interface IPayDapperContext
    {
        DbConnection GetDbconnection();
    }
}
