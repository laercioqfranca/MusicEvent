using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MusicEvent.Infra.Data.StoredProcedureRepositoryExtension
{
    public static class StoredProcedureRepositoryExtensions
    {
        public static DbCommand LoadSqlQuery(this DbContext context, string query)
        {
            var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            command.CommandType = System.Data.CommandType.Text;
            return command;
        }
        public static DbCommand LoadStoredProcedure(this DbContext context, string storedProcName)
        {
            var command = context.Database.GetDbConnection().CreateCommand();
            //command.CommandTimeout = 0;
            command.CommandText = storedProcName;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            return command;
        }

        public static DbCommand WithSqlParams(this DbCommand command, params(string, object)[] nameValues)
        {
            foreach(var pair in nameValues)
            {
                var param = command.CreateParameter();
                param.ParameterName = pair.Item1;
                param.Value = pair.Item2 ?? DBNull.Value;
                command.Parameters.Add(param);
            }

            return command;
        }

        public static IList<T> ExecuteStoredProcedure<T>(this DbCommand command)
            where T : class
        {
            using (command)
            {
                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    command.Connection.Open();
                try
                {
                    using(var reader = command.ExecuteReader())
                    {
                        return reader.MapToList<T>();
                    }
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }

        public static async Task<IList<T>> ExecuteStoredProcedureAsync<T>(this DbCommand command)
            where T : class
        {
            using (command)
            {
                if (command.Connection.State == System.Data.ConnectionState.Closed)
                    await command.Connection.OpenAsync();
                try
                {
                    using (var reader = command.ExecuteReader())
                    {
                        return reader.MapToList<T>();
                    }
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }

        private static IList<T> MapToList<T>(this DbDataReader dr)
        {
            var objList = new List<T>();
            var props = typeof(T).GetRuntimeProperties();

            var colMapping = dr.GetColumnSchema()
                .Where(x => props.Any(y => y.Name.ToLower() == x.ColumnName.ToLower()))
                .ToDictionary(key => key.ColumnName.ToLower());

            if (dr.HasRows)
            {
                //bool firstRead = dr.Read();
                while (dr.Read())
                {
                    T obj = Activator.CreateInstance<T>();
                    foreach(var prop in props)
                    {
                        var val = dr.GetValue(colMapping[prop.Name.ToLower()].ColumnOrdinal.Value);
                        prop.SetValue(obj, val == DBNull.Value ? null : val);
                    }
                    objList.Add(obj);
                    //firstRead = false;
                }
            }
            return objList;
        }
    }
}
