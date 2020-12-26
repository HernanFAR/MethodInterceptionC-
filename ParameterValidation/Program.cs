using Castle.DynamicProxy;
using ParameterValidation.Exceptions;
using ParameterValidation.Interceptor;
using System;
using System.ComponentModel.DataAnnotations;

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

            Console.WriteLine("Validando con 1er parametro invalido");
            try
            {
                Console.WriteLine(proxy.Add(-1, 10, 25));
            }
            catch (NotValidParametersException ex)
            {
                foreach (var validationResult in ex.Errors)
                    Console.WriteLine(validationResult.ErrorMessage);
            }

            Console.WriteLine("\nValidando con 2do parametro invalido");
            try
            {
                Console.WriteLine(proxy.Add(10, -1, 25));
            }
            catch (NotValidParametersException ex)
            {
                foreach (var validationResult in ex.Errors)
                    Console.WriteLine(validationResult.ErrorMessage);
            }

            Console.WriteLine("\nValidando con 3er parametro invalido");
            Console.WriteLine(proxy.Add(10, 25, -1));

            Console.WriteLine("\nValidando con 1er y 2do parametro invalido");
            try
            {
                Console.WriteLine(proxy.Add(-1, -5, 100));
            }
            catch (NotValidParametersException ex)
            {
                foreach (var validationResult in ex.Errors)
                    Console.WriteLine(validationResult.ErrorMessage);
            }

            Console.WriteLine("\nTodos validos");
            Console.WriteLine(proxy.Add(5, 10, 100));
        }        
    }

    public interface ICalculator
    {
        int Add(
            [Range(0, int.MaxValue, ErrorMessage = "El primer numero debe ser valido")] int a, 
            [Range(0, int.MaxValue, ErrorMessage = "El segundo numero debe ser valido")] int b,
            int c
        );
    }

    public class Calculator : ICalculator
    {
        public int Add(int a, int b, int c)
        {
            return a + b + c;
        }
    }
}
