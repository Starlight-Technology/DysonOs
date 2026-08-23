using System;
using System.Collections.Generic;
using System.Text;

namespace DysonHub.Shared.Services;

public sealed class DrawerStateService
{
    public bool IsOpen { get; private set; }
    public event Action? OnChange;

    public void Toggle()
    {
        IsOpen = !IsOpen;
        OnChange?.Invoke();
    }

    public void SetOpen(bool open)
    {
        IsOpen = open;
        OnChange?.Invoke();
    }
}
