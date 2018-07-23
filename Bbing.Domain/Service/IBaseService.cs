using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bbing.Domain.BaseModel;
using Bbing.Domain.IRepositories;

namespace Bbing.Domain.Service
{
    public interface IBaseService<TEntity> where TEntity : AggregateRoot
    {
    }
}
