using Godot;

namespace AssetsPathGenerator;

public class AssetsPathGenerator(IAssetsPathHandler handler)
{
    private string _generatedCode = "";
    
    public static void Generate(IAssetsPathHandler handler)
    {
        var generator = new AssetsPathGenerator(handler);
        generator.TraverseFolder(AssetsPathGeneratorConfig.Folder);
    }

    private string GetFullPath(string additionalPath) => AssetsPathGeneratorConfig.Folder + "/" + additionalPath;

    private void AppendLine(string line)
    {
        _generatedCode += "\n";
        _generatedCode += line;
    }

    private void TraverseFolder(string path)
    {
        var dir = DirAccess.Open(path);
        if (dir == null)
        {
            GD.PrintErr($"Failed to open directory in TraverseFolder: {path}");
            return;
        }

        foreach (var directory in dir.GetDirectories())
        {
            PrintDirectoryClass(directory, 2);
        }

        var finalCode = handler.GenerateFinalCode(_generatedCode);
        var file = FileAccess.Open(AssetsPathGeneratorConfig.GeneratedFilePath, FileAccess.ModeFlags.Write);
        if (file == null)
        {
            GD.PrintErr("Failed to open file in TraverseFolder");
            return;
        }

        file.StoreString(finalCode);
        file.Close();
        GD.Print("[AssetsPathGenerator] Generated file: " + AssetsPathGeneratorConfig.GeneratedFilePath);
    }
    
    private void PrintDirectoryClass(string dirName, int depth)
    {
        var firstIndent = new string('\t', depth);
        var className = handler.GenerateClassName(dirName);
        
        AppendLine($"{firstIndent}public static class {className}\n{firstIndent}{{");

        var dir = DirAccess.Open(GetFullPath(dirName));
        if (dir == null)
        {
            GD.PrintErr($"Failed to open directory in PrintDirectoryClass: {GetFullPath(dirName)}");
            return;
        }

        foreach (var subDir in dir.GetDirectories())
        {
            dir.Dispose();
            PrintDirectoryClass(dirName + "/" + subDir, depth + 1);
        }

        PrintContentFiles(dirName, depth + 1);
        
        AppendLine( $"{firstIndent}}}\n");
    }
    
    private void PrintContentFiles( string dirName, int depth)
    {
        var dir = DirAccess.Open(GetFullPath(dirName));
        if (dir == null)
        {
            GD.PrintErr($"Failed to open directory in PrintContentFiles: {dirName}");
            return;
        }

        var firstIndent = new string('\t', depth);
        
        foreach(var file in dir.GetFiles())
        {
            if (file.EndsWith(".import")) continue;
			 
            var finalPath = GetFullPath(dirName) + "/" + file;
            var className = handler.GenerateFileName(file);
    
            if(finalPath[0] == '/' || finalPath[0] == '\\' ) finalPath = finalPath.Substring(1);
            
            AppendLine($"{firstIndent}public const string {className} = @\"{finalPath}\";");
        }
        dir.Dispose();
    }
}