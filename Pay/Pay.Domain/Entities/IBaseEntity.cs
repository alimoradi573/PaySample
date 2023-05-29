using static Pay.OvetimePolicies.Domain.Enums.Types;

namespace Pay.OvetimePolicies.Domain.Entities
{
    public class IBaseEntity<TKey>// : IObjectState
    {
        TKey Id { get; set; }
        // public ObjectState ObjectState { get;set; }
    }
    public interface IObjectState
    {
        ObjectState ObjectState { get; set; }
    }




}
