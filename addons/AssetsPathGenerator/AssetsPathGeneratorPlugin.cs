#if TOOLS
using Godot;

namespace AssetsPathGenerator;

[Tool]
public partial class AssetsPathGeneratorPlugin : EditorPlugin
{
	public override void _EnterTree()
	{
		AddToolMenuItem("Generate Path", new Callable(this, nameof(GeneratePath)));
		EditorInterface.Singleton.GetResourceFilesystem().ResourcesReimported += OnResourcesReload;
		EditorInterface.Singleton.GetFileSystemDock().FileRemoved += OnFileRemoved;
		EditorInterface.Singleton.GetFileSystemDock().FolderMoved += OnFolderRemoved;
		GeneratePath();
	}

	public override void _ExitTree()
	{
		RemoveToolMenuItem("Generate Path");
		EditorInterface.Singleton.GetResourceFilesystem().ResourcesReimported -= OnResourcesReload;
		EditorInterface.Singleton.GetFileSystemDock().FileRemoved -= OnFileRemoved;
		EditorInterface.Singleton.GetFileSystemDock().FolderMoved -= OnFolderRemoved;
	}

	private void GeneratePath()
	{
		AssetsPathGenerator.Generate(new DefaultAssetsPathHandler());
	}

	private void OnResourcesReload(string[] resources) => GeneratePath();
	private void OnFileRemoved(string file) => GeneratePath();
	private void OnFolderRemoved(string folder, string folder2) => GeneratePath();
}
#endif
