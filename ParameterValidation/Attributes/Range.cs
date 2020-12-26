using System;
using System.Collections.Generic;
using System.Text;

namespace ParameterValidation.Attributes
{
    public class RangeAttribute : ArgumentValidationAttribute
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public RangeAttribute(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public override void Validate(object value, string argumentName)
        {
            int intValue = (int)value;
            if (intValue < Min || intValue > Max)
            {
                throw new ArgumentOutOfRangeException(argumentName, string.Format("min={0}, max={1}", Min, Max));
            }
        }
    }
}
