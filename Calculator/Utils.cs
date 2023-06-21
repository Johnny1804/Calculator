using System;
using System.Text;
using System.Collections.Generic;

namespace Calculator
{
    public static class Utils
    {
        public static bool PeriodState(string text)
        {
            if (text.Contains(","))
            {
                return true;
            }

            return false;
        }

        private static double Operation(char operation, double firstNumber, double secondNumber)
        {
            Dictionary<char, Func<double, double, double>> operators = new Dictionary<char, Func<double, double, double>>
            {
                { '+', (firstNumber, secondNumber) => firstNumber + secondNumber },
                { '-', (firstNumber, secondNumber) => firstNumber - secondNumber },
                { '/', (firstNumber, secondNumber) => firstNumber / secondNumber },
                { '*', (firstNumber, secondNumber) => firstNumber * secondNumber }
            };

            return operators[operation](firstNumber, secondNumber);
        }

        public static string CalculateOperation(string text1, string text2)
        {
            double previousNumber = double.Parse(text1[0..^1]);
            double currentNumber = double.Parse(text2);
            char operation = text1.ToCharArray()[text1.Length - 1];

            if (operation.Equals('/') && previousNumber.Equals(0) && currentNumber.Equals(0))
            {
                return "Result is undefined";
            }

            if (operation.Equals('/') && currentNumber.Equals(0))
            {
                return "Cannot divide by zero";
            }            

            text1 = Operation(operation, previousNumber, currentNumber).ToString("0.######");

            return text1;
        }
    }
}
