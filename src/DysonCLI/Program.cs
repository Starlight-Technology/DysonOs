using DysonDrive.Models;
using DysonDrive.Services;

#if DEBUG
while(true)
{
    Console.WriteLine("Enter the directory path to scan:");
    string path = Console.ReadLine() ?? "";
    var result = FileScannerService.ScanDirectory(path);
    PrintFileNode(result);
}

static void PrintFileNode(FileNodeModel node, int indent = 0)
{
    if(node is null)
        return;
    string indentString = new(' ', indent * 2);
    Console.WriteLine($"{indentString}- {node.Name} (Directory: {node.IsDirectory}, Size: {node.Size}, Last Modified: {node.LastModified})");
    foreach (var child in node.Children)
    {
        PrintFileNode(child, indent + 1);
    }
}
#else
Console.WriteLine("This application is only available in DEBUG mode.");
#endif