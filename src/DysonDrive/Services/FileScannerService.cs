using DysonDrive.Models;

namespace DysonDrive.Services;

public class FileScannerService
{
    public FileNodeModel ScanDirectory(string path)
    {
        var rootInfo = new DirectoryInfo(path);

        var rootNode = new FileNodeModel
        {
            Name = rootInfo.Name,
            FullPath = rootInfo.FullName,
            IsDirectory = true,
            LastModified = rootInfo.LastWriteTime
        };

        ScanRecursive(rootInfo, rootNode);

        return rootNode;
    }

    private void ScanRecursive(DirectoryInfo dirInfo, FileNodeModel parentNode)
    {
        try
        {
            // Pastas
            Parallel.ForEach(dirInfo.GetDirectories(), directory =>
            {
                var node = new FileNodeModel
                {
                    Name = directory.Name,
                    FullPath = directory.FullName,
                    IsDirectory = true,
                    LastModified = directory.LastWriteTime
                };

                parentNode.Children.Add(node);

                ScanRecursive(directory, node);
            });

            // Arquivos
            Parallel.ForEach(dirInfo.GetFiles(), file =>
            {
                var node = new FileNodeModel
                {
                    Name = file.Name,
                    FullPath = file.FullName,
                    IsDirectory = false,
                    Size = file.Length,
                    LastModified = file.LastWriteTime
                };

                parentNode.Children.Add(node);
            });
        }
        catch (Exception ex)
        {
            parentNode.Accessible = false;
            parentNode.Error = ex.Message;
        }
    }
}
