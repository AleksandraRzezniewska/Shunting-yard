using System;
using System.Collections;

namespace Calculator.Library
{
    public class Equations
    {
        private string _equation;
        private int _number;
        private double? _result;

        public double? Result
        {
            get
            {
                if (_result != null)
                {
                    return _result;
                }
                else
                {
                    _result = RozwiklanieSzyfru();

                    return _result;
                }
            }
        }

        public Equations(string equation)
        {
            _equation = equation;
        }

        public static double? operator +(Equations equation1, Equations equation2)
        {
            return equation1.Result + equation2.Result;
        }

        public static double? operator -(Equations equation1, Equations equation2)
        {
            return equation1.Result - equation2.Result;
        }

        public static double? operator *(Equations equation1, Equations equation2)
        {
            return equation1.Result * equation2.Result;
        }

        public static double? operator /(Equations equation1, Equations equation2)
        {
            return equation1.Result / equation2.Result;
        }

        public object[] StringDivider()
        {
            var equationString = new Stack();
            var condition = false;
            var number = "";

            for (var i = 0; i < _equation.Length; i++)
            {
                if (condition)
                {
                    if (int.TryParse(Convert.ToString(_equation[i]), out _number))
                    {
                        number = Convert.ToString(equationString.Pop()) + Convert.ToString(_equation[i]);
                        equationString.Push(number);
                        condition = true;
                    }
                    else
                    {
                        equationString.Push(Convert.ToString(_equation[i]));
                        condition = false;
                    }
                }
                else
                {
                    if (int.TryParse(Convert.ToString(_equation[i]), out _number))
                    {
                        number = Convert.ToString(_equation[i]);
                        equationString.Push(number);
                        condition = true;
                    }
                    else
                    {
                        equationString.Push(Convert.ToString(_equation[i]));
                    }
                }
            }

            var equation = new Stack();
            var equationlength = equationString.ToArray();

            for (var i = 0; i < equationlength.Length; i++)
            {
                equation.Push(equationString.Pop());
            }

            return equation.ToArray();
        }

        public object[] Shantingyard()
        {
            var equationString = StringDivider();
            var queue = new Queue();
            var stack = new Stack();

            for (var i = 0; i < equationString.Length; i++)
            {
                if (int.TryParse(Convert.ToString(equationString[i]), out _number))
                {
                    queue.Enqueue(equationString[i]);
                }
                else
                {
                    if (Convert.ToString(equationString[i]) == Constants.Plus | Convert.ToString(equationString[i]) == Constants.Minus)
                    {
                        if (stack.Count == 0)
                        {
                            stack.Push(equationString[i]);
                        }
                        else if (Convert.ToString(stack.Peek()) == Constants.Asterisk | Convert.ToString(stack.Peek()) == Constants.DivisionSlash | Convert.ToString(stack.Peek()) == Constants.Exponent | Convert.ToString(stack.Peek()) == Constants.Plus | Convert.ToString(stack.Peek()) == Constants.Minus)
                        {
                            queue.Enqueue(Convert.ToString(stack.Pop()));

                            if (stack.Count == 0)
                            {

                            }
                            else if (Convert.ToString(stack.Peek()) == Constants.Asterisk | Convert.ToString(stack.Peek()) == Constants.DivisionSlash | Convert.ToString(stack.Peek()) == Constants.Exponent | Convert.ToString(stack.Peek()) == Constants.Plus | Convert.ToString(stack.Peek()) == Constants.Minus)
                            {
                                queue.Enqueue(Convert.ToString(stack.Pop()));
                            }

                            stack.Push(equationString[i]);
                        }
                        else
                        {
                            stack.Push(equationString[i]);
                        }
                    }
                    else if (Convert.ToString(equationString[i]) == Constants.Asterisk | Convert.ToString(equationString[i]) == Constants.DivisionSlash)
                    {
                        if (stack.Count == 0)
                        {
                            stack.Push(equationString[i]);
                        }
                        else if (Convert.ToString(stack.Peek()) == Constants.Plus | Convert.ToString(stack.Peek()) == Constants.Minus)
                        {
                            stack.Push(equationString[i]);
                        }
                        else if (Convert.ToString(stack.Peek()) == Constants.Asterisk | Convert.ToString(stack.Peek()) == Constants.DivisionSlash | Convert.ToString(stack.Peek()) == Constants.Exponent)
                        {
                            queue.Enqueue(stack.Pop());
                            if (stack.Count == 0)
                            {

                            }
                            else if (Convert.ToString(stack.Peek()) == Constants.Asterisk | Convert.ToString(stack.Peek()) == Constants.DivisionSlash | Convert.ToString(stack.Peek()) == Constants.Exponent)
                            {
                                queue.Enqueue(Convert.ToString(stack.Pop()));
                            }
                            stack.Push(equationString[i]);
                        }
                        else
                        {
                            stack.Push(equationString[i]);
                        }
                    }
                    else if (Convert.ToString(equationString[i]) == Constants.Exponent)
                    {
                        stack.Push(equationString[i]);
                    }
                    else if (Convert.ToString(equationString[i]) == Constants.LeftBracket)
                    {
                        stack.Push(equationString[i]);
                    }
                    else if (Convert.ToString(equationString[i]) == Constants.RightBracket)
                    {
                        if (Convert.ToString(stack.Peek()) == Constants.LeftBracket)
                        {
                            stack.Pop();
                        }
                        else
                        {
                            queue.Enqueue(stack.Pop());

                            if (Convert.ToString(stack.Peek()) == Constants.LeftBracket)
                            {
                                stack.Pop();
                            }
                        }
                    }
                }
            }

            var stos = stack.ToArray();

            for (var i = 0; i < stos.Length; i++)
            {
                queue.Enqueue(stack.Pop());
            }

            return queue.ToArray();
        }

        public double RozwiklanieSzyfru()
        {
            var stack = new Stack();
            var equation = Shantingyard();

            for (var i = 0; i < equation.Length; i++)
            {
                if (int.TryParse(Convert.ToString(equation[i]), out _number))
                {
                    stack.Push(equation[i]);
                }
                else
                {
                    var y = Convert.ToDouble(stack.Pop());
                    var x = Convert.ToDouble(stack.Pop());
                    var sign = Convert.ToString(equation[i]);

                    double result = MathOperations(x, y, sign);
                    stack.Push(result);
                }
            }

            return Convert.ToDouble(stack.Pop());
        }

        public double MathOperations(double number1, double number2, string sign)
        {
            switch (sign)
            {
                case Constants.Plus:
                    return number1 + number2;
                case Constants.Minus:
                    return number1 - number2;
                case Constants.Asterisk:
                    return number1 * number2;
                case Constants.Exponent:
                    return Math.Pow(number1, number2);
                case Constants.DivisionSlash:
                    if (number2 == 0)
                    {
                        throw new DivideByZeroException();
                    }

                    return number1 / number2;
            }

            return 0;
        }
    }
}
