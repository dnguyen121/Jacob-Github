using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace HumanManagement.Utils
{
    public static class CustomHelper
    {
        public static MvcHtmlString SelectList<TModel, TProperty>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> expression,
            object list,
            string IdPropName,
            string NamePropName,
            string @class = null)
        {
            IList collection = (IList)list;
            if (collection == null || collection.Count == 0)
            {
                return new MvcHtmlString("");
            }
            var keyProp = collection[0].GetType().GetProperty(IdPropName);
            var nameProp = collection[0].GetType().GetProperty(NamePropName);


            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string PropertyName = metadata.PropertyName;
            string className = @class == null ? "" : $" class=\"{@class}\"";
            string s = $"<select name=\"{PropertyName}\"{className}>";
            var value = ModelMetadata.FromLambdaExpression(
                expression, helper.ViewData
            ).Model;
            
            foreach (var item in collection)
            {
                string key = keyProp.GetValue(item) == null ? "" : keyProp.GetValue(item).ToString();
                string name = nameProp.GetValue(item) == null ? "" : nameProp.GetValue(item).ToString();
                if (value != null && key.Equals(value.ToString()))
                {
                    s += $"<option value=\"{key}\" selected>{name}</option>";
                }
                else
                {
                    s += $"<option value=\"{key}\">{name}</option>";
                }
            }
            s += "</select>";
            return new MvcHtmlString(s);
        }

        public class SelectOption
        {
            public string Value { get; set; }
            public string Display { get; set; }

            public SelectOption()
            {
            }

            public SelectOption(string Value, string Display)
            {
                this.Value = Value;
                this.Display = Display;
            }

            public SelectOption(string Value)
            {
                this.Value = Value;
                this.Display = Value;
            }
        }
    }
}