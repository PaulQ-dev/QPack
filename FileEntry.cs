namespace PaulQpro.QPack;

internal readonly struct FileEntry
{
	private readonly byte[] data;

	public string Name { get; }
	public byte[] Data { get => data; }
	public int Parent { get; }

	public FileEntry(string name, byte[] data, int parent){
		Name = name;
		this.data = data;
		Parent = parent;
	}
}
