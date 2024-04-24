namespace PaulQpro.QPack;

public static class Program
{
	public static void Main(string[] args){
		// Encoder encoder = new(".");
		// encoder.Encode();
		// encoder.Save();
		Decoder decoder = new("Pack.qpack");
		decoder.Decode();
	}
}
