


using System.Text.RegularExpressions;

while (true)
{
	Console.Write("Enter folder: ");
	string folder = Console.ReadLine() ?? "";
	Console.Write("Enter text: ");
	string searchingText = Console.ReadLine() ?? "";

	if (!Directory.Exists(folder))
	{
		Console.WriteLine("Folder does not exist.");
		return;
	}

	List<string> labelsAndButtons = new List<string>();
	string[] files = GetTscnFilePaths(folder);
	//var regex = new Regex("\\[node name = \"(.+?)\".*?text *= *\"(.*?)\"", RegexOptions.Multiline);
	//var regex = new Regex("\\[node name = \"(.+?)\"", RegexOptions.Multiline);
	//var regex = new Regex("\\[node name=\"(.+?)\" type=\"(Label|Button)\"[\\s\\S]*?text *= *\"(.+?)\"");
	//var regex = new Regex("text *= *\"(.+?)\"");
	var regex = new Regex(searchingText);
	int matchesCount = 0;
	foreach (string file in files)
	{
		string content = File.ReadAllText(file);
		MatchCollection matches = regex.Matches(content);
		if (matches.Count > 0)
		{
			matchesCount++;
			Console.WriteLine(file.Replace(folder + "\\", ""));
			for (int i = 0; i < matches.Count; i++)
				Console.WriteLine(matches[i].Groups[0]);
			Console.WriteLine("");
		}
	}

	if (matchesCount == 0)
		Console.WriteLine("Found no matches\n");
	else
		Console.WriteLine("============= End ==============\n");
}

string[] GetTscnFilePaths (string folder)
{
	List<string> files = new List<string>();

	string[] directiories = Directory.GetDirectories(folder);
	foreach (string directory in directiories)
		files.AddRange(GetTscnFilePaths(directory));

	foreach (string file in Directory.EnumerateFiles(folder, "*.tscn"))
	files.Add(file);

	return files.ToArray();
}

Console.ReadLine();