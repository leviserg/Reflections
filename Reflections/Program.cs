using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reflections;

namespace Reflections
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee employee = new Employee("Tom", 30);
            //employee.PrintInfo();
            PrintColorMessage(ConsoleColor.Green, employee.Payment(8,4).ToString());
            Type myType_way0 = employee.GetType();

            Type myType_way1 = typeof(Employee);

            Type myType_way2 = Type.GetType("Reflections.Employee", false, true);
            //                              |                     |     ^--------------v
            //                        ClassName, GenerateExceptionIfNotFound, IgnoreCharRegisterInClassName
            PrintColorMessage(ConsoleColor.Yellow, "#### TypeInfo from way 0 ####");
            foreach (MemberInfo mi in myType_way0.GetMembers())
            {
                PrintColorMessage(ConsoleColor.Yellow, $"{mi.DeclaringType} {mi.MemberType} {mi.Name}");
            }
            PrintColorMessage(ConsoleColor.Cyan, "#### TypeInfo from way 1 ####");
            foreach (MemberInfo mi in myType_way1.GetMembers())
            {
                PrintColorMessage(ConsoleColor.Cyan, $"{mi.DeclaringType} {mi.MemberType} {mi.Name}");
            }
            PrintColorMessage(ConsoleColor.Blue, "#### TypeInfo from way 2 ####");
            foreach (MemberInfo mi in myType_way2.GetMembers())
            {
                PrintColorMessage(ConsoleColor.Blue, $"{mi.DeclaringType} {mi.MemberType} {mi.Name}");
            }

            Console.WriteLine("#### Methods ####");
            foreach (MethodInfo method in myType_way2.GetMethods())
            {
                string modificator = "";
                if (method.IsStatic)
                    modificator += "static ";
                if (method.IsVirtual)
                    modificator += "virtual";
                Console.Write($"{modificator} {method.ReturnType.Name} {method.Name} (");
                // get all parameters
                ParameterInfo[] parameters = method.GetParameters();
                for (int i = 0; i < parameters.Length; i++)
                {
                    Console.Write($"{parameters[i].ParameterType.Name} {parameters[i].Name}");
                    if (i + 1 < parameters.Length) Console.Write(", ");
                }
                Console.WriteLine(")");
            }

            Console.WriteLine("#### Constructors ####");
            foreach (ConstructorInfo ctor in myType_way2.GetConstructors())
            {
                Console.Write(myType_way2.Name + " (");
                // get constructor 
                ParameterInfo[] parameters = ctor.GetParameters();
                for (int i = 0; i < parameters.Length; i++)
                {
                    Console.Write(parameters[i].ParameterType.Name + " " + parameters[i].Name);
                    if (i + 1 < parameters.Length) Console.Write(", ");
                }
                Console.WriteLine(")");
            }
            /*################################################################*/

            //Assembly asm = Assembly.LoadFrom("MyApp.dll");

            try
            {
                Console.WriteLine("##############");

                Type t = Type.GetType("Reflections.Employee", false, true);
                var ctors = t.GetConstructors();
                var ctor = ctors.FirstOrDefault(c => c.GetParameters().Length > 0); // [0] - default empty ctor
                object[] parametersArray = new object[] { "Jackson", 314 };

                object reflectClassObject = ctor.Invoke(parametersArray);
                MethodInfo reflectMethod = t.GetMethod("PrintInfo", BindingFlags.NonPublic | BindingFlags.Instance);

                reflectMethod.Invoke(reflectClassObject, null);
                Console.WriteLine("##############");

            }
            catch (Exception ex)
            {
                PrintColorMessage(ConsoleColor.Red, ex.Message);
            }
            Console.ReadLine();

        }

        private static string GetUserInput()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            string inpName = Console.ReadLine();
            Console.ResetColor();
            return inpName;
        }

        private static void PrintColorMessage(ConsoleColor color, string sMessage)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(sMessage);
            Console.ResetColor();
        }

    }
}
