using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccess
{
    //public class Paging
    //{
    //    public int PageLength { get; set; }
    //    public int PageNumber { get; set; }

    //    public Paging(int pageSize)
    //    {

    //    }
    //}
    public interface IDataStore
    {
        //public DataStore(string name, List<string>fields)
        //{
        //    Name = name;
        //    fields.ForEach(f => Store.Add(f, new List<object>()));
        //}
        // string FieldID { get; private set; }
        //  List<dynamic> Values { get; private set; }

        //int SetValue<T>(T value);
        //T GetValue<T>(string field, int index, T defaultValue);
        //void Clear();



    }

    public interface IDataSource : IDisposable
    {
        //  bool Select(string name);
        void Query(IForm form);
        void QueryAsync();
    }

    public interface ISource 
    {
        IForm Get(string name,int pageNumber, int pageLength);
        IDataStore GetAsync(string name);
        IDataStore GetFromCache(string name);
        Task<IDataStore> GetFromCacheAsync(string name);

     
    }
    public interface IForm
    {
        void SetValues(SqlDataReader reader);
        void Clear();
        void AddRecord<T>(string fieldID, T value);
        string Query();
        string Insert();
        string Update();
        string Delete();
        string Save();
        public Dictionary<string, object> Values(int index);
    }

    //public interface ISource : IDisposable
    //{
    //    bool Select(string name);
    //    IDataStore Query();
    //    Task<IDataStore> QueryAsync();

    //}
}
