using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pay.OvetimePolicies.Domain.Enums
{
    public class Types
    {
        public enum ObjectState
        {
            Unchanged,
            Added,
            Modified,
            Deleted,
            Detached
        }
    }
}
