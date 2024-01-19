using System;
using System.Linq.Expressions;

namespace WebAPI
{
    public static class EntityExtensions
    {
        public static void UpdateProperties<T>(this T target, T source, params Expression<Func<T, object?>>[] propertiesToUpdate)
        {
            foreach (var property in propertiesToUpdate)
            {
                if (property.Body is MemberExpression memberExpression)
                {
                    var propertyInfo = memberExpression.Member as System.Reflection.PropertyInfo;
                    if (propertyInfo != null)
                    {
                        var value = property.Compile().Invoke(source);
                        propertyInfo.SetValue(target, value);
                    }
                }
            }
        }
    }
}