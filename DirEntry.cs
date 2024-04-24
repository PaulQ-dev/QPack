namespace PaulQpro.QPack;

internal readonly struct DirEntry
{
	private readonly int[] childrenD;
	private readonly int[] childrenF;

	public string Name { get; }
	public int[] ChildrenDirs { get => childrenD; }
	public int[] ChildrenFiles { get => childrenF; }
	public int Parent { get; }
	public string Path { get; }

	public DirEntry(string name, int[] subDirs, int[] files, int parent, string path){
		Name = name;
		childrenD = subDirs;
		childrenF = files;
		Parent = parent;
		Path = path;
	}
}
