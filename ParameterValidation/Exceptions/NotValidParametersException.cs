using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace ParameterValidation.Exceptions
{
    public class NotValidParametersException : Exception
    {
        public IEnumerable<ValidationResult> Errors { get; }

        public NotValidParametersException(IEnumerable<ValidationResult> errors)
        {
            Errors = errors;
        }

        public NotValidParametersException(IEnumerable<string> members, string message)
        {
            ValidationResult validationResult = new ValidationResult(message, members);
            IEnumerable<ValidationResult> errors = new List<ValidationResult> { validationResult };

            Errors = errors;
        }

        public NotValidParametersException(string member, string message)
        {
            ValidationResult validationResult = new ValidationResult(message, new List<string> { member });
            IEnumerable<ValidationResult> errors = new List<ValidationResult> { validationResult };

            Errors = errors;
        }

        public NotValidParametersException(string message) 
            : base(message) { }

        public NotValidParametersException(string message, Exception innerException) 
            : base(message, innerException) { }

        protected NotValidParametersException(SerializationInfo info, StreamingContext context) 
            : base(info, context) { }
    }
}
