using Castle.DynamicProxy;
using ParameterValidation.Exceptions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ParameterValidation.Interceptor
{
    public class ValidationInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            ParameterInfo[] parameters = invocation.Method.GetParameters();
            bool proceed = true;
            IList<ValidationResult> results = new List<ValidationResult>();
            
            for (int index = 0; index < parameters.Length; index++)
            {
                ParameterInfo paramInfo = parameters[index];
                object[] attributes = paramInfo.GetCustomAttributes(typeof(ValidationAttribute), false);

                if (attributes.Length == 0)
                    continue;

                foreach (ValidationAttribute attr in attributes)
                {
                    object valueToValidate = invocation.Arguments[index];
                    bool isValid = attr.IsValid(valueToValidate);

                    if (!isValid)
                    {
                        proceed = false;

                        results.Add(new ValidationResult ( 
                            attr.ErrorMessage,
                            new List<string> { paramInfo.Name }
                        ));
                    }
                }
            }

            if (proceed)
            {
                invocation.Proceed();

                return;
            }

            throw new NotValidParametersException(results);
        }
    }
}
