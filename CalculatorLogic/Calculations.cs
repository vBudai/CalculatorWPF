using System;

namespace CalculatorLogic
{
    public class Calculations
    {

        private static int ReadOperand(string input, int i)
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

        public static string Calculate(string input)
        {
            string opnd1, opnd2, expr = input;
            char op = '\0';
            int i, n;
            double num1, num2, result = 0;
            input += '\0';
            while (true)
            {
                i = 0;
                n = 0;
                i = ReadOperand(input, i);
                if (i == -1)
                {
                    throw new Exception("Impossible to solve!");
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
                    throw new Exception("Impossible to solve");
                }
                op = input[i++];
                if (op is not '+' and not '-' and not '*' and not '/')
                {
                    throw new Exception("Impossible to solve!");
                }
                n = i;
                i = ReadOperand(input, i);
                if (i == -1)
                {
                    throw new Exception("Impossible to solve!");
                }
                opnd2 = input[(opnd1.Length + 1)..i];
                if (string.IsNullOrEmpty(opnd2))
                {
                    throw new Exception("Impossible to solve!");
                }
                if (input[i] is '/' or '*')
                {
                    op = input[i++];
                    n = i;
                    opnd1 = opnd2;
                    i = ReadOperand(input, i);
                    if (i == -1)
                    {
                        throw new Exception("Impossible to solve!");
                    }
                    opnd2 = input[n..i];
                    if (string.IsNullOrEmpty(opnd2))
                    {
                        throw new Exception("Impossible to solve!");
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
