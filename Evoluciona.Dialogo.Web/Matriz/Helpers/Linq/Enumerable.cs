
namespace Evoluciona.Dialogo.Web.Matriz.Helpers.Linq
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public static class Enumerable
    {
         public static Enumerable<T> From<T>(
                    IEnumerable<T> source)
    {
       return new Enumerable<T>(source);
    }
        public static IEnumerable<T> Where<T>(
                         IEnumerable<T> source,
                         Predicate<T> predicate)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            return WhereIterator(source, predicate);
        }

        private static IEnumerable<T> WhereIterator<T>(
                                 IEnumerable<T> source,
                                 Predicate<T> predicate)
        {
            foreach (T item in source)
                if (predicate(item))
                    yield return item;
        }

        public static IEnumerable<TResult> Select<T, TResult>(
                                 IEnumerable<T> source,
                                 Func<T, TResult> projection)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (projection == null)
                throw new ArgumentNullException("projection");

            return SelectIterator(source, projection);
        }

        private static IEnumerable<TResult> SelectIterator<T, TResult>(
                                   IEnumerable<T> source,
                                   Func<T, TResult> projection)
        {
            foreach (T item in source)
                yield return projection(item);
        }

        //...
    }

    public struct Enumerable<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> source;

        public Enumerable(IEnumerable<T> source)
        {
            this.source = source;
        }


        public IEnumerator<T> GetEnumerator()
        {
            return source.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Enumerable<T> Where(Predicate<T> predicate)
        {
            return new Enumerable<T>(
                Enumerable.Where(source, predicate)
            );
        }

        public Enumerable<TResult> Select<TResult>(
                          Func<T, TResult> projection)
        {
            return new Enumerable<TResult>(
                Enumerable.Select(source, projection)
             );
        }

        //...
    }
}
