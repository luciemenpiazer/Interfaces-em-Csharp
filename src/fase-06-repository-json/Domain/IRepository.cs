using System.Collections.Generic;

namespace Fase06.Domain
{
    // O contrato permanece idêntico às fases anteriores
    public interface IRepository<T, TId>
    {
        T Add(T entity);
        T? GetById(TId id);
        IReadOnlyList<T> ListAll();
        bool Update(T entity);
        bool Remove(TId id);
    }
}