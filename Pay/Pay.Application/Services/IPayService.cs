using Pay.OvetimePolicies.Application.DTOs;

namespace Pay.OvetimePolicies.Application.Services
{
    public interface IPayService
    {
        Task<IEnumerable<PayDTO>> GetRangeAsync(PayFilterDTO filter);
        Task<PayDTO> GetPayAsync(int id);
        Task<int> CreatePayAsync(PayDTO pay);
        Task<PayDTO> UpdatePayAsync(PayDTO pay);
        Task<int> DeletePayAsync(int id);
    }



    /*
    Task<T> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<int> AddAsync(T entity);
    Task<int> UpdateAsync(T entity);
    Task<int> DeleteAsync(int id);
     */

}
