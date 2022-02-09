using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PrimatScheduleBot
{
    sealed class Date : Command, IValidatableObject
    {
        private readonly string _date;
        private static int _year;

        static Date() => _year = DateTime.Now.Year;

        public Date(string date) : base(
            new Dictionary<MessageResult, string>
            {
                { MessageResult.NOTOK, $"Невірні вхідні дані. Скористуйтеся довідкою {PrimatScheduleBot.Messages.Help}." }
            })
            => _date = date.Insert(0, $"{_year}-");

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (!IsDateInRightFormat() || !IsSuchADateExist())
            {
                errors.Add(new ValidationResult(Messages.GetValueOrDefault(MessageResult.NOTOK)));
            }

            return errors;
        }

        private bool IsSelfValidateSuccessful()
        {
            var context = new ValidationContext(this);

            return Validator.TryValidateObject(this, context, new List<ValidationResult>(), true);
        }

        private bool IsDateInRightFormat()
        {
            Regex dateFormat = new Regex($"^{DateTime.Now.Year}[-]([0][1-5])[-]([0]?[1-9]|[12][0-9]|[3][01])$");

            return dateFormat.IsMatch(_date);
        }

        private bool IsSuchADateExist()
        {
            try
            {
                DateTime.Parse(_date);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public override string DoTaskAndGetMessage()
        {
            if (IsSelfValidateSuccessful())
            {
                return Querier.GetQuerySelection(_date).ToString();
            }

            return Messages.GetValueOrDefault(MessageResult.NOTOK);
        }
    }
}
