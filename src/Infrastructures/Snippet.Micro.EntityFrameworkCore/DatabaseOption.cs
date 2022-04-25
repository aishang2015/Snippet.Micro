namespace Snippet.Micro.EntityFrameworkCore
{
    public class DatabaseOption
    {
        /// <summary>
        /// 数据库类型,可使用的值有SQLite,SQLServer,MySQL,PostgreSQL,Oracle,InMemory
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 数据库版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 连接串
        /// </summary>
        public string Connection { get; set; }
    }
}