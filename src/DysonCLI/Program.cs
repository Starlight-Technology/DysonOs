using DysonDrive.Models;
using DysonDrive.Services;

using System.Runtime.CompilerServices;

var fileService = new FileScannerService();

while(true)
{
    Console.WriteLine("Enter the directory path to scan:");
    string path = Console.ReadLine() ?? "";
    var result = fileService.ScanDirectory(path);
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