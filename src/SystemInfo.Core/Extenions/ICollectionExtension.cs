namespace System.Collections.Generic
{
    internal static class ICollectionExtension
    {
        public static void UpdateAndRemove<T>(this IList<T> collection, IEnumerable<T> items)
        {
            var i = 0;
            foreach (var item in items)
            {
                if (i >= collection.Count)
                {
                    collection.Add(item);
                }
                else
                {
                    collection[i] = item;
                }
                i++;
            }

            for (int j = i; j < collection.Count; j++)
            {
                collection.RemoveAt(j);
            }
        }
    }
}