using System.Collections.Generic;
using System.Data;

namespace ORM.Model
{
    public interface IReadOnlyRepository<T> where T : class, IEntity
    {
        IDbConnection Connection { get; }
        T FindById(int id);
        IEnumerable<T> GetAll();
    }
}
