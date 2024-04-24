using System.Collections;
using System.Text.Json;

namespace PaulQpro.QPack;

public class Decoder{
    private List<DirEntry> dirs;
	private List<FileEntry> files;

	public string Source { get; }

    public void Decode(){
        using (BinaryReader br = new (File.OpenRead(Source))){
            int dirC = br.ReadInt32();
            for (int i = 0; i < dirC; i++){
                string name = "";
                int nameC = br.ReadInt32();
                foreach(char c in br.ReadChars(nameC)){
                    name += c;
                }
                int[] cDirs = new int[br.ReadInt32()];
                for(int j = 0; j < cDirs.Length; j++){
                    cDirs[j] = br.ReadInt32();
                }
                int[] cFiles = new int[br.ReadInt32()];
                for(int j = 0; j < cFiles.Length; j++){
                    cFiles[j] = br.ReadInt32();
                }
                int parent = br.ReadInt32();
                dirs.Add(new(name, cDirs, cFiles, parent, (parent == -1) ? name : $"dirs[parent].Path/{name}"));
            }
            while (br.BaseStream.Position < br.BaseStream.Length){
                string name = "";
                int nameC = br.ReadInt32();
                foreach(char c in br.ReadChars(nameC)){
                    name += c;
                }
                int len = br.ReadInt32();
                byte[] data = br.ReadBytes(len);
                int parent = br.ReadInt32();
                files.Add(new(name,data,parent));
            }
        }
        Console.WriteLine(JsonSerializer.Serialize(dirs.ToArray()));
        Console.WriteLine(JsonSerializer.Serialize(files.ToArray()));
        //DecodeRecursive(-1);
    }

    private void DecodeRecursive(int dirID){

    }

    public Decoder(string source){
        Source = source;

        dirs = new();
        files = new();
    }
}