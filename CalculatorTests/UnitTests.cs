using System;
using Xunit;

namespace CalculatorTests
{
    public class UnitTests
    {

        [Fact]
        public void SubtractionTest()
        {
            string expression = "7-8";
            string result = "-1";
            string got_result = CalculatorLogic.Calculations.Calculate(expression);
            Assert.Equal(result, got_result);
        }

        [Fact]
        public void SumTest()
        {
            string expression = "7+8";
            string result = "15";
            string got_result = CalculatorLogic.Calculations.Calculate(expression);
            Assert.Equal(result, got_result);
        }


        [Fact]
        public void MultiplicationTest()
        {
            string expression = "7*8";
            string result = "56";
            string got_result = CalculatorLogic.Calculations.Calculate(expression);
            Assert.Equal(result, got_result);
        }

        [Fact]
        public void DivisonTest()
        {
            string expression = "8/4";
            string result = "2";
            string got_result = CalculatorLogic.Calculations.Calculate(expression);
            Assert.Equal(result, got_result);
        }

        [Fact]
        public void ComplexExpressionTest()
        {
            string expression = "4*11-2+6-32/12";
            double result = 45.33;
            double got_result = double.Parse(CalculatorLogic.Calculations.Calculate(expression));
            got_result = Math.Round(got_result, 2);
            Assert.Equal(result, got_result);
        }

        [Fact]
        public void ZeroDivTest()
        {
            string expression = "5/0";
            Assert.Throws<Exception>(() => CalculatorLogic.Calculations.Calculate(expression));
        }

        [Fact]
        public void IncorrectInputTest1()
        {
            string expression = "sarddgrfjha";
            Assert.Throws<Exception>(() => CalculatorLogic.Calculations.Calculate(expression));
        }

        [Fact]
        public void IncorrectInputTest2()
        {
            string expression = "1+zxcvzxcvzxcv";
            Assert.Throws<Exception>(() => CalculatorLogic.Calculations.Calculate(expression));
        }

        [Fact]
        public void IncorrectInputTest3()
        {
            string expression = "1+";
            Assert.Throws<Exception>(() => CalculatorLogic.Calculations.Calculate(expression));
        }

        [Fact]
        public void IncorrectInputTest4()
        {
            string expression = "1";
            Assert.Throws<ArgumentException>(() => CalculatorLogic.Calculations.Calculate(expression));
        }

        [Fact]
        public void IncorrectInputTest5()
        {
            string expression = "/";
            Assert.Throws<Exception>(() => CalculatorLogic.Calculations.Calculate(expression));
        }
    }
}
