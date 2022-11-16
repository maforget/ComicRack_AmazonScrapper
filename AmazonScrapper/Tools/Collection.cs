using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections;

namespace AmazonScrapper.Tools
{
    public class Collection<T> : IEnumerable<T>
    {
        private List<T> list;

        public Collection(object[] param)
        {
            //list = typeof(T).IsInterface ? GetInstanceOfInterface(param).ToList() : GetInstanceOfType(param).ToList();
            list = GetInstanceOfInterface(param).ToList();
        }

        public Collection()
        {
            //list = typeof(T).IsInterface ? GetInstanceOfInterface().ToList() : GetInstanceOfType().ToList();
            list = GetInstanceOfInterface().ToList();
        }

        public T GetInterface<U>() where U : class, T
        {
            return list.FirstOrDefault(x => x.GetType() == typeof(U));
        }

        public U Get<U>() where U : class
        {
            return list.FirstOrDefault(x => x.GetType() == typeof(U)) as U;
        }

        //protected T Get(string type)
        //{
        //    return list.FirstOrDefault(x => x.GetType().Name == type);
        //}

        public void Add(T source)
        {
            if (source != null)
                list.Add(source);
        }

        private IEnumerable<T> GetInstanceOfInterface()
        {
            return Factory.GetEnumerableOfInterface<T>();
        }
        private IEnumerable<T> GetInstanceOfInterface(object[] param)
        {
            return Factory.GetEnumerableOfInterface<T>(param);
        }

        private IEnumerable<T> GetInstanceOfType()
        {
            return Factory.GetEnumerableOfType<T>();
        }

        private IEnumerable<T> GetInstanceOfType(object[] param)
        {
            return Factory.GetEnumerableOfType<T>(param);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)list).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)list).GetEnumerator();
        }
    }
}
