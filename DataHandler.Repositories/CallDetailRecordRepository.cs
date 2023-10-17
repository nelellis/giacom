using DataHandler.CdrDbContext;
using DataHandler.Entities;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace DataHandler.Repositories
{
    public interface ICallDetailRecordRepository
    {
        Task ProcessAndSaveBatch(List<CallDetailRecord> batch);
        IQueryable<CallDetailRecord> GetQueriable();


    }
    public class CallDetailRecordRepository: ICallDetailRecordRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        private readonly ApplicationDbContext _applicationDbContext;
        public CallDetailRecordRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory, ApplicationDbContext applicationDbContext = null)
        {
            _dbContextFactory = dbContextFactory;
            _applicationDbContext = applicationDbContext;
        }

        public async Task ProcessAndSaveBatch(List<CallDetailRecord> batch)
        {
            using var context = _dbContextFactory.CreateDbContext();
            await context.BulkInsertAsync(batch);
            await context.BulkSaveChangesAsync();

        }

        public IQueryable<CallDetailRecord> GetQueriable()
        {
            return _applicationDbContext.CallDetailRecords.AsNoTracking();
        }
    }
}