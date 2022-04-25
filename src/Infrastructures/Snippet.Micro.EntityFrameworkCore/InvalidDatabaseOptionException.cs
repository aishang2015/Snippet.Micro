using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snippet.Micro.EntityFrameworkCore
{
    public class InvalidDatabaseOptionException:Exception
    {
        public InvalidDatabaseOptionException() : base("错误的数据库配置!") { }
    }
}
