using System;
using System.Collections.Generic;
using System.Text;

namespace ParameterValidation.Attributes
{
    public abstract class ArgumentValidationAttribute : Attribute
    {
        public abstract void Validate(object value, string argumentName);
    }
}
