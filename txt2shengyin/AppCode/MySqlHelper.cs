using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

public static class MySqlHelper
{
    private static MySqlConnection _connenction;
    private static MySqlConnection GetConnection()
    {
        if (_connenction == null)
        {
            _connenction = new MySqlConnection(ZmConfig.MysqlConnet);
        }
        if (_connenction.State != ConnectionState.Open)
        {
            _connenction.Open();
        }
        return _connenction;
    }
    private static List<MySqlParameter> ConvertParam(List<ZmParameter> parameters)
    {
        List<MySqlParameter> mysqlparams = new List<MySqlParameter>();
        if (parameters!=null&& parameters.Count>0)
        {
            foreach (ZmParameter item in parameters)
            {
                mysqlparams.Add(new MySqlParameter("@"+item.name, item.value));
            }
        }
        return mysqlparams;
    }
    private static string ConvertSqlStr( string sql)
    {
        return sql.Replace("$_", "@");
    }
    #region 执行SQL语句,返回受影响行数
    public static int ExecuteNonQuery(string sql)
    {
        int x = 0;
        using (MySqlConnection conn = GetConnection())
        {
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                x = cmd.ExecuteNonQuery();
            }
        }
        return x;
    }
    public static int ExecuteNonQuery(string sql, List<ZmParameter> parameters)
    {
        List<MySqlParameter> mysqlparams = ConvertParam(parameters);
        using (MySqlConnection conn = GetConnection())
        {
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = ConvertSqlStr(sql);
                cmd.Parameters.AddRange(mysqlparams.ToArray());
                return cmd.ExecuteNonQuery();
            }
        }
    }
    #endregion
    #region 执行SQL语句,返回DataTable;只用来执行查询结果比较少的情况
    public static DataTable ExecuteDataTable(string sql)
    {
        using (MySqlConnection conn = GetConnection())
        {
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable datatable = new DataTable();
                adapter.Fill(datatable);
                return datatable;
            }
        }
    }
    public static DataTable ExecuteDataTable(string sql, List<ZmParameter> parameters)
    {
        List<MySqlParameter> mysqlparams = ConvertParam(parameters);
        using (MySqlConnection conn = GetConnection())
        {
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = ConvertSqlStr(sql); 
                cmd.Parameters.AddRange(mysqlparams.ToArray());
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable datatable = new DataTable();
                adapter.Fill(datatable);
                return datatable;
            }
        }
    }
    #endregion
    #region 插入表
    /// <summary>
    /// 插入单表数据
    /// </summary>
    /// <param name="tabname"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public static int InsertTab(string tabname, List<ZmParameter> parameters)
    {
        //拼接sql
        string i_sql_p = "Insert into " + tabname + "(";
        string i_sql_b = " ) values ( $_";
        List<string> cols = new List<string>();
        foreach (ZmParameter dc in parameters)
        {
            cols.Add(dc.name);

        }
        i_sql_p += string.Join(",", cols.ToArray());
        i_sql_b += string.Join(",$_", cols.ToArray());
        string i_sql = i_sql_p + i_sql_b + ")";
        return ExecuteNonQuery(i_sql,parameters);
    }
    #endregion
}