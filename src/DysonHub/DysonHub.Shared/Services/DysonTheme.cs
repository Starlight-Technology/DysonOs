using Corona.Theming;

using System;
using System.Collections.Generic;
using System.Text;

namespace DysonHub.Shared.Services;

public static class DysonTheme
{
    public static CoronaThemeOverrides Overrides { get; } = new CoronaThemeOverrides
    {
        Primitive = new CoronaPrimitiveTokenOverrides(),
        Semantic = new CoronaSemanticTokenOverrides(),
    };
}
