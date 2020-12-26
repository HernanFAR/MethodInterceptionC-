using Castle.DynamicProxy;
using ParameterValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ParameterValidation.Interceptor
{
    public class ValidationInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            ParameterInfo[] parameters = invocation.Method.GetParameters();
            for (int index = 0; index < parameters.Length; index++)
            {
                var paramInfo = parameters[index];
                var attributes = paramInfo.GetCustomAttributes(typeof(ArgumentValidationAttribute), false);

                if (attributes.Length == 0)
                    continue;

                foreach (ArgumentValidationAttribute attr in attributes)
                {
                    attr.Validate(invocation.Arguments[index], paramInfo.Name);
                }
            }

            invocation.Proceed();
        }
    }
}
