namespace AssetsPathGenerator;

public interface IAssetsPathHandler
{
    public string GenerateClassName(string className);
    public string GenerateFileName(string fileName);

    public string GenerateFinalCode(string generatedCode) => GetCodeTemplate().Replace("{0}", generatedCode);

    public string GetCodeTemplate() => """
                                       namespace AssetsPathGenerator;

                                       public static class AssetPaths
                                       {
                                           public static class Assets
                                           {
                                           {0}
                                           }
                                       }
                                       """;
}