using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using System.IO;
using DataHandler.CdrDbContext;
using DataHandler.Entities;
using DataHandler.CsvMapper;
using EFCore.BulkExtensions;
using DataHandler.Validators;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Index.HPRtree;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using DataHandler.Repositories;
using System.ComponentModel.DataAnnotations;

namespace DataHandler.Services
{
    public interface ICallDetailRecordService
    {
        Task<string> ImportFile(string filePath);

        Task<string> ImportFile(StreamReader fileStream);
    }
    public class CallDetailRecordService: ICallDetailRecordService
    {
        
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        private readonly ILogger<CallDetailRecordService> _logger;
        private readonly ICallDetailRecordRepository _callDetailRecordRepo;
        public CallDetailRecordService(IDbContextFactory<ApplicationDbContext> dbContextFactory, 
            ILogger<CallDetailRecordService> logger,
            ICallDetailRecordRepository callDetailRecordRepo
            )
        {
            _dbContextFactory = dbContextFactory;
            _logger = logger;
            _callDetailRecordRepo= callDetailRecordRepo;
        }

        public async Task<string> ImportFile(string filePath)
        {
            
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("filePath is not provided");
            }
            if (!File.Exists(filePath))
            {
                throw new ArgumentException("Invalid filePath is provided");
            }
            
            try
            {
                using var fileStream = File.OpenText(filePath);

                return await ImportFile(fileStream);
            }
            catch(Exception ex)
            {
                _logger.LogError($"{ex.Message} {ex.StackTrace}");
                throw;
            }
        }

        public async Task<string> ImportFile(StreamReader fileStream)
        {
            _logger.LogTrace("Reading CSV Records");
            using var csvReader = new CsvReader(fileStream, new CsvConfiguration(CultureInfo.InvariantCulture));
            csvReader.Context.RegisterClassMap(new CsvCallDetailRecordMap());

            int batchSize = 5000;

            var batch = new List<CallDetailRecord>(batchSize);
            var allRecords = csvReader.GetRecords<CallDetailRecord>().ToList();
            var validRecords = new List<CallDetailRecord>();
            var invalidRecords = new List<CallDetailRecord>();

            _logger.LogTrace("validating CSV Records");
            CallDetailRecordValidator validator = new CallDetailRecordValidator();
            var tasks = allRecords.Select(t => ValidateCallRecord(t, validator, validRecords, invalidRecords)).ToList();
            await Task.WhenAll(tasks);

            _logger.LogTrace($"Creating batches of valid records");
            var recordBatches = Enumerable.Range(0, (validRecords.Count + batchSize - 1) / batchSize)
                .Select(i => validRecords.Skip(i * batchSize).Take(batchSize).ToList())
                .ToList();

            _logger.LogTrace($"Inserting {validRecords.Count} Records");
            await Parallel.ForEachAsync(recordBatches, async (batch, cancellationToken) =>
            {
                await _callDetailRecordRepo.ProcessAndSaveBatch(batch);
            });
            _logger.LogTrace($"Imported Records");
            return $"{validRecords.Count} records are imported and {invalidRecords.Count} records are ignored.";
        }

        private async Task ValidateCallRecord(CallDetailRecord item, CallDetailRecordValidator validator, List<CallDetailRecord> validRecords, List<CallDetailRecord> inValidRecords)
        {
            var validationResult = await validator.ValidateAsync(item);
            if (validationResult.IsValid)
            {
                validRecords.Add(item);
            }
            else
            {
                // Handle invalid item, e.g., log it or take appropriate action
                _logger.LogError($"Call Record {item.Reference} is not valid.");
                inValidRecords.Add(item);
            }
        }
    }
}