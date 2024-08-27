using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AssetsPathGenerator;

public class DefaultAssetsPathHandler : IAssetsPathHandler
{
    private static readonly HashSet<string> Keywords = new System.Collections.Generic.HashSet<string>
    {
        "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class", "const", "continue", "decimal", "default", "delegate",
        "do", "double", "else", "enum", "event", "explicit", "extern", "false", "finally", "fixed", "float", "for", "foreach", "goto", "if", "implicit", "in",
        "int", "interface", "internal", "is", "lock", "long", "namespace", "new", "null", "object", "operator", "out", "override", "params", "private",
        "protected", "public", "readonly", "ref", "return", "sbyte", "sealed", "short", "sizeof", "stackalloc", "static", "string", "struct", "switch", "this",
        "throw", "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using", "virtual", "void", "volatile", "while"
    };

    private readonly List<string>
        _classNames = new(),
        _fileNames = new();
    
    public string GenerateClassName(string className)
    {
        if(char.IsLower(className[0]))
            className = char.ToUpper(className[0]) + className.Substring(1);
        
        className =
            new Regex(@"[^\p{Ll}\p{Lu}\p{Lt}\p{Lo}\p{Nd}\p{Nl}\p{Mn}\p{Mc}\p{Cf}\p{Pc}\p{Lm}]")
            .Replace(className, "");
        
        if(Keywords.Contains(className)) className = className.Insert(0, "@");

        var finalName = className.Replace(" ", string.Empty);

        while (_classNames.Contains(finalName) || _fileNames.Contains(finalName))
        {
            finalName = finalName.Insert(0, "_");
        }
        
        _classNames.Add(finalName);
        
        return finalName;
    }
    
    public string GenerateFileName(string className)
    {
        if(char.IsLower(className[0]))
            className = char.ToUpper(className[0]) + className.Substring(1);

        className = Path.GetFileNameWithoutExtension(className);
        className =
            new Regex(@"[^\p{Ll}\p{Lu}\p{Lt}\p{Lo}\p{Nd}\p{Nl}\p{Mn}\p{Mc}\p{Cf}\p{Pc}\p{Lm}]")
            .Replace(className, "");

        if(Keywords.Contains(className)) className = className.Insert(0, "@");

        var finalName = className.Replace(" ", string.Empty);

        while (_fileNames.Contains(finalName) || _classNames.Contains(finalName))
        {
            finalName = finalName.Insert(0, "_");
        }
        
        _fileNames.Add(finalName);
        
        return finalName;
    }
}