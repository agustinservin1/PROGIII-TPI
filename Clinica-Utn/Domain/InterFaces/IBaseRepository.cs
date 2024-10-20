using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.InterFaces
{
    public interface IBaseRepository<T> where T : class

    {
        T? GetById<TId>(TId id);
        IEnumerable<T> GetAll();
        T Update(T entity);
        T Delete(T entity);
        T Create(T entity);
    }
}
