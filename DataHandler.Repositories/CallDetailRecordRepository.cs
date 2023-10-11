using DataHandler.CdrDbContext;
using DataHandler.Entities;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace DataHandler.Repositories
{
    public interface ICallDetailRecordRepository
    {
        Task ProcessAndSaveBatch(List<CallDetailRecord> batch);
    }
    public class CallDetailRecordRepository: ICallDetailRecordRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        public CallDetailRecordRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory) {
            _dbContextFactory = dbContextFactory;
        }

        public async Task ProcessAndSaveBatch(List<CallDetailRecord> batch)
        {
            using var context = _dbContextFactory.CreateDbContext();
            await context.BulkInsertAsync(batch);
            await context.BulkSaveChangesAsync();

        }
    }
}