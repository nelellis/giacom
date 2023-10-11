using DataHandler.Entities;
using DataHandler.Validators;

namespace DataHelper.Validators.Tests
{
    [TestClass]
    public class CallDetailRecordValidatorTests
    {
        [TestMethod]
        public void ValidCallDetailRecord_Success()
        {
            var callRecord = new CallDetailRecord()
            {
                CallDate = DateTime.Today,
                CallerId = "123456789",
                Cost = 0.500M,
                Currency = "GBP",
                Duration = 1,
                EndTime = new TimeSpan(10, 0, 0),
                Recipient = "23456712",
                Reference = Guid.NewGuid().ToString(),
            };
            var validator = new CallDetailRecordValidator();
            var validationResult = validator.Validate(callRecord);
            Assert.IsTrue(validationResult.IsValid);
            Assert.IsFalse(validationResult.Errors.Any());
        }

        [TestMethod]
        public void InvalidCallDetailRecord_Missing_Ref_Error()
        {
            var callRecord = new CallDetailRecord()
            {
                CallDate = DateTime.Today,
                CallerId = "123456789",
                Cost = 0.500M,
                Currency = "GBP",
                Duration = 1,
                EndTime = new TimeSpan(10, 0, 0),
                Recipient = "23456712",
            };
            var validator = new CallDetailRecordValidator();
            var validationResult = validator.Validate(callRecord);
            Assert.IsFalse(validationResult.IsValid);
            Assert.IsTrue(validationResult.Errors.Count == 1);
            Assert.IsTrue(validationResult.Errors.Any(e => e.ErrorMessage == "Reference is not provided"));
        }
        [TestMethod]
        public void InvalidCallDetailRecord_Missing_Recipient_Error()
        {
            var callRecord = new CallDetailRecord()
            {
                CallDate = DateTime.Today,
                CallerId = "123456789",
                Cost = 0.500M,
                Currency = "GBP",
                Duration = 1,
                EndTime = new TimeSpan(10, 0, 0),
                Reference = Guid.NewGuid().ToString(),
            };
            var validator = new CallDetailRecordValidator();
            var validationResult = validator.Validate(callRecord);
            Assert.IsFalse(validationResult.IsValid);
            Assert.IsTrue(validationResult.Errors.Count == 1);
            Assert.IsTrue(validationResult.Errors.Any(e => e.ErrorMessage == "Recipient is not provided"));
        }
    }
}