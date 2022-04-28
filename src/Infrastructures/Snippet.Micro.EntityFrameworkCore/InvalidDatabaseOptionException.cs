namespace Snippet.Micro.EntityFrameworkCore
{
    public class InvalidDatabaseOptionException : Exception
    {
        public InvalidDatabaseOptionException() : base("错误的数据库配置!") { }
    }
}
