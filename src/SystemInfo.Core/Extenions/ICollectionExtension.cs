namespace System.Collections.Generic;

/// <summary>
/// Extension methods for <see cref="IList{T}"/>.
/// </summary>
internal static class ICollectionExtension
{
    /// <summary>
    /// Update the collection with the new values, and remove the items no in the new one.
    /// </summary>
    /// <typeparam name="T">The type stored in the collection.</typeparam>
    /// <param name="collection">The collection to update.</param>
    /// <param name="items">The items to update. (items not in this collection will be removed).</param>
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

        for (var j = i; j < collection.Count; j++)
        {
            collection.RemoveAt(j);
        }
    }
}
