using School.Core.Entities;
using System.Collections.Concurrent;

public static class MarkStorage
{
    public static readonly ConcurrentDictionary<string, Mark> Marks = new();
}