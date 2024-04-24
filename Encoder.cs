namespace PaulQpro.QPack;

public class Encoder{
	private List<DirEntry> dirs;
	private List<FileEntry> files;

	public string Source { get; }

	public void Encode(){
		EncodeRecursive(-1);
	}

	public void Save(){
		string name = Path.GetDirectoryName(Source) ?? "Pack"; 
		Save(((name == "") ? "Pack" : name)  + ".qpack");
	}
	public void Save(string name){
		using (BinaryWriter bw = new(File.Create(name))){
			bw.Write(dirs.Count);
			foreach(DirEntry dir in dirs){
				bw.Write(dir.Name.Length);
				foreach(char c in dir.Name){
					bw.Write(c);
				}
				bw.Write(dir.ChildrenDirs.Length);
				foreach(int cDir in dir.ChildrenDirs){
					bw.Write(cDir);
				}
				bw.Write(dir.ChildrenFiles.Length);
				foreach(int cFile in dir.ChildrenFiles){
					bw.Write(cFile);
				}
				bw.Write(dir.Parent);
			}
			foreach(FileEntry file in files){
				bw.Write(file.Name.Length);
				foreach(char c in file.Name){
					bw.Write(c);
				}
				bw.Write(file.Data.Length);
				bw.Write(file.Data);
				bw.Write(file.Parent);
			}
		}
	}

	private void EncodeRecursive(int dirID){
		string source = (dirID == -1) ? Source : dirs[dirID].Path;
		int i = 0;
		foreach (string dir in Directory.GetDirectories(source)){
			Console.WriteLine(dir);
			int thisDirID = dirs.Count;
			dirs.Add(new(
				Path.GetDirectoryName(dir) ?? "_",
				new int[Directory.GetDirectories(dir).Length],
				new int[Directory.GetFiles(dir).Length],
				dirID,
				dir
			));
			if (dirID != -1){
				dirs[dirID].ChildrenDirs[i] = thisDirID;
			}
		} 
		i = 0;
		foreach (string file in Directory.GetFiles(source)){
			Console.WriteLine(file);
			byte[] bytes;
			using(BinaryReader br = new(File.OpenRead(file))){
				bytes = br.ReadBytes((int)br.BaseStream.Length);
			}
			int thisFileID = files.Count;
			files.Add(new(
				Path.GetFileName(file),
				bytes,
				dirID
			));
			if (dirID != -1){
				dirs[dirID].ChildrenFiles[i] = thisFileID;
			}
		}
	}
	
	public Encoder() : this(Directory.GetCurrentDirectory()) {}
	public Encoder(string source){
		Source = source;
		dirs = new();
		files = new();
	}
}
