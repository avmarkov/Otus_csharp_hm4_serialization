using System.Reflection;
using System.Text;

namespace hm3_serialization
{
    public static class MySerializer<T>
    {
        // Сериализация
        public static string Serialize(T obj)
        {
            var sb = new StringBuilder();
            var myType = obj.GetType();
            var fields = myType.GetFields();

            foreach (var field in fields)
            {
                if (sb.Length > 0)
                {
                    sb.Append(";");
                }                    
                sb.Append(field.Name);
                sb.Append("=");
                sb.Append(field.GetValue(obj).ToString());
                
            }
            return sb.ToString();
        }

        // Десериализация
        public static T DeSerialize(string s)
        {
            var myType = typeof(T);
            var instance = Activator.CreateInstance(myType);

            var strfieldValues = s.Split(';');
            foreach (var strfieldValue in strfieldValues)
            {
                var oneStrField = strfieldValue.Split('=');
                var field = myType.GetField(oneStrField[0]);
                field.SetValue(instance, Convert.ChangeType(oneStrField[1], field.FieldType));
            }

            return (T)instance;
        }
    }
}
