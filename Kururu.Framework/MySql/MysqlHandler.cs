using System;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections.Generic;
using Kururu.Framework;
using MySql.Data.MySqlClient;

namespace Kururu.Framework.MySql
{
	public class MysqlHandler
	{
		private MySqlConnection _connection;
		public List<GuildData> Servers;

		public MysqlHandler (MysqlConfig config)
		{
			_connection = new MySqlConnection($"server={config.DBHost};user id={config.DBUser};password={config.DBPassword};database={config.DBName};SslMode=none;charset=utf8;");
		}

        public async Task<List<T>> QueryData<T> (string Query)
        {
            await _connection.OpenAsync();
            MySqlCommand command = new MySqlCommand(Query, _connection);
            var reader = await command.ExecuteReaderAsync();
            var res = typeof(T).GetFields().Where(x => x.GetCustomAttribute<MySqlPropertyAttribute>() != null);
            List<T> instances = new List<T>();
            while (await reader.ReadAsync())
            {
                var instance = (T)Activator.CreateInstance(typeof(T));
                foreach (var item in res)
                {
                    object value = reader[item.Name];
                    if (value is DBNull)
                        continue;
                    try
                    {
                        
                        item.SetValue(instance, Convert.ChangeType(value, item.FieldType)); 
                    }
                    catch (InvalidCastException ex) 
                    {
                        throw ex;
                    }
                }
                instances.Add(instance);
            }
            await _connection.CloseAsync();
            return instances;
        }
        

		public async Task QueryData (string Query)
		{
			await _connection.OpenAsync();
			MySqlCommand Command = new MySqlCommand(Query, _connection);
            try
            {
			await Command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            await _connection.CloseAsync();
		}


	}
}
