using System.Reflection;

namespace DynamicLoopAutoMapper.Extensions
{
    public static class ObjectExtensions
    {
        public static T Clone<T>(this T toClone) where T : class, new()
        {
            var newT = new T();
            foreach (var property in newT.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                if (property.CanRead && property.CanWrite)
                    property.SetValue(newT, property.GetValue(toClone, null), null);
            return newT;
        }
    }
}