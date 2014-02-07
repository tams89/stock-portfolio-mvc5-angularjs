using Core.Models.HFT;
using System;

namespace ORM.Model
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Tick> TickRepository { get; }
        void Save();
    }
}
