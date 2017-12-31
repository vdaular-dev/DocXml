# DocXml - C# XML documentation reader 

This component is a set of helper classes and methods for compiler-generated XML documentation retrieval.
XML documentation as described here https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/xmldoc/xml-documentation-comments
is generated by compiler from source code comments and saved in a XML file where each
documented item is identified by its unique name. These names sometimes are quite complicated especially
when templates and generic types are involved. DocXml makes retrieval of XML documentation easy. For example
here is the class with generic method and the piece of code that retrieves method documentation.

```
// Source code for XML documentation
namespace LoxSmoke.DocXmlUnitTests
{
    class MyClass2
    {
        /// <summary>
        /// TemplateMethod description
        /// </summary>
        /// <returns>Return value description</returns>
        public List<T> TemplateMethod<T>()
        { return null; }
    }
}
```

```
var minfo = typeof(MyClass2).GetMethod("TemplateMethod");

DocXmlReader reader = new DocXmlReader("DocXmlUnitTests.xml");
var comments = reader.GetMethodComments(minfo);
Console.WriteLine(comments.Summary);
Console.WriteLine(comments.Returns);
```

## How to use

DocXml is available as LoxSmoke.DocXml nuget package. It is built as .net standard 2.0 class library.
Current version is 1.0.0

## Classes and methods

The main class is **DocXmlReader**. It reads XML file and returns documentation/comments objects.

Comment classes represent one or more comments associated with each item in the source code. Simple summary 
comments are returned as strings and more complex comments are returned as comments objects. Here is the list of 
comments classes:
* **TypeComments** is documentation of the class or struct. 
* **MethodComments** is documentation of the method, constructor, operator or property with parameter descriptions.
* **EnumComments** is documentation of the Enum type. Comments of enum values are included here as well.

Static **XmlDocId** class is a set of static methods that generates IDs used for retrieval of documentation from 
XML file. This class is used by **DocXmlReader**.   



