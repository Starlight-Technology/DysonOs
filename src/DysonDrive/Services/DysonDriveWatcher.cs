using System.Collections.Concurrent;

namespace DysonDrive.Services;

public class DysonDriveWatcher
{
    private readonly FileSystemWatcher _watcher;
    private readonly ConcurrentDictionary<string, DateTime> _eventCache = new();

    public event Action<string>? OnCreated;
    public event Action<string>? OnChanged;
    public event Action<string>? OnDeleted;
    public event Action<string, string>? OnRenamed;

    public DysonDriveWatcher(string path)
    {
        _watcher = new FileSystemWatcher(path)
        {
            IncludeSubdirectories = true,
            Filter = "*.*",
            NotifyFilter = NotifyFilters.FileName |
                           NotifyFilters.DirectoryName |
                           NotifyFilters.LastWrite
        };

        _watcher.Created += (s, e) => HandleEvent(e.FullPath, () => OnCreated?.Invoke(e.FullPath));
        _watcher.Changed += (s, e) => HandleEvent(e.FullPath, () => OnChanged?.Invoke(e.FullPath));
        _watcher.Deleted += (s, e) => HandleEvent(e.FullPath, () => OnDeleted?.Invoke(e.FullPath));
        _watcher.Renamed += (s, e) => HandleEvent(e.FullPath, () => OnRenamed?.Invoke(e.OldFullPath, e.FullPath));

        _watcher.EnableRaisingEvents = true;
    }

    private void HandleEvent(string path, Action action)
    {
        var now = DateTime.Now;

        // Debounce: evita eventos duplicados
        if (_eventCache.TryGetValue(path, out var lastEvent))
        {
            if ((now - lastEvent).TotalMilliseconds < 200)
                return;
        }

        _eventCache[path] = now;
        action();
    }
}
