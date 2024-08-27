# AssetsPathGenerator

Auto generate assets path for Godot.

## Usage

- Download and install plugin.

- Modify "res://addons/AssetsPathGenerator/AssetsPathGeneratorConfig.cs", everything is self-evident.

- Build project.

- Enabled plugin.

- You can found "Generate Path" in Project -> Tools.

> When you add or remove a file or folder, the plugin will try to auto generate path but if not work, by yourself.

## Custom

if you want to modify output file format, check DefaultAssetsPathHandler.cs and line 28 in AssetsPathGeneratorPlugin.cs.

## Sample output

```csharp
namespace AssetsPathGenerator;

public static class AssetPaths
{
    public static class Assets
    {

        public static class Characters
        {
            public static class CharactersSkeleton1
            {
                public const string Weapon = @"res://Assets//Characters/Skeleton1/Weapon.png";
            }

        }

        public static class Data
        {
            public const string Tbabilityinfo = @"res://Assets//Data/tbabilityinfo.json";
            public const string Tbconstants = @"res://Assets//Data/tbconstants.json";
        }

        public static class Gestures
        {
            public const string _Gestures = @"res://Assets//Gestures/Gestures.json";
            public const string Test = @"res://Assets//Gestures/Test.json";
        }

        public static class Languages
        {
            public const string Tables = @"res://Assets//Languages/Tables.csv";
            public const string Tablesen = @"res://Assets//Languages/Tables.en.translation";
            public const string Tableszh = @"res://Assets//Languages/Tables.zh.translation";
        }

        public static class Particles
        {
            public const string Flare = @"res://Assets//Particles/Flare.tres";
        }

        public static class Shaders
        {
            public const string ThunderVfx = @"res://Assets//Shaders/ThunderVfx.tres";
        }

        public static class Textures
        {
            public static class TexturesParticles
            {
                public const string Thunder = @"res://Assets//Textures/Particles/Thunder.png";
                public const string Lighting_circle = @"res://Assets//Textures/Particles/lighting_circle.png";
                public const string Spark_05_rotated = @"res://Assets//Textures/Particles/spark_05_rotated.png";
            }

            public const string DuctTape = @"res://Assets//Textures/DuctTape.png";
        }

    }
}
```




