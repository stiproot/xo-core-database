using System.Text.RegularExpressions;

namespace Xo.Core.Database.Extensions;

public static partial class StringExtensions
{
    public static string ToSnakeCase(this string @this)
    {
        return Regex.Replace(@this, "[A-Z]", "_$0")
            .ToLower()
            .Remove(0, 1);
    }

    public static string ToPostgresParameter(this string @this)
        => $"p_{@this.ToSnakeCase()}";
}
