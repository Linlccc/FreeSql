using System;
using System.Collections.Generic;
using FreeSql.DataAnnotations;

namespace Model;

public class User
{
    [Column(IsPrimary = true)] public int Id { get; set; }

    public int GroupId { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public DateTime CreatedAt { get; set; }

    public static List<User> GetSample()
    {
        return
        [
            new() { Id = 1, GroupId = 1, Name = "Alice", Email = "Alice@test.com", CreatedAt = DateTime.Now },
            new() { Id = 2, GroupId = 1, Name = "Bob", Email = "Bob@test.com", CreatedAt = DateTime.Now },
            new() { Id = 3, GroupId = 2, Name = "Charlie", Email = "Charlie@test.com", CreatedAt = DateTime.Now },
            new() { Id = 4, GroupId = 2, Name = "David", Email = "David@test.com", CreatedAt = DateTime.Now }
        ];
    }
}

public class Group
{
    [Column(IsPrimary = true)] public int Id { get; set; }

    public string Name { get; set; }

    public static List<Group> GetSample()
    {
        return
        [
            new() { Id = 1, Name = "Group A" },
            new() { Id = 2, Name = "Group B" }
        ];
    }
}