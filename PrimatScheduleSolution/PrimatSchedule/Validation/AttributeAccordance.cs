using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrimatScheduleBot
{
    public class AttributeAccordance<AttributeType> : IValidator
        where AttributeType : ValidationAttribute, new()
    {
        private readonly object _instance;

        public AttributeAccordance(object instance) => _instance = instance;

        private (ValidationContext Context, List<ValidationAttribute> Attributes) ValidationTools() 
        {
            var context = new ValidationContext(_instance);

            var validationAttributes = new List<ValidationAttribute> { new AttributeType() };

            return (context, validationAttributes);
        }

        public void Validate()
        {
            var tools = ValidationTools();

            Validator.TryValidateValue(_instance, tools.Context, null, tools.Attributes);
        }

        public bool IsValid()
        {
            var tools = ValidationTools();

            return Validator.TryValidateValue(_instance, tools.Context, null, tools.Attributes);
        }
    }
}