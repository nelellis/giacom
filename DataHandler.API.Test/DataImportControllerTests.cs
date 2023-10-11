using DataHandler.API.Controllers;
using DataHandler.CdrDbContext;
using DataHandler.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataHandler.Repositories;

namespace DataHandler.API.Test
{
    [TestClass]
    public class DataImportControllerTests
    {
        private ICallDetailRecordService _callDetailRecordService;
        private IFileService _fileService;
        private DataImportController controller;
        private Mock<ILogger<CallDetailRecordService>> mockServiceLogger;
        private Mock<IDbContextFactory<ApplicationDbContext>> mockDbFactory;
        private Mock<ICallDetailRecordRepository> mockRepo;

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

            _fileService = new FileService();
            _callDetailRecordService = new CallDetailRecordService(mockDbFactory.Object, mockServiceLogger.Object, mockRepo.Object);
            controller = new DataImportController(_callDetailRecordService, _fileService);
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
            var csvData = "caller_id,recipient,call_date,end_time,duration,cost,reference,currency\n" +
                     "123,456,01/10/2023,14:30:00,60,5.00,ABC123,USD\n"; // Sample CSV data

            
            var memoryStream = new MemoryStream();
            var writer = new StreamWriter(memoryStream);
            writer.Write(csvData);
            writer.Flush();
            memoryStream.Position = 0;
            var csvFile = new FormFile(memoryStream, 0, memoryStream.Length, "file", "test.csv");

            // Act
            var result = await controller.Import(csvFile);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            Assert.AreEqual("1 records are imported and 0 records are ignored.", okResult.Value);

        }
        [TestMethod]
        public async Task Import_InvalidFile_Error()
        {
            var csvData = "caller_id,recipient,call_date,end_time,duration,cost,reference,currency\n" +
                     "123,456,01/10/2023,14:30:00,60,5.00,,USD\n"; // Sample CSV data


            var memoryStream = new MemoryStream();
            var writer = new StreamWriter(memoryStream);
            writer.Write(csvData);
            writer.Flush();
            memoryStream.Position = 0;
            var csvFile = new FormFile(memoryStream, 0, memoryStream.Length, "file", "test.csv");

            // Act
            var result = await controller.Import(csvFile);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            Assert.AreEqual("0 records are imported and 1 records are ignored.", okResult.Value);

        }


        [TestMethod]
        public async Task Import_EmptyFile_Error()
        {
            
            var memoryStream = new MemoryStream();
            var writer = new StreamWriter(memoryStream);
            memoryStream.Position = 0;
            var csvFile = new FormFile(memoryStream, 0, memoryStream.Length, "file", "test.csv");

            // Act
            Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => controller.Import(csvFile), "No file uploaded.");
        }

        [TestMethod]
        public async Task Import_OtherThanCsvFile_Error()
        {

            var csvData = "caller_id,recipient,call_date,end_time,duration,cost,reference,currency\n" +
                     "123,456,01/10/2023,14:30:00,60,5.00,,USD\n"; // Sample CSV data


            var memoryStream = new MemoryStream();
            var writer = new StreamWriter(memoryStream);
            writer.Write(csvData);
            writer.Flush();
            memoryStream.Position = 0;
            var csvFile = new FormFile(memoryStream, 0, memoryStream.Length, "file", "test.csv");

            // Act
            Assert.ThrowsExceptionAsync<ArgumentException>(async () => controller.Import(csvFile), "Invalid file uploaded");
        }
    }
}