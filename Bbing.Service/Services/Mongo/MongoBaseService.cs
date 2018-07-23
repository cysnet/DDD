using Bbing.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bbing.Domain.BaseModel;
using Bbing.Domain.IRepositories;

namespace Bbing.Service.Services.Mongo
{
    public abstract class MongoBaseService<TEntity> : IBaseService<TEntity> where TEntity : AggregateRoot
    {
    }
}
