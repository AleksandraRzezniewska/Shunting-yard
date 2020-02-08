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
    }
}
