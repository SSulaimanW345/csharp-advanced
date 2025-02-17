using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSharp.Advanced.Console.Reflections
{
    public class ReflectionUsage
    {
        class Program
        {   


            private static Dictionary<string, string> _methodDictionary;
            //static void Main(string[] args)
            //{

            //    // Using typeof keyword
            //    Type exampleType = typeof(String);
            //    System.Console.WriteLine(exampleType.FullName); // System.String

            //    // Get properties & methods from Type
            //    PropertyInfo[] properties = exampleType.GetProperties();
            //    MethodInfo[] methods = exampleType.GetMethods();

            //    // Display property names
            //    foreach (PropertyInfo prop in properties)
            //    {
            //        System.Console.WriteLine(prop.Name);
            //    }

            //    // Get the method name in C#
            //    foreach (MethodInfo method in methods)
            //    {
            //        System.Console.WriteLine(method.Name);
            //    }

            //    Assembly currentAssembly = Assembly.GetExecutingAssembly();

            //    // Get the assembly details
            //    System.Console.WriteLine($"FullName: {currentAssembly.FullName}");
            //    System.Console.WriteLine($"Location: {currentAssembly.Location}");

            //    // Getypes from assembly
            //    Type[] assemblyTypes = currentAssembly.GetTypes();
            //    //foreach (Type type in assemblyTypes)
            //    //{
            //    //    System.Console.WriteLine(type.FullName);
            //    //}

            //    // Load an assembly (replace with actual assembly file)
            //    Assembly externalAssembly = Assembly.LoadFile(@"C:\path\to\your\assembly.dll");

            //    // Get all the types in the assembly
            //    Type[] assemblyTypes1 = externalAssembly.GetTypes();

            //    // Find a specific type by its name
            //    Type targetType = assemblyTypes1.FirstOrDefault(t => t.Name == "TargetTypeName");

            //    // Create an instance of the targetType (if found)
            //    if (targetType != null)
            //    {
            //        object targetInstance = Activator.CreateInstance(targetType);
            //    }

            //    _methodDictionary = new Dictionary<string, string>();
            //    _methodDictionary = GetMethodsDictionary();

            //    Type type = typeof(StudentFunction);
            //    var studentFunctionInstance = Activator.CreateInstance(type);

            //    var testString = "Hello [GetName], your university name is [GetUniversity] and roll is [GetRoll]";
            //    var match = Regex.Matches(testString, @"\[([A-Za-z0-9\-]+)]", RegexOptions.IgnoreCase);
            //    foreach (var v in match)
            //    {
            //        var originalString = v.ToString();
            //        var x = v.ToString();
            //        x = x.Replace("[", "");
            //        x = x.Replace("]", "");
            //        x = _methodDictionary[x];

            //        var toInvoke = type.GetMethod(x);
            //        var result = toInvoke.Invoke(studentFunctionInstance, null);
            //        testString = testString.Replace(originalString, result.ToString());
            //    }

            //    System.Console.WriteLine(testString);
            //}

            private static Dictionary<string, string> GetMethodsDictionary()
            {
                var dictionary = new Dictionary<string, string>
            {
            {"GetName", "GetName"},
            {"GetUniversity", "GetUniversity"},
            {"GetRoll","GetRoll"}
            };
                return dictionary;
            }
        }

    }
}


// 3 major - Assembly, Types, Members

// .Assembly
// .MethodInfo
// .PropertyInfo
// .MemberInfo
// .ConstructorInfo
// .EventInfo
// It permits the creation of new types during runtime and executes various actions utilizing those kinds.
// Attribute information can be seen during runtime.
// Late binding to functions and attributes is permitted.
// It enables instantiating and inspecting numerous kinds within an assembly.
public class Student
{
    public string Name { get; set; }
    public string University { get; set; }
    public int Roll { get; set; }
}

class StudentFunction
{
    private Student student;
    public StudentFunction()
    {
        student = new Student
        {
            Name = "Gopal C. Bala",
            University = "Jahangirnagar University",
            Roll = 1424
        };
    }

    public string GetName()
    {
        return student.Name;
    }

    public string GetUniversity()
    {
        return student.University;
    }

    public int GetRoll()
    {
        return student.Roll;
    }
}
