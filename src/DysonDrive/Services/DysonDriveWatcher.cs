using System.Collections.Concurrent;

namespace DysonDrive.Services;

public class DysonDriveWatcher
{
    private readonly ConcurrentDictionary<string, DateTime> _eventCache = new();

    public event Action<string>? OnCreated;
    public event Action<string>? OnChanged;
    public event Action<string>? OnDeleted;
    public event Action<string, string>? OnRenamed;

    public DysonDriveWatcher(string path)
    {
        FileSystemWatcher watcher = new(path)
        {
            IncludeSubdirectories = true,
            Filter = "*.*",
            NotifyFilter = NotifyFilters.FileName |
                           NotifyFilters.DirectoryName |
                           NotifyFilters.LastWrite
        };

        watcher.Created += (s, e) => HandleEvent(e.FullPath, () => OnCreated?.Invoke(e.FullPath));
        watcher.Changed += (s, e) => HandleEvent(e.FullPath, () => OnChanged?.Invoke(e.FullPath));
        watcher.Deleted += (s, e) => HandleEvent(e.FullPath, () => OnDeleted?.Invoke(e.FullPath));
        watcher.Renamed += (s, e) => HandleEvent(e.FullPath, () => OnRenamed?.Invoke(e.OldFullPath, e.FullPath));

        watcher.EnableRaisingEvents = true;
    }

    private void HandleEvent(string path, Action action)
    {
        var now = DateTime.Now;

        // Debounce: evita eventos duplicados
        if (_eventCache.TryGetValue(path, out var lastEvent) && (now - lastEvent).TotalMilliseconds < 200)
            return;

        _eventCache[path] = now;
        action();
    }
}
