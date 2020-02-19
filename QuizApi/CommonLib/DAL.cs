using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

namespace QuizApi.CommonLib
{
  public enum ParamType
  {
    Numeric = 0,
    Varchar = 1,
    Boolean = 2,
    Date = 3
  }

  public class DAL
  {
    public object ExecuteProcedureScalar(string procedureName, ICollection<Parameter> paramCollection, string connectionString)
    {
      using (SqlConnection Connection = new SqlConnection(connectionString))
      {
        using (SqlCommand cmd = new SqlCommand(procedureName, Connection))
        {
          cmd.CommandType = CommandType.StoredProcedure;
          foreach (Parameter param in paramCollection)
          {
            cmd.Parameters.Add(param.GetParam());
          }
          
          cmd.Connection.Open();
          return cmd.ExecuteScalar();
        }
      }
    }

    public int ExecuteNonQuerySql(string sqlText, string connectionString)
    {
      using (SqlConnection Connection = new SqlConnection(connectionString))
      {
        using (SqlCommand cmd = new SqlCommand(sqlText, Connection))
        {
          cmd.CommandType = CommandType.Text;
          cmd.Connection.Open();
          return cmd.ExecuteNonQuery();
        }
      }
    }

    public int ExecuteProcedure(string procedureName, ICollection<Parameter> paramCollection, string connectionString)
    {
        using (SqlConnection Connection = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(procedureName, Connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (Parameter param in paramCollection)
                {
                    cmd.Parameters.Add(param.GetParam());
                }
                cmd.Connection.Open();
                return cmd.ExecuteNonQuery();
            }
        }
    }

    public DataTable ExecuteCommand(string sqlCommand, string connectionString)
    {
      DataTable dtResult = new DataTable();
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        using (SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCommand, connection))
        {
          sqlDA.Fill(dtResult);
          return dtResult;
        }
      }
    }

    
  }

  public class Parameter
  {
    SqlParameter _param;
    public Parameter(string name, ParamType paramType, object value)
    {
      _param = new SqlParameter(name, value);
      if (paramType == ParamType.Varchar)
        _param.SqlDbType = SqlDbType.VarChar;
      else if (paramType == ParamType.Numeric)
        _param.SqlDbType = SqlDbType.Int;
      else if (paramType == ParamType.Boolean)
        _param.SqlDbType = SqlDbType.Bit;
      else
        throw new Exception("Sql Parameter currenty not supported");
      _param.SqlValue = value;
    }

    public SqlParameter GetParam() 
    { 
        return _param;
    }
  }
}
