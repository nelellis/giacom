using DataHandler.CdrDbContext;
using DataHandler.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace DataHandler.Services.Tests
{
    [TestClass]
    public class CallDetailRecordServiceTests
    {
        private ICallDetailRecordService _callDetailRecordService;                                                          
        private Mock<ILogger<CallDetailRecordService>> mockServiceLogger;
        private Mock<IDbContextFactory<ApplicationDbContext>> mockDbFactory;
        private Mock<ICallDetailRecordRepository> mockRepo;

        private string validFilePath;
        private string invalidFilePath;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

            mockDbFactory = new Mock<IDbContextFactory<ApplicationDbContext>>();
            mockDbFactory.Setup(f => f.CreateDbContext())
                .Returns(new ApplicationDbContext(options));

            mockServiceLogger = new Mock<ILogger<CallDetailRecordService>>();
            mockRepo = new Mock<ICallDetailRecordRepository>();

            var directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            validFilePath = System.IO.Path.Combine(directory, "Files", "valid-4-records.csv");
            invalidFilePath = System.IO.Path.Combine(directory, "Files", "valid-4-and-2-invalid.csv");

            _callDetailRecordService = new CallDetailRecordService(mockDbFactory.Object, mockServiceLogger.Object, mockRepo.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Dispose and clean up resources if needed
            mockServiceLogger.Reset();
            mockDbFactory.Reset();
            mockRepo.Reset();
        }

        [TestMethod]
        public async Task Import_ValidFile_Success()
        {
            using var fileStream = File.OpenText(validFilePath);

            var result = await _callDetailRecordService.ImportFile(fileStream);

            Assert.AreEqual("4 records are imported and 0 records are ignored.", result);
        }
    }
}