using System;
using System.Collections.Generic;
using System.Text;

namespace DysonContext.Entities;

public class FileNodeEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string FullPath { get; set; } = "";
    public bool IsDirectory { get; set; }
    public long? Size { get; set; }
    public DateTime LastModified { get; set; }
    public List<FileNodeEntity> Children { get; set; } = new();
    public bool Accessible { get; set; } = true;
    public string? Error { get; set; }
}
