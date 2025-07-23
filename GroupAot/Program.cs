using Model;

// aot 发布
// dotnet publish -r osx-arm64 -c Release -o ./bin/publish

DbHelper.Executing = cmd => { };
IFreeSql db = DbHelper.DbSqlite;

// 迁移表，添加初始化数据
db.CodeFirst.SyncStructure<User>();
db.CodeFirst.SyncStructure<Group>();
if (!db.Select<User>().Any()) db.Insert(User.GetSample()).ExecuteAffrows();
if (!db.Select<Group>().Any()) db.Insert(Group.GetSample()).ExecuteAffrows();
Console.WriteLine();

var v1 = db.Select<User>().GroupBy(u => u.GroupId)
    .ToList(g => new { GroupId = g.Key, Count = g.Count() });
Console.WriteLine($"v1\t {string.Join(", ", v1.Select(g => $"GroupId: {g.GroupId}, Count: {g.Count}"))}");

// 在 aot 模式下 3.5.210 版本会报错
Dictionary<int, int>? v2 = db.Select<User>().GroupBy(u => u.GroupId)
    .ToDictionary(g => g.Count());
Console.WriteLine($"v2\t {string.Join(", ", v2.Select(g => $"GroupId: {g.Key}, Count: {g.Value}"))}");

Console.WriteLine($"Hello, World!\t{DateTime.Now}");