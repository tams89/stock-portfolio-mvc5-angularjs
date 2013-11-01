using System.Collections.Generic;

namespace Core.Factory
{
    /// <summary>
    /// Factory for creating DTO objects.
    /// </summary>
    public class DtoFactory<T> where T : class, new()
    {
        public static T Create()
        {
            return new T();
        }

        public static IEnumerable<T> CreateList()
        {
            return new List<T>();
        }
    }
}
