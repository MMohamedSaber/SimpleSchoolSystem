
using School.Core.Entities;
using System.Collections.Concurrent;

namespace School.Infrastructure.SharedStorage
{
    public static class EnrolledStorage
    {
        public static  readonly ConcurrentDictionary<string, Enrollment> _enrollment = new();

    }
}
