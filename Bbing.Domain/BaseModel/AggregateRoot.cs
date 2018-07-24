using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bbing.Domain.BaseModel
{
    public abstract class AggregateRoot : IAggregateRoot
    {
        [NotMapped]
        public string ObjId { get; set; }

        [Key]
        [BsonIgnore]
        public int ID { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; }

        public bool IsDelete { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime LastModifyTime { get; set; }


    }
}
