namespace AoC.AoC2022;

internal class Day07 : AoC<List<string>, int, int>
{
    private readonly FileTreeNode _rootNode;

    public Day07(string dayName) : base(dayName)
    {
        _rootNode = GetFilesTree(InputData);
    }

    public void PrintFilesTree()
    {
        PrintTree(_rootNode, "", true);
    }

    private static void PrintTree(FileTreeNode fileTreeElement, string indent, bool lastNode)
    {
        Console.WriteLine(indent + "- " + fileTreeElement.Name);
        indent += lastNode ? "   " : "|  ";

        var children = fileTreeElement.GetChildren().ToList();
        for (var i = 0; i < children.Count; i++)
            PrintTree(children[i], indent, i == children.Count - 1);
    }

    private static FileTreeNode GetFilesTree(List<string> terminalOutput)
    {
        var rootNode = new FileTreeNode("/", true);
        var currentFolder = rootNode;

        if (terminalOutput == null)
            return rootNode;

        foreach (var row in terminalOutput)
        {
            if (string.IsNullOrWhiteSpace(row))
                continue;

            if (row[0] == '$')
                currentFolder = ExecuteCommand(row, rootNode, currentFolder);
            else
                AddFileTreeElement(row, currentFolder);
        }

        UpdateDirectorySizes(rootNode);

        return rootNode;
    }

    private static void UpdateDirectorySizes(FileTreeNode fileTreeElement)
    {
        if (fileTreeElement == null) return;
        GetFileTreeNodeSize(fileTreeElement);
    }

    private static int GetFileTreeNodeSize(FileTreeNode fileTreeElement)
    {
        if (!fileTreeElement.IsDirectory) return fileTreeElement.Size;

        var elementTotalSize = fileTreeElement.GetChildren().Sum(GetFileTreeNodeSize);

        fileTreeElement.Size = elementTotalSize;

        return elementTotalSize;
    }

    private static void AddFileTreeElement(string command, FileTreeNode currentFolder)
    {
        const string directory = "dir";

        if (string.IsNullOrWhiteSpace(command) || currentFolder == null) return;

        var splitCommand = command.Split(" ", 2, StringSplitOptions.RemoveEmptyEntries);
        if (splitCommand.Length < 2) return;

        var name = splitCommand[1];
        if (!currentFolder.ChildExists(name))
        {
            var isDirectory = splitCommand[0] == directory;
            var newNode = new FileTreeNode(name, isDirectory)
            {
                Parent = currentFolder
            };
            currentFolder.AddChild(newNode);
            if (!isDirectory && int.TryParse(splitCommand[0], out var fileSize))
                newNode.Size = fileSize;
        }
    }

    private static FileTreeNode ExecuteCommand(string command, FileTreeNode rootFolder, FileTreeNode currentFolder)
    {
        const string changeDirOutermost = "$ cd /";
        const string changeDirOut = "$ cd ..";
        const string changeDirIn = "$ cd ";

        var newCurrentFolder = currentFolder ?? rootFolder;

        if (string.IsNullOrWhiteSpace(command))
            return newCurrentFolder;

        if (command.StartsWith(changeDirOutermost))
        {
            newCurrentFolder = rootFolder;
        }
        else if (command.StartsWith(changeDirOut))
        {
            newCurrentFolder = currentFolder?.Parent ?? currentFolder ?? rootFolder;
        }
        else if (command.StartsWith(changeDirIn))
        {
            var targetName = command.Length > changeDirIn.Length ? command[changeDirIn.Length..] : string.Empty;
            if (string.IsNullOrEmpty(targetName)) return newCurrentFolder;

            if (!currentFolder.TryGetChild(targetName, out var child))
            {
                // If directory wasn't listed before, create it to keep tree consistent.
                child = new FileTreeNode(targetName, true) { Parent = currentFolder };
                currentFolder.AddChild(child);
            }

            newCurrentFolder = child;
        }

        return newCurrentFolder;
    }


    public override int CalculatePart1()
    {
        return SumOfTotalSizesOfDirectoriesBelowThreshold(_rootNode, 100000, 0);
    }


    // Depth first search: https://en.wikipedia.org/wiki/Depth-first_search
    // If the node is not a directory, then return the Total Size
    // For each of the children of the current node, run method again and store the result in Total Size
    // If the node size is less than the threshold, then return the size of this node + Total Size, otherwise just return Total Size
    private static int SumOfTotalSizesOfDirectoriesBelowThreshold(FileTreeNode fileTreeElement, int threshold,
        int totalSize)
    {
        if (fileTreeElement == null || !fileTreeElement.IsDirectory) return totalSize;

        foreach (var child in fileTreeElement.GetChildren())
            totalSize = SumOfTotalSizesOfDirectoriesBelowThreshold(child, threshold, totalSize);

        if (fileTreeElement.Size <= threshold)
            return fileTreeElement.Size + totalSize;

        return totalSize;
    }

    public override int CalculatePart2()
    {
        const int diskSpaceAvailable = 70000000;
        const int unusedSpaceExpected = 30000000;
        var unusedSpaceAvailable = diskSpaceAvailable - _rootNode.Size;
        var unusedSpaceRequired = unusedSpaceExpected - unusedSpaceAvailable;

        return SizeOfSmallestDirectoryToFreeUpEnoughSpace(_rootNode, unusedSpaceRequired);
    }

    private static int SizeOfSmallestDirectoryToFreeUpEnoughSpace(FileTreeNode fileTreeElement, int threshold)
    {
        if (fileTreeElement == null) return 0;

        var directorySizeList = new List<int>();
        GetDirectorySizes(fileTreeElement, directorySizeList);

        directorySizeList.Sort();

        foreach (var size in directorySizeList)
            if (size >= threshold)
                return size;

        return fileTreeElement.Size;
    }

    private static void GetDirectorySizes(FileTreeNode fileTreeElement, ICollection<int> directorySizeList)
    {
        if (fileTreeElement == null) return;

        if (fileTreeElement.IsDirectory)
        {
            directorySizeList.Add(fileTreeElement.Size);
            foreach (var child in fileTreeElement.GetChildren())
                GetDirectorySizes(child, directorySizeList);
        }
    }
}

/*
 * Credits:
 *  https://stackoverflow.com/questions/9860207/build-a-simple-high-performance-tree-data-structure-in-c-sharp
 *  https://www.siepman.nl/blog/a-generic-tree-of-nodes-the-easy-way
 */
internal class FileTreeNode(string name, bool isDirectory)
{
    private readonly Dictionary<string, FileTreeNode> _children = new();
    public string Name { get; } = name;
    public bool IsDirectory = isDirectory;
    public int Size;
    public FileTreeNode Parent { get; set; }

    public FileTreeNode GetChild(string name)
    {
        return _children[name];
    }

    public bool TryGetChild(string name, out FileTreeNode child)
    {
        return _children.TryGetValue(name, out child);
    }

    public bool ChildExists(string name)
    {
        return _children.ContainsKey(name);
    }

    public void AddChild(FileTreeNode item)
    {
        if (item == null) return;
        item.Parent?._children.Remove(item.Name);
        item.Parent = this;
        _children[item.Name] = item;
    }

    public IReadOnlyCollection<FileTreeNode> GetChildren()
    {
        return _children.Values;
    }

    public int Count => _children.Count;
}