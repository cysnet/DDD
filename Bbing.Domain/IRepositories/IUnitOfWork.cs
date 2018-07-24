using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bbing.Domain.IRepositories
{
    public interface IUnitOfWork
    {
        bool IsCommitted { get; set; }

        int Commit();

        void Rollback();
    }
}
