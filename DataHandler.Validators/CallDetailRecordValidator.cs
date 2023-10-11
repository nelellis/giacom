using DataHandler.Entities;
using FluentValidation;

namespace DataHandler.Validators
{
    public class CallDetailRecordValidator : AbstractValidator<CallDetailRecord>
    {
        public CallDetailRecordValidator() {
            ClassLevelCascadeMode = CascadeMode.Stop;
            //RuleFor(x => x.CallerId).NotEmpty().WithMessage("Caller is not provided");            
            RuleFor(x => x.Recipient).NotEmpty().WithMessage("Recipient is not provided");
            RuleFor(x => x.Reference).NotEmpty().WithMessage("Reference is not provided");

        }
        
    }
}