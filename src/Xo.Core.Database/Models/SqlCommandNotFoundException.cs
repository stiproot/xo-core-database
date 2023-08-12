using System.Runtime.Serialization;

namespace Xo.Core.Database.Models;

[Serializable]
public class SqlCommandNotFoundException : Exception
{
    public SqlCommandNotFoundException() { }

    public SqlCommandNotFoundException(string message) : base(message) { }

    public SqlCommandNotFoundException(string message, Exception inner) : base(message, inner) { }

    protected SqlCommandNotFoundException(
        SerializationInfo info,
        StreamingContext context) : base(info, context) { }
}
