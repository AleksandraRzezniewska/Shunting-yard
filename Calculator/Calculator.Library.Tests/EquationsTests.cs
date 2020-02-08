using System;
using Xunit;

namespace Calculator.Library.Tests
{
    public class EquationsTests
    {
        [Fact(DisplayName = "Division by zero test")]
        public void DividedByZeroTest()
        {
            var equation = new Equations("3/0+3-4*9");
            var exception = Assert.Throws<DivideByZeroException>(() => equation.Result);

            Assert.Equal("Can't divide by zero", exception.Message);
        }

        [Fact(DisplayName = "StringDivider returns array of divides strings ")]
        public void StringDeviderReturnsArrayOfDividedStringsTest()
        {
            var equation = new Equations("3/1+2-4*8");
            var array = equation.StringDivider();
            var expectedArray = new string[9] { "3", "/", "1", "+", "2", "-", "4", "*", "8"}; 

            Assert.Equal(expectedArray, array);
        }

        [Fact(DisplayName = "Result returns proper result")]
        public void ResultReturnsProperResultTest()
        {
            var equation = new Equations("3*9-10/4+12-(2*3)");

            Assert.Equal(30.5, equation.Result);
        }
    }
}
