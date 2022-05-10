using System.ComponentModel.DataAnnotations;

namespace PrimatScheduleBot
{
    public class FullValidationMode<AttributeType, ExceptionType> : IValidator
        where AttributeType : ValidationAttribute, new()
        where ExceptionType : MessageException, new()
    {
        private readonly AttributeAccordance<AttributeType> _validator;

        public FullValidationMode(object instance) 
            => _validator = new AttributeAccordance<AttributeType>(instance);

        public void Validate()
        {
            bool statement = IsValid();

            var polygraph = new Polygraph<ExceptionType>(statement);

            polygraph.Validate();
        }

        private bool IsValid() => _validator.IsValid();
    }
}