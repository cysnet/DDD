using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bbing.DtoModel.BaseData
{
    public class Loginer
    {
        public string UserName { get; set; }

        public Loginer()
        {
        }

        public Loginer(string userName)
        {
            this.UserName = userName;
        }
    }
}
