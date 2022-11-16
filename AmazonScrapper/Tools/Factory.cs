using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AmazonScrapper.Tools
{
    public static class Factory
    {
        public static IEnumerable<T> GetEnumerableOfType<T>(params object[] constructorArgs)
        {
            List<T> objects = new List<T>();
            foreach (Type type in
                Assembly.GetAssembly(typeof(T)).GetTypes()//only gets types from this assembly
                //AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes())//Get though all the loaded files, slower
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T))))
            {
                objects.Add((T)Activator.CreateInstance(type, constructorArgs));
            }
            return objects;
        }

        public static IEnumerable<T> GetEnumerableOfType<T>()
        {
            return GetEnumerableOfType<T>(null);
        }

        public static IEnumerable<T> GetEnumerableOfInterface<T>(params object[] constructorArgs)
        {
            List<T> objects = new List<T>();
            var t = typeof(T);
            foreach (Type type in
                Assembly.GetAssembly(t).GetTypes()//only gets types from this assembly
                //AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes())//Get though all the loaded files, slower
                .Where(myType => myType.IsClass && !myType.IsAbstract && t.IsAssignableFrom(myType) && myType != t))
            {
                objects.Add((T)Activator.CreateInstance(type, constructorArgs));
            }
            return objects;
        }

        public static IEnumerable<T> GetEnumerableOfInterface<T>()
        {
            return GetEnumerableOfInterface<T>(null);
        }
    }
}
