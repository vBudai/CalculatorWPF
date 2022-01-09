using System;

namespace CalculatorLogic
{
    public class Calculations
    {

        //Calculating length of number
        private static int operandRead(string input, int i)
        {
            bool dflag = false;
            if (input[i] == '-')
            {
                i++;
            }
            while (input[i] is '0' or '1' or '2' or '3' or '4' or '5' or '6' or '7' or '8' or '9' or ',')
            {
                if (input[i] is ',')
                {
                    if (dflag is false)
                    {
                        dflag = true;
                    }
                    else
                    {
                        return -1;
                    }
                }
                i++;
            }
            return i;
        }


        //Calculating
        public static string Calculate(string input)
        {
            string opnd1, opnd2;
            char op = '\0';
            int i, n;
            double num1, num2, result = 0;
            input += '\0';

            while (true)
            {
                i = 0;
                n = 0;
                i = operandRead(input, i);

                if (i == -1)
                {
                    throw new Exception("Input error!");
                }
                if (input[i] is '\0')
                {
                    if (op is '\0')
                    {
                        throw new ArgumentException("Expression isn't changed!");
                    }
                    else 
                    {
                        break;
                    }
                }

                opnd1 = input[0..i];

                if (string.IsNullOrEmpty(opnd1))
                {
                    throw new Exception("Input error!");
                }

                op = input[i++];

                if (op is not '+' and not '-' and not '*' and not '/')
                {
                    throw new Exception("Wrong operation!");
                }

                n = i;
                i = operandRead(input, i);

                if (i == -1)
                {
                    throw new Exception("Input error!");
                }

                opnd2 = input[n..i];

                if (string.IsNullOrEmpty(opnd2))
                {
                    throw new Exception("Input error!");
                }

                //Priority of * and /
                if (input[i] is '/' or '*')
                {
                    op = input[i++];
                    n = i;
                    opnd1 = opnd2;
                    i = operandRead(input, i);
                    if (i == -1)
                    {
                        throw new Exception("Input error!");
                    }
                    opnd2 = input[n..i];
                    if (string.IsNullOrEmpty(opnd2))
                    {
                        throw new Exception("Input error!");
                    }
                }

                double.TryParse(opnd1, out num1);
                double.TryParse(opnd2, out num2);

                switch (op)
                {
                    case '+':
                        result = num1 + num2;
                        break;

                    case '-':
                        result = num1 - num2;
                        break;

                    case '*':
                        result = num1 * num2;
                        break;

                    case '/':
                        result = num1 / num2;
                        break;
                }
                input = input.Replace(input[(n - opnd1.Length - 1)..i], result.ToString());
            }
            input = input.Remove(input.Length - 1, 1);
            return input;
        }
    }
}
