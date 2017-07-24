using System;
using System.Collections.Generic;
using System.Linq;
//using System.Linq;

namespace DataRecord
{
    //public interface IRecord
    //{

    //}

    public interface IRecordSource
    {
        void ToRecord(Form form);
        void FromRecord(Form form, int index);
    }

    public class Record //: IRecord
    {
        public string fieldID { get; set; }
        public List<dynamic> values;


        public Record(string fieldID)
        {
            this.fieldID = fieldID;
            values = new List<dynamic>();
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

        public int SetValue<T>(T value)
        {
            values.Add(value);
            return values.Count;
        }


        public T GetValue<T>(string field, int index, T defaultValue)
        {
            if (index >= 0 && index < values.Count)
            {
                try
                {
                    return (T)values[index]; // Dynamic type converts int to long - cast to the generic type...is tht kosher? seems to work...
                }
                catch (System.Exception ex)
                {

                }
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

    public class RecordRepository
    {
        List<IRecordSource> _sources = new List<IRecordSource>();

        public void RegisterSource(IRecordSource source)
        {
            _sources.Add(source);
        }


    }

    public class Form // : IRecord
    {
        public string FormName { get; set; }
        public int RecordCount { get; set; }
        public List<Record> Content;
        public List<Form> DependentContent;

        public Form(string formName)
        {
            FormName = formName;
            Content = new List<Record>();
            DependentContent = new List<Form>();
        }

        public void Clear()
        {
            Content.ForEach(r => r.Clear());
        }

        public void AddRecord(string fieldID)
        {
            Content.Add(new Record(fieldID));
        }

        public Record GetRecord(string fieldID)
        {
            return Content.FirstOrDefault(r => r.fieldID == fieldID);
        }

        public int SetValue<T>(string fieldID, T value)
        {
            var record = GetRecord(fieldID);
            if (record == null)
            {
                Content.Add(new Record(fieldID));
                record = GetRecord(fieldID);
            }
            return record.SetValue(value);
        }

        public void AddForm(Form form)
        {
            DependentContent.Add(form);
        }

        public T GetValue<T>(string fieldID, int index, T defaultValue)   /// devault(T)
        {
            var record = GetRecord(fieldID);
            if( record == null)
            {
                return defaultValue;
            }
            return record.GetValue<T>(fieldID, index, defaultValue);
        }

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
}