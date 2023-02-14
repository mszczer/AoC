using System;
using System.Collections.Generic;
using System.Linq;
using AoC.AoC2022.Common;

namespace AoC.AoC2022
{
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
            indent += lastNode ? " " : "| ";

            for (var i = 0; i < fileTreeElement.Count; i++)
                PrintTree(fileTreeElement.GetAllChildren()[i], indent, i == fileTreeElement.Count - 1);
        }

        private static FileTreeNode GetFilesTree(List<string> terminalOutput)
        {
            var rootNode = new FileTreeNode("/", true);
            var currentFolder = rootNode;

            foreach (var row in terminalOutput)
                if (row[0] == '$')
                    currentFolder = ExecuteCommand(row, rootNode, currentFolder);
                else
                    AddFileTreeElement(row, currentFolder);

            UpdateDirectorySize(rootNode);

            return rootNode;
        }

        private static void UpdateDirectorySize(FileTreeNode fileTreeElement)
        {
            var fileTreeElementSize = GetFileTreeNodeSize(fileTreeElement);
        }

        private static int GetFileTreeNodeSize(FileTreeNode fileTreeElement)
        {
            if (!fileTreeElement.IsDirectory) return fileTreeElement.Size;

            //foreach (var child in fileTreeElement.GetAllChildren())
            //    elementTotalSize += GetFileTreeNodeSize(child);
            var elementTotalSize = fileTreeElement.GetAllChildren().Sum(GetFileTreeNodeSize);

            fileTreeElement.Size = elementTotalSize;

            return elementTotalSize;
        }

        private static void AddFileTreeElement(string command, FileTreeNode currentFolder)
        {
            const string directory = "dir";

            var splitCommand = command.Split(" ");
            if (!currentFolder.ChildExists(splitCommand[1]))
            {
                var isDirectory = splitCommand[0] == directory;
                var newNode = new FileTreeNode(splitCommand[1], isDirectory)
                {
                    Parent = currentFolder
                };
                currentFolder.AddChild(newNode);
                if (!isDirectory)
                    newNode.Size = int.Parse(splitCommand[0]);
            }
        }

        private static FileTreeNode ExecuteCommand(string command, FileTreeNode rootFolder, FileTreeNode currentFolder)
        {
            const string changeDirOutermost = "$ cd /";
            const string changeDirOut = "$ cd ..";
            const string changeDirIn = "$ cd ";

            var newCurrentFolder = currentFolder;

            if (command.StartsWith(changeDirOutermost)) newCurrentFolder = rootFolder;
            else if (command.StartsWith(changeDirOut)) newCurrentFolder = currentFolder.Parent;
            else if (command.StartsWith(changeDirIn)) newCurrentFolder = currentFolder.GetChild(command[5..]);

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
            if (!fileTreeElement.IsDirectory) return totalSize;

            foreach (var child in fileTreeElement.GetAllChildren())
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
            if (fileTreeElement.IsDirectory)
            {
                directorySizeList.Add(fileTreeElement.Size);
                foreach (var child in fileTreeElement.GetAllChildren())
                    GetDirectorySizes(child, directorySizeList);
            }
        }
    }

    /*
     * Credits:
     *  https://stackoverflow.com/questions/9860207/build-a-simple-high-performance-tree-data-structure-in-c-sharp
     *  https://www.siepman.nl/blog/a-generic-tree-of-nodes-the-easy-way
     */
    internal class FileTreeNode
    {
        private readonly Dictionary<string, FileTreeNode> _children = new Dictionary<string, FileTreeNode>();
        public readonly string Name;
        public bool IsDirectory;
        public int Size;
        public FileTreeNode Parent { get; set; }

        public FileTreeNode(string name, bool isDirectory)
        {
            Name = name;
            Parent = null;
            IsDirectory = isDirectory;
            Size = 0;
        }

        public FileTreeNode GetChild(string name)
        {
            return _children[name];
        }

        public bool ChildExists(string name)
        {
            return _children.ContainsKey(name);
        }

        public void AddChild(FileTreeNode item)
        {
            if (item.Parent != null)
                item.Parent._children.Remove(item.Name);
            item.Parent = this;
            _children.Add(item.Name, item);
        }

        public List<FileTreeNode> GetAllChildren()
        {
            return _children.Values.ToList();
        }

        public int Count => _children.Count;
    }
}