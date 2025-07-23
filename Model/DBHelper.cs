using System;
using System.Data.Common;
using System.IO;
using FreeSql;

namespace Model;

public static class DbHelper
{
    /// <summary>
    /// 执行 SQL 命令前的回调
    /// </summary>
    public static Action<DbCommand>? Executing;

    /// <summary>
    /// 执行 SQL 命令后的回调
    /// </summary>
    public static Action<DbCommand, string>? Executed;

    public static string WorkDir => AppDomain.CurrentDomain.BaseDirectory;
    public static string EntryName => AppDomain.CurrentDomain.FriendlyName;

    private static readonly Lazy<IFreeSql> LazySqlite;
    public static IFreeSql DbSqlite => LazySqlite.Value;

    static DbHelper()
    {
        CoreErrorStrings.Language = "cn";

        LazySqlite = new Lazy<IFreeSql>(() => new FreeSqlBuilder()
            .UseConnectionString(DataType.Sqlite, $"Data Source={Path.Combine(WorkDir, $"{EntryName}.db")};")
            .UseNoneCommandParameter(true)
            .UseMonitorCommand(OnExecuting, OnExecuted)
            .Build());
    }

    private static void OnExecuting(DbCommand cmd)
    {
        if (Executing is not null) Executing.Invoke(cmd);
        else Console.WriteLine($"----SQL----{Environment.NewLine}{cmd.CommandText}{Environment.NewLine}-----------");
    }

    private static void OnExecuted(DbCommand cmd, string log)
    {
        Executed?.Invoke(cmd, log);
    }
}