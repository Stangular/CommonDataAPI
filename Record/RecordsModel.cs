using System;
using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;
//using System.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using DataAccess;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

using System.Data.SqlClient;
//using System.Linq;

namespace DataRecord
{

    public class PagingFormModel
    {
        public int PageNumber { get; set; }
        public int PageLength { get; set; }
        public string FormName { get; set; }

        public PagingFormModel(string formName, int pageNumber, int pageLength)
        {
            FormName = formName;
            PageNumber = pageNumber;
            PageLength = pageLength;
        }


    }

    public interface IRecordRepo
    {
        void Initialize();
        string GetValue(string key);
    }

    public interface IRedisRepo
    {
        void Initialize();
        string GetValue(string key);

    }

    public interface IRecordSource
    {
        void ToRecord(Form form);
        void FromRecord(Form form, int index);
    }

    //public interface IRecord
    //{
    //    void Clear();

    //}
    //public abstract class Record : IRecord
    //{
    //    public string FieldID { get; protected set; }
    //    public void Clear() { }

    //}
    public class Record//: Record
    {
        dynamic _defaultValue;
        public List<dynamic> values;
        public string FieldID { get; protected set; }
        // protected string FieldID { get; set; }

        public Record(string fieldID, dynamic defaultValue)
        {
            this.FieldID = fieldID;
            values = new List<dynamic>();
            _defaultValue = defaultValue;

            //if (keys != null)
            //{
            //    foreach (string k in keys)
            //    {
            //        Data.Add(k, "");
            //    }
            //}
        }


        //public void AddDate(DateTime datetime)
        //{
        //    SetValue("Year", datetime.Year.ToString());
        //    SetValue("Month", datetime.Month.ToString());
        //    SetValue("Day", datetime.Day.ToString());
        //    SetValue("Hour", datetime.Hour.ToString());
        //    SetValue("Minute", datetime.Minute.ToString());
        //}

        public void Clear()
        {
            values.Clear();
        }

        //public string TheFieldID
        //{
        //    get
        //    {
        //        return fieldID;
        //    }
        //}

        public void SetValue(SqlDataReader reader)
        {
            values.Add(reader[this.FieldID]);
        }
        //public int SetXValue(T value)
        //{
        //    values.Add(value);
        //    return values.Count;
        //}

        //public T GetValue<T>(int index)
        //{
        //    if (index >= 0 && index < values.Count)
        //    {
        //        try
        //        {
        //            return (T)values[index]; // Dynamic type converts int to long - cast to the generic type...is tht kosher? seems to work...
        //        }
        //        catch (System.Exception ex)
        //        {

        //        }
        //    }
        //    return defaultValue;
        //}

        public string GetValue(int index, string defaultValue = "")
        {
            try
            {
                return values[index].ToString();
            }
            catch (System.Exception ex)
            {
                // 
            }
            return defaultValue;
        }

        public dynamic GetValue(int index, dynamic defaultValue)
        {
            try
            {
                return values[index].ToString();
            }
            catch (System.Exception ex)
            {

            }
            return defaultValue;
        }
        //public bool SetData(string key, string value)
        //{
        //    if (!Data.ContainsKey(key)) { return false; }
        //    Data[key] = value;
        //    return true;
        //}

        //public string GetData(string key)
        //{
        //    if (!Data.ContainsKey(key)) { return ""; }
        //    return Data[key];
        //}
    }

    public class RecordRepository // : IRecordRepo
    {
        List<IRecordSource> _sources = new List<IRecordSource>();

        public void RegisterSource(IRecordSource source)
        {
            _sources.Add(source);
        }
    }

    public class RedisRepository : IRedisRepo
    {
        ConnectionMultiplexer _redis;

        public RedisRepository(string path = "localhost")
        {
            _redis = ConnectionMultiplexer.Connect(path);
            if (_redis == null)
            {
                _redis = ConnectionMultiplexer.Connect("localhost");
            }
        }

        public void Initialize()
        {
            StoreValue("name", "stan");
        }

        //public void Load( SqlDataReader rdr )
        //{
        //    while (rdr.Read())
        //    {
        //    //    StoreValue();
        //    }
        //}
        protected void StoreValue(string key, string value)
        {
            IDatabase db = _redis.GetDatabase();
            db.StringSet(key, value);
        }

        public string GetValue(string key)
        {
            IDatabase db = _redis.GetDatabase();
            return db.StringGet(key);
        }
    }

    public class FormModel
    {

        public string FormName { get; set; }
        public List<Record> Content;
        protected List<string> _fields = new List<string>();
        string _fieldSet = "";
        protected string _query;

        public FormModel(string formName, string[] fields)
        {
            FormName = formName;
            Content = new List<Record>();
            _fields.AddRange(fields);
            _query = "select " + Fields;


            _query = "select id,listName,listItem,listValue from lists";

        }

        //public bool IsNew(string idfieldName,int index )
        //{
        //    var record = Content.FirstOrDefault(r => r.  == idfieldName);
        //    return record.GetValue<int>( index, -1 ) >= 0;
        //}
        public int RecordCount { get; set; }

        protected string Fields
        {
            get
            {
                var flds = "";
                foreach (var f in _fields)
                {
                    flds += f;
                    flds += ",";
                }
                _fieldSet = flds.Substring(0, flds.Length - 1);
                return _fieldSet;
            }
        }

        public dynamic GetValue(string fieldID)
        {
            return Content.FirstOrDefault(r => r.FieldID == fieldID);
        }
    }
    public class Form : FormModel, IForm
    {

        protected string _insert;
        protected string _update;
        protected string _delete;

        public Form(string formName, string[] fields) : base(formName, fields)
        {

        }

        public string Query()
        {
            return _query;
        }

        virtual public string Insert()
        {
            return _insert;
        }

        public string Update()
        {
            return _update;
        }

        public string Delete()
        {
            return _delete;
        }

        public Record GetRecord(string fieldID)
        {
            return Content.FirstOrDefault(r => r.FieldID == fieldID);
        }

        public int SetValue(string fieldID, dynamic value)
        {
            var record = GetRecord(fieldID);
            if (record == null)
            {
                Content.Add(new Record(fieldID, value));
                record = GetRecord(fieldID);
            }
            return record.SetValue(value);
        }

        public void AddForm(Form form)
        {
            //   DependentContent.Add(form);
        }

        public T GetValue<T>(string fieldID, int index, T defaultValue)   /// devault(T)
        {
            //var record = GetRecord<T>(fieldID);
            //if (record == null)
            //{
            //    return defaultValue;
            //}
            //return record.GetValue(index, defaultValue);
            return defaultValue;
        }


        public void SetValues(SqlDataReader reader)
        {
            foreach (var r in Content)
            {
                r.SetValue(reader);
            }
        }

        public void Clear()
        {
            Content.ForEach(r => r.Clear());
        }

        public void AddRecord<T>(string fieldID, T value)
        {
            Content.Add(new Record(fieldID, value));
        }

        public string Save()
        {
            var record = Content.FirstOrDefault(r => r.FieldID == _fields[0]);
            if (record != null)
            {
                int id = record.GetValue(index, -1);
                return (id < 0) ? Insert(index) : Update(index, id);
            }
            return "";
        }

        public Dictionary<string, object> Values(int index)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            Record record;
            for (int i = 0; i < _fields.Count - 1; i = i + 1)
            {
                record = Content.FirstOrDefault(r => r.FieldID == _fields[i]);
                if( record != null)
                {
                    values.Add(_fields[i], record.GetValue(index));
                }
            }
            return values;
        }
        public string Insert(int index)
        {
            string sql = "";
            if (index >= RecordCount)
            {
                return sql;
            }
            int i;
            sql = String.Format("Insert Into Person ({0})", Fields);
            string values = "";
            Record record;
            for (i = 0; i < _fields.Count - 1; i = i + 1)
            {
                record = Content.FirstOrDefault(r => r.FieldID == _fields[i]);
                values += record.GetValue(index) + ",";
            }
            record = Content.FirstOrDefault(r => r.FieldID == _fields[i]);
            values += record.GetValue(index);
            sql += String.Format(sql + " values({0})", values + record.GetValue(index));

            return sql;
        }

        public string Update(int index, int id)
        {
            string sql = "";
            if (index >= RecordCount)
            {
                return sql;
            }
            int i;
            Record record;
            string values = "Set ";
            for (i = 1; i < _fields.Count - 1; i = i + 1)
            {
                record = Content.FirstOrDefault(r => r.FieldID == _fields[i]);
                values += _fields[i] + "=" + record.GetValue(index) + ",";
            }
            record = Content.FirstOrDefault(r => r.FieldID == _fields[i]);
            values += _fields[i] + "=" + record.GetValue(index);

            sql = String.Format("Update Person ({0})", values);
            record = Content.FirstOrDefault(r => r.FieldID == _fields[0]);
            return sql + " where Id = " + id;
        }
        //override public string Insert()
        //{
        //    Record r = Content.FirstOrDefault(c => c.FieldID == "Id");
        //    int nbr = 0, id;
        //    while ((id = r.GetValue<int>("Id", nbr++, -2)) != -2)
        //    {
        //        if (id == -1)  // insert...
        //        {

        //        }
        //        else // update...
        //        {
        //        }
        //    }
        //    foreach (var f in _fields)
        //    {

        //    }
        //    return "";
        // }
        //public void AddRecord(IRecordSource source)
        //{
        //    Content.Add(source.AsRecord);
        //}

        //public List<Record> NewRecords
        //{
        //    get
        //    {
        //        return Content.FindAll(r => r.ID.Length <= 0);
        //    }
        //}

    }

    public class ListForm : Form
    {
        public ListForm() : base("lists", new string[] { "Id", "ListName", "ListItem", "ListValue" })
        {
        }
    }
    //public class MasterForm : ISource
    //{
    //    List<Form> _forms = new List<Form>();
    //    List<IDataSource> _sources = new List<IDataSource>();
    //    IMemoryCache _cache;
    //    public MasterForm(IConfigurationRoot configuration, IMemoryCache cache, string formName = "Main")
    //    {
    //        _sources.Add(new Source(configuration));
    //        _cache = cache;
    //        _forms.Add(new ListForm());
    //    }

    //    public IForm Get(string name, int pageNumber, int pageLength)
    //    {
    //        var form = _forms.FirstOrDefault(f => f.FormName.ToLower() == name.ToLower());
    //        if (form == null)
    //        {
    //            form = new Form(name, new string[] { "" });
    //            _forms.Add(form);
    //        }
    //        _sources[0].Query(form);
    //        return form;
    //    }

    //    IDataStore ISource.GetAsync(string name)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    IDataStore ISource.GetFromCache(string name)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    Task<IDataStore> ISource.GetFromCacheAsync(string name)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}