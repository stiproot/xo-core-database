namespace Xo.Core.Database.ResourceAccess;

public class SqlCommandMapper : ISqlCommandMapper
{
    protected readonly IDictionary<string, string> _CommandHash = new Dictionary<string, string>();

    public SqlCommandMapper()
    {
        this.Load();
    }

    protected virtual void Load()
    {
        // example...
        // this._CommandHash.Add(typeof(InsertImageCommand).FullName, StoredProcedures.Images.InsertImage);
    }

    public ISqlInstruction Map(ICommand cmd)
    {
        Type commandTypeInfo = cmd.GetType();
        MemberInfo[] membersInfo = commandTypeInfo
            .GetMembers()
            .Where(m => m.MemberType == MemberTypes.Property && m.Name != nameof(ICommand.Result))
            .ToArray();

        if (this._CommandHash.TryGetValue(commandTypeInfo.FullName!, out var sqlCommand) is false)
        {
            throw new SqlCommandNotFoundException($"A cmd for key {commandTypeInfo.FullName} not found.");
        }

        var parameters = new DynamicParameters();
        foreach (var member in membersInfo)
        {
            var v = commandTypeInfo.GetProperty(member.Name)!.GetValue(cmd);
            parameters.Add($"@{member.Name.ToPostgresParameter()}", v);
        }

        return new SqlInstruction
        {
            Sql = sqlCommand,
            Parameters = parameters
        };
    }
}
