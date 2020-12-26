using Castle.DynamicProxy;
using ParameterValidation.Attributes;
using ParameterValidation.Interceptor;
using System;

namespace ParameterValidation
{
    class Program
    {
        public static void Main(string[] args)
        {
            ProxyGenerator proxyGenerator = new ProxyGenerator();

            ICalculator calculator = new Calculator();

            ICalculator proxy = proxyGenerator.CreateInterfaceProxyWithTarget(
                calculator,
                ProxyGenerationOptions.Default,
                new ValidationInterceptor()
            );

            Console.WriteLine(proxy.Add(-1, 10));
        }        
    }

    public interface ICalculator
    {
        int Add([Range(0, int.MaxValue)] int a, [Range(0, int.MaxValue)] int b);
    }

    public class Calculator : ICalculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }
}
