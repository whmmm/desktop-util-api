using SqlSugar;

namespace tauri_api.Core;

/// <summary>
/// 建表：
/// https://www.donet5.com/home/doc?masterId=1&typeId=1206  4.1 特性列表
/// </summary>
public class SqlSugarHelper //不能是泛型类
{
    static readonly string dbName = Path.Combine(Environment.CurrentDirectory, "SampleDB.db");

    //如果是固定多库可以传 new SqlSugarScope(List<ConnectionConfig>,db=>{}) 文档：多租户
    //如果是不固定多库 可以看文档Saas分库

    //用单例模式
    public static readonly SqlSugarScope Db = new SqlSugarScope(new ConnectionConfig()
        {
            ConnectionString = $@"DataSource={SqlSugarHelper.dbName}", //连接符字串
            DbType = DbType.Sqlite, //数据库类型
            IsAutoCloseConnection = true, //不设成true要手动close
            ConfigureExternalServices = new ConfigureExternalServices()
            {
                // https://www.donet5.com/home/doc?masterId=1&typeId=1206
                // 驼峰命名法
                EntityService = (x, p) => //处理列名
                {
                    p.DbColumnName = UtilMethods.ToUnderLine(p.DbColumnName); //ToUnderLine驼峰转下划线方法
                },
                EntityNameService = (x, p) => //处理表名
                {
                    p.DbTableName = UtilMethods.ToUnderLine(p.DbTableName); //ToUnderLine驼峰转下划线方法
                }
            }
        },
        db =>
        {
            //(A)全局生效配置点
            //调试SQL事件，可以删掉
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql); //输出sql,查看执行sql
                //5.0.8.2 获取无参数化 SQL 
                //UtilMethods.GetSqlString(DbType.SqlServer,sql,pars)
            };
        });
}