using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public class DataContent
    {
        Dictionary<string, object> values = new Dictionary<string, object>();
    }
    public class Source : IDataSource
    {
        SqlConnection _connection = null;
    //    List<DataStore> _stores = new List<DataStore>();
        Dictionary<string, string> _queries = new Dictionary<string, string>();

        string _selectedQuery = string.Empty;
      //  DataStore _selectedStore = null;
            
        public Source(IConfigurationRoot configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("TestConnection"));
            //_queries.Add("lists", "select id,listName,listItem,listValue from lists");
            //List<string> fields = new List<string>();
            //fields.Add("listName");
            //fields.Add("listItem");
            //fields.Add("listValue");
            //_stores.Add(new DataStore("lists", fields));
        }

        //public bool Select( string name)
        //{
        //    _selectedQuery = _queries[name];
        //    if( string.IsNullOrWhiteSpace(_selectedQuery))
        //    {
        //        return false;
        //    }
        //    _selectedStore = _stores.FirstOrDefault();
        //    return _selectedStore != null;
        //}

        //public async Task<bool> SelectAsync(string name)
        //{
        //    _selectedQuery = _queries[name];
        //    if (string.IsNullOrWhiteSpace(_selectedQuery))
        //    {
        //        return false;
        //    }
        //    _selectedStore = _stores.FirstOrDefault();
        //    return _selectedStore != null;
        //}

        public void Query(IForm form)
        {
            SqlCommand cmd = new SqlCommand(form.Query(), _connection);
            try
            {
                _connection.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while(rdr.Read())
                {
                    form.SetValues(rdr);
                    //foreach( var k in _selectedStore.Store.Keys)
                    //{
                    //    _selectedStore.Store[k].Add(rdr[k]);
                    //}
                }
            }
            catch( Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public void Update(IForm form)
        {
            try
            {
                _connection.Open();
                string sql = form.Save();
                SqlCommand cmd = new SqlCommand(sql, _connection);
                for (int i = 0;  (sql = form.Save(i)).Length > 0; i++ )
                {
                    cmd.Parameters.Clear();
                    object v;
                    var values = form.Values(i);
                    foreach( var k in values.Keys)
                    {
                        if( values.TryGetValue(k, out v))
                        {
                            cmd.Parameters.AddWithValue("@" + k, v);
                        }
                    }
                    cmd.ExecuteNonQuery();
                }
                
              
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }
        public async void QueryAsync()
        {
            SqlCommand cmd = new SqlCommand(_selectedQuery, _connection);
            try
            {
                await _connection.OpenAsync();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (await rdr.ReadAsync())
                {
                    //foreach (var k in _selectedStore.Store.Keys)
                    //{
                    //    _selectedStore.Store[k].Add(rdr[k]);
                    //}
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        //    return _selectedStore;
        }
        public void Dispose()
        {
      //      _connection.Close();
            _connection.Dispose();
        }
    }
}
