using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();
        IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    }
}
