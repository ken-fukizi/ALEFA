using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel
{
    public abstract class ReferenceEntity : AggregateRoot
    {
        protected ReferenceEntity()
        {
        }

        protected ReferenceEntity(string displayName)
        {
            DisplayName = displayName;
        }

        public override string ToString()
        {
            return DisplayName;
        }

        public string DisplayName { get; protected set; }
        public static IEnumerable<T> GetAll<T>() where T : ReferenceEntity
        {
            var type = typeof(T);
            var valuesField = type.GetFields();

            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Where(fi => fi.FieldType == typeof(T));

            foreach (var info in fields)
            {
                yield return info.GetValue(null) as T;

                //// Modified original class. We do not need an instance of the class as all the members we are looking at are static
                //var instance = new T();
                //var locatedValue = info.GetValue(instance) as T;

                //var locatedValue = info.GetValue(null) as T;

                //if (locatedValue != null)
                //{
                //    yield return locatedValue;
                //}
            }
        }
    }
}
