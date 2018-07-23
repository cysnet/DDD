using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bbing.DtoModel.ResponseData
{
    public class OutPutData
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public object Data { get; set; }
        public static OutPutData NewOutPutData(string message = null, bool success = true, object data = null) => new OutPutData() { Message = message, Success = success, Data = data };
    }
}
