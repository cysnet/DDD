using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Bbing.Domain.BaseModel
{
    public interface IEntity
    {
        string ObjId { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        DateTime CreateTime { get; set; }

        bool IsDelete { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        DateTime LastModifyTime { get; set; }
    }
}
