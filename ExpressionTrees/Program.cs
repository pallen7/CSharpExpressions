using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTrees
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Expression Trees");
            Console.WriteLine("Primarily from Jamie King tutorial on YouTube");

            lesson_1();
            lesson_2();
            lesson_3();
            lesson_4();
            lesson_5();
            lesson_6();
            lesson_7();

            Console.WriteLine("\nPress enter to exit");
            Console.ReadLine();
        }

        private static void lesson_7()
        {
            /*
            The following lambda expression:       Func<int, bool> exp = x => x > 5;
            The => lambda expression with the body is made up as below
            This is pretty much how the compiler breaks down a lambda and converts to an expression tree except it doesn't
            use all of the intermediate variables
            Entity Framework (for example) takes the objects in the expression tree below and creates a SQL query
            
            Expression.Lambda --> has a generic (requires the types) and a non-generic (may be a possibility to create on the fly) version

            */

            Console.WriteLine("\n----> lesson 7 <----");

            ParameterExpression parameter_expression = Expression.Parameter(typeof(int), "x");
            ConstantExpression constant_expression = Expression.Constant(5, typeof(int));
            BinaryExpression binary_expression = Expression.GreaterThan(parameter_expression, constant_expression);
            Expression<Func<int, bool>> lambda_expression =
                                            Expression.Lambda<Func<int, bool>>(binary_expression, parameter_expression);

            // Most of the tie you wouldn't do the below as it can be slow/expensive
            // Don't think this would be such a factor if we did this at startup?
            Func<int, bool> a_delegate = lambda_expression.Compile();
            Console.WriteLine(a_delegate(10));
            Console.WriteLine(a_delegate(3));

        }

        private static void lesson_6()
        {
            /*
            The following lambda expression:       Func<int, bool> exp = x => x > 5;
            The ">" is broken down as a ParameterExpression as below

            BinaryExpression is abstracted and could be GreaterThan, LessThan etc...

            */

            Console.WriteLine("\n----> lesson 6 <----");

            ParameterExpression parameter_expression = Expression.Parameter(typeof(int), "x");
            ConstantExpression constant_expression = Expression.Constant(5, typeof(int));
            BinaryExpression binary_expression = Expression.GreaterThan(parameter_expression, constant_expression);

            Console.WriteLine("binary_expression.NodeType: {0}", binary_expression.NodeType);
            Console.WriteLine("binary_expression.Type: {0}", binary_expression.Type);
            Console.WriteLine("binary_expression.Left: {0}", binary_expression.Left);
            Console.WriteLine("binary_expression.Right: {0}", binary_expression.Right);
        }

        private static void lesson_5()
        {
            /*
            The following lambda expression:       Func<int, bool> exp = x => x > 5;
            The "x" is broken down as a ParameterExpression as below

            */

            Console.WriteLine("\n----> lesson 5 <----");

            ParameterExpression parameter_expression = Expression.Parameter(typeof(int), "x");

            Console.WriteLine("parameter_expression.NodeType: {0}", parameter_expression.NodeType);
            Console.WriteLine("parameter_expression.Type: {0}", parameter_expression.Type);
            Console.WriteLine("parameter_expression.Name: {0}", parameter_expression.Name);
        }

        private static void lesson_4()
        {
            /*
            From Vid: "An expression is something that returns in place of itself a value"
            i.e. the expression x > 5:
            ">" is an expression that takes a left expression and a right expression and will return a bool
            "x" is also and expression
            "5"... also

            */

            Console.WriteLine("\n----> lesson 4 <----");
            Console.WriteLine("....");

        }

        private static void lesson_3()
        {
            /*
            The following lambda expression:       Func<int, bool> exp = x => x > 5;
            The "5" in the above expression is broken into a ConstantExpression by csc.exe as in the below

            NodeType is of type ExpressionType which is an enum listing all of the types of expressions we have in C#

            */

            Console.WriteLine("\n----> lesson 3 <----");

            ConstantExpression constant_expression =  Expression.Constant(5, typeof(int));

            Console.WriteLine("constant_expression.NodeType: {0}", constant_expression.NodeType);
            Console.WriteLine("constant_expression.Type: {0}", constant_expression.Type);
            Console.WriteLine("constant_expression.Value: {0}", constant_expression.Value);
        }

        private static void lesson_2()
        {
            /*
            Unlike delegates Expressions cannot refer to functions and only work with Lambda expressions
            Expression takes the lambda and converts into objects at runtime
            */

            Console.WriteLine("\n----> lesson 2 <----");

            Expression<Func<int, bool>> exp = x => x > 5;
            BinaryExpression binary = (BinaryExpression)exp.Body;

            Console.WriteLine("exp.Body: {0}", exp.Body);
            Console.WriteLine("exp.Body.GetType(): {0}", exp.Body.GetType());
            Console.WriteLine("binary.Left: {0}", binary.Left);
            Console.WriteLine("binary.Left: {0}", binary.NodeType);
            Console.WriteLine("binary.Right: {0}", binary.Right);
        }

        private static void lesson_1()
        {
            /*
            .net (MSIL) has no understanding of lambdas so the csc (c# compiler) TYPICALLY compiles the below into:

            Func<int, bool> test = x => x > 5;      // Is transformed into:

            function bool some_name(int x)
            {
                return x > 5;
            }
            */
            Console.WriteLine("\n----> lesson 1 <----");

            Func<int, bool> test = x => x > 5;

            Console.WriteLine(test(3));
            Console.WriteLine(test(8));
        }
    }
}
