using AutoMapper;
using Dapper;
using Pay.OvetimePolicies.Application.DTOs;
using Pay.OvetimePolicies.Application.IDatabaseContexts;
using Pay.OvetimePolicies.Domain.Entities;
using Pay.OvetimePolicies.Persistence.DatabaseContexts;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Pay.OvetimePolicies.Application.Services
{
    public class PayService : IPayService
    {
        private readonly IPayDbContext _payDbContext;
        private readonly IMapper _mapper;
        private readonly IPayDapperContext _payDapperContext;

        public PayService(IPayDbContext payDbContext, IMapper mapper, IPayDapperContext payDapperContext)
        {
            this._payDbContext = payDbContext;
            this._mapper = mapper;
            this._payDapperContext = payDapperContext;
        }
        public async Task<int> CreatePayAsync(PayDTO payDTO)
        {
            _payDbContext.Pays.Add(_mapper.Map<PayEntity>(payDTO));
            return await _payDbContext.SaveChangesAsync();
        }
        public async Task DeletePayAsync(int id)
        {
            var result = await _payDbContext.Pays.FindAsync(id);
            if (result != null)
            {
                _payDbContext.Pays.Remove(result);
                await _payDbContext.SaveChangesAsync();
            }
            await Task.CompletedTask;
        }
        //public async Task<IEnumerable<PayDTO>> GetRangeAsync(PayFilterDTO filter)
        //{
        //    IQueryable<PayEntity> result = ExecuteQuery(filter);
        //    return await Task.FromResult(_mapper.Map<List<PayDTO>>(result.ToList()));
        //}
        public async Task<IEnumerable<PayDTO>> GetRangeAsync(PayFilterDTO filter)
        {
            var query = $"SELECT * FROM Pays WHERE Date>='{filter.FromDate}' and Date<'{filter.ToDate}'";
            using (var connection = _payDapperContext.GetDbconnection())
            {
                return await connection.QueryAsync<PayDTO>(query);
            }
        }

        private IQueryable<PayEntity> ExecuteQuery(PayFilterDTO filter)
        {
            var query = _payDbContext.Pays.AsQueryable();
            if (string.IsNullOrEmpty(filter.FirstName))
            {
                query = query.Where(c => c.FirstName.Contains(filter.FirstName));
            }
            if (string.IsNullOrEmpty(filter.LastName))
            {
                query = query.Where(c => c.LastName.Contains(filter.LastName));
            }
            if (filter.FromDate != DateTime.MinValue)
            {
                query = query.Where(c => c.Date >= filter.FromDate);
            }
            if (filter.ToDate != DateTime.MinValue)
            {
                query = query.Where(c => c.Date < filter.ToDate);
            }

            return query;
        }

        //public async Task<PayDTO> GetPayAsync(int id)
        //{
        //    var result = await Task.FromResult(_payDbContext.Pays.FirstOrDefault(item => item.Id == id));
        //    return _mapper.Map<PayDTO>(result);
        //}

        public async Task<PayDTO> GetPayAsync(int id)
        {
            var query = $"SELECT * FROM Pays WHERE id={id}";
            using (var connection = _payDapperContext.GetDbconnection())
            {
                return  await connection.QueryFirstOrDefaultAsync<PayDTO>(query);
            }
        }



        public async Task UpdatePayAsync(PayDTO item)
        {
            var result = _payDbContext.Pays.Find(item.Id);
            if (result != null)
            {
                _mapper.Map(item, result);
                _payDbContext.Pays.Update(result);
                _payDbContext.SaveChanges();
            }
            await Task.CompletedTask;
        }

        

        

    }
}
