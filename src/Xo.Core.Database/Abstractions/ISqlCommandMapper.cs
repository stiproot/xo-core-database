namespace Xo.Core.Database.Abstractions;

public interface ISqlCommandMapper
{
    ISqlInstruction Map(ICommand cmd);
}
