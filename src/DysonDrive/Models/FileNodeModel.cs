using System;
using System.Collections.Generic;
using System.Text;

namespace DysonDrive.Models;

public class FileNodeModel
{
    public string Name { get; set; } = "";
    public string FullPath { get; set; } = "";
    public bool IsDirectory { get; set; }
    public long? Size { get; set; }
    public DateTime LastModified { get; set; }
    public List<FileNodeModel> Children { get; set; } = new();
    public bool Accessible { get; set; } = true;
    public string? Error { get; set; }
}
