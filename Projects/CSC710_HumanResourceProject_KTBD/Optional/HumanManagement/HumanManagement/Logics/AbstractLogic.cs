using HumanManagement.DTO;
using HumanManagement.Utils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace HumanManagement.Logics
{
    public abstract class AbstractLogic<TModel, TDTO, TIdType> where TDTO : class, new()
    {
        protected readonly SqlConnection _connection;

        public AbstractLogic()
        {
            _connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString); 
        }

        public virtual List<TModel> GetList()
        {
            _connection.Open();
            SqlCommand command = _connection.CreateCommand();
            command.CommandText = GetDefaultSelectClause();

            List<TModel> models = new List<TModel>();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                TModel model = GetModelByReader(ref reader);
                models.Add(model);
            }
            _connection.Close();
            return models;
        }

        public List<TModel> GetListAddClause(string addWhere)
        {
            _connection.Open();
            SqlCommand command = _connection.CreateCommand();
            command.CommandText = GetDefaultSelectClause() + addWhere;

            List<TModel> models = new List<TModel>();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                TModel model = GetModelByReader(ref reader);
                models.Add(model);
            }
            _connection.Close();
            return models;
        }

        protected virtual TModel GetModelByReader(ref SqlDataReader reader)
        {
            TDTO dto = new TDTO();

            var props = GetSelectProp();
            object[] os = new object[props.Count];

            foreach (var prop in props)
            {
                object o = reader[prop.Name];
                if (o == null || o is DBNull) continue;
                prop.SetValue(dto, o);
            }
            TModel model = BeanUtils.CreateAndCopy<TModel>(dto);
            return model;
        }

        public virtual TModel GetById(int id)
        {
            _connection.Open();
            string query = GetDefaultSelectClause();
            if (query.Contains("where"))
            {
                query += $" and t.{FindKeyProp().Name} = @id";
            } else
            {
                query += $" where t.{FindKeyProp().Name} = @id";
            }
            SqlCommand command = _connection.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@id", id);

            TModel model = default(TModel);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                model = GetModelByReader(ref reader);
            }
            _connection.Close();
            return model;
        }

        protected virtual string GetDefaultSelectClause()
        {
            List<PropertyInfo> properties = GetSelectProp();

            string query = "select {0} from {1} t " + GetJoinTable();
            query = string.Format(query, string.Join(",", GetSelectPropNames(properties)), GetTableName());
            return query;
        }

        protected virtual string GetJoinTable()
        {
            return "";
        }

        protected List<PropertyInfo> GetSelectProp()
        {
            PropertyInfo keyProp = FindKeyProp();
            List<PropertyInfo> propList = GetEditProps();
            propList.Insert(0, keyProp);

            return propList;
        }

        protected virtual string[] GetSelectPropNames(List<PropertyInfo> propList)
        {
            string[] props = propList.Select(q => "t." + q.Name).ToArray();
            return props;
        }

        private List<PropertyInfo> GetEditProps()
        {
            Type dtoType = typeof(TDTO);
            List<PropertyInfo> propNames = new List<PropertyInfo>();
            PropertyInfo keyProp = FindKeyProp();
            foreach (var prop in dtoType.GetProperties())
            {
                if (prop == keyProp) continue;

                if (prop.PropertyType == typeof(int)
                    || prop.PropertyType == typeof(string)
                    || prop.PropertyType == typeof(DateTime)
                    || prop.PropertyType == typeof(decimal))
                    propNames.Add(prop);
            }
            return propNames;
        }

        private Tuple<string, string> GetParamList(List<PropertyInfo> propNames)
        {
            string insertNames = string.Join(",", propNames.Select(q => q.Name));
            string paramNames = string.Join(",", propNames.Select(q => "@" + q.Name).ToArray());
            return Tuple.Create(insertNames, paramNames);
        }

        public virtual void PreInsert(TModel model) { }
        public virtual void PostInsert(TModel model, TDTO dto) { }
        public void Create(TModel model)
        {
            _connection.Open();
            PreInsert(model);
            TDTO dto = BeanUtils.CreateAndCopy<TDTO>(model);
            PostInsert(model, dto);

            SqlCommand command = GetInsertCommand(dto);
            command.ExecuteNonQuery();
            command.Dispose();
            _connection.Close();
        }

        private SqlCommand GetInsertCommand(TDTO dto)
        {
            List<PropertyInfo> editProps = GetEditProps();
            string query = "INSERT INTO {0} ({1}) VALUES ({2})";
            Tuple<string, string> editnames = GetParamList(editProps);
            query = string.Format(query, GetTableName(), editnames.Item1, editnames.Item2);

            SqlCommand command = _connection.CreateCommand();
            command.CommandText = query;

            SetDTOValueToCommand(ref command, dto);
            return command;
        }

        public virtual void PreEdit(TModel model) { }
        public virtual void PostEdit(TModel model, TDTO dto) { }
        public virtual void Edit(TModel model)
        {
            _connection.Open();
            PreEdit(model);
            TDTO dto = BeanUtils.CreateAndCopy<TDTO>(model);

            SqlCommand command = GetUpdateCommand(dto);
            command.ExecuteNonQuery();
            command.Dispose();

            _connection.Close();
            PostInsert(model, dto);
        }

        private SqlCommand GetUpdateCommand(TDTO dto)
        {
            PropertyInfo KeyProp = FindKeyProp();
            List<PropertyInfo> editProps = GetEditProps();
            string query = "UPDATE {0} SET {1} Where {2}";
            Tuple<string, string> editnames = GetParamList(editProps);
            string setValueString = string.Join(",",editProps.Select(q => q.Name + " = @" + q.Name).ToArray());
            string whereString = $"{KeyProp.Name} = @id";
            query = string.Format(query, GetTableName(), setValueString, whereString);
            
            SqlCommand command = _connection.CreateCommand();
            command.CommandText = query;

            SetDTOValueToCommand(ref command, dto);
            command.Parameters.AddWithValue("@id", KeyProp.GetValue(dto));

            return command;
        }

        private void SetDTOValueToCommand(ref SqlCommand command, TDTO dto)
        {
            foreach (var propName in GetEditProps())
            {
                object o = propName.GetValue(dto);
                if (o == null)
                {
                    o = DBNull.Value;
                }
                command.Parameters.AddWithValue($"@{propName.Name}", o);
            }
        }

        public virtual void PostDelete(TDTO dto) { }
        public virtual void Delete(TIdType key)
        {
            _connection.Open();
            PropertyInfo KeyProp = FindKeyProp();
            string query = $"DELETE {GetTableName()} Where {KeyProp.Name} = @id";
            SqlCommand command = _connection.CreateCommand();
            command.CommandText = query;

            command.Parameters.AddWithValue("@id", key);

            command.ExecuteNonQuery();
            command.Dispose();
            _connection.Close();
        }

        private string GetTableName()
        {
            return typeof(TDTO).Name;
        }

        public PropertyInfo FindKeyProp()
        {
            Type dtoType = typeof(TDTO);
            return dtoType.GetProperties().First();
        }

        public TIdType FindKeyValue(TModel model)
        {
            Type dtoType = typeof(TDTO);
            PropertyInfo keyProp = dtoType.GetProperties().First();
            PropertyInfo modelKey = typeof(TModel).GetProperties()
                .Where(q => q.Name.ToSimplePropName() == keyProp.Name.ToSimplePropName()).First();
            return (TIdType)modelKey.GetValue(model);
        }

        public virtual int Count()
        {
            _connection.Open();
            string query = $"Count * from {GetTableName()}";
            DbCommand command = _connection.CreateCommand(); ;
            command.CommandText = query;
            var reader = command.ExecuteReader();
            reader.Read();
            int count = reader.GetInt32(0);
            _connection.Close();
            return count;
        }
    }
}
