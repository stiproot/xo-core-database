namespace Xo.Core.Database.Abstractions;

public interface ISqlQueryMapper
{
    ISqlInstruction Map(IBaseQuery qry);
    ISqlInstruction Map<TKey>(TKey key) where TKey : struct;
}
