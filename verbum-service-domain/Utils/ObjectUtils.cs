using System.Collections;

namespace verbum_service_domain.Utils
{
    public class ObjectUtils
    {
        public static bool IsEmpty(object obj)
        {
            if (obj == null)
            {
                return true;
            }
            else if (obj is string str)
            {
                return str.Length == 0;
            }
            else if (obj.GetType().IsArray)
            {
                return ((Array)obj).Length == 0;
            }
            else if (obj is ICollection collection)
            {
                return collection.Count == 0;
            }
            else if (obj is IDictionary dictionary)
            {
                return dictionary.Count == 0;
            }
            else if (obj is IEnumerable enumerable)
            {
                return !enumerable.Cast<object>().Any();
            }
            else
            {
                return false;
            }
        }

        public static bool IsNotEmpty (object obj)
        {
            return !IsEmpty(obj);
        }
    }
}
