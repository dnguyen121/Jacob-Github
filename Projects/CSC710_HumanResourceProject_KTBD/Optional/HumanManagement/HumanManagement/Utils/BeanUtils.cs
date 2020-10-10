using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace HumanManagement.Utils
{
    public static class BeanUtils
    {
        public const int PAGE_COUNT = 2;

        public static T CreateAndCopy<T>(Object srcObject) 
        {
            if (srcObject == null)
            {
                return default(T);
            }
            T destObj = CreateInstance<T>();
            Copy(srcObject, destObj);
            return destObj;
        }

        public static void Copy(Object srcObject, Object destObject)
        {
            if (srcObject == null || destObject == null)
            {
                return;
            }
            else
            {
                foreach (PropertyInfo srcProp in srcObject.GetType().GetProperties())
                {
                    CopyByPropName(srcObject, destObject, srcProp.Name);
                }
            }
        }

        public static void Copy(Object srcObject, Object destObject, List<string> propList)
        {
            if (srcObject == null || destObject == null)
            {
                return;
            }
            else
            {
                foreach (string propName in propList) {
                    CopyByPropName(srcObject, destObject, propName);
                }
            }
        }

        private static void CopyByPropName(Object srcObject, Object destObject, string propName)
        {
            PropertyInfo srcProp = srcObject.GetType().GetProperty(propName);
            PropertyInfo destProp = destObject.GetType().GetProperty(propName);
            if (destProp == null)
            {
                destProp = destObject.GetType().GetProperty(propName.CamelCase());
                if (destProp == null)
                {
                    destProp = destObject.GetType().GetProperty(propName.DeCamelCase());
                }
            }
            if (destProp == null)
            {
                destProp = destObject.GetType().GetProperties()
                    .Where(q => q.Name.ToSimplePropName() == propName.ToSimplePropName())
                    .FirstOrDefault();
            }
            if (srcProp != null && destProp != null && srcProp.PropertyType.Equals(destProp.PropertyType))
            {
                var value = srcProp.GetValue(srcObject);
                if (value != null)
                {
                    destProp.SetValue(destObject, value);
                }
            }
            if (srcProp != null && destProp != null && !srcProp.PropertyType.Equals(destProp.PropertyType))
            {
                var value = srcProp.GetValue(srcObject);
                if (value != null && (srcProp.PropertyType == typeof(DateTime) || srcProp.PropertyType == typeof(DateTime?)) && destProp.PropertyType == typeof(string))
                {
                    DateTime dateValue = (DateTime)value;
                    destProp.SetValue(destObject, dateValue.ToVNDateFormat());
                }

                if (value != null && srcProp.PropertyType == typeof(string) && (destProp.PropertyType == typeof(DateTime) || destProp.PropertyType == typeof(DateTime?)))
                {
                    string stringValue = (string)value;
                    destProp.SetValue(destObject, stringValue.ToVNDate());
                }
            }
        }

        public static T CreateInstance<T>()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }

        public static TD[] CoppyArray<TS, TD>(ICollection<TS> srcArrays)
        {
            if (srcArrays == null)
            {
                return null;
            }
            List<TD> destList = new List<TD>();
            foreach (TS srcObj in srcArrays)
            {
                TD destObj = CreateAndCopy<TD>(srcObj);
                destList.Add(destObj);
            }
            return destList.ToArray();
        }
    }
}