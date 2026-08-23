using Corona.Components.Models;

namespace DysonModels;

public class MenuItens
{
    public static IReadOnlyList<CoronaNavItem> Items { get; } =[ new CoronaNavItem() { Text = "Home", Url = "/" } ];
}