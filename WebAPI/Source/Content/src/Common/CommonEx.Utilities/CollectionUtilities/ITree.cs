namespace CommonEx.Utilities.CollectionUtilities
{
    public interface ITree<T>
    {
        ITree<T> Parent { get; set; }
        T Item { get; set; }
        List<ITree<T>> Children { get; set; }

        bool HasChild { get; }
        bool IsRoot { get; }


        void Add(T item);
        void Add(ITree<T> child);
        void AddRange(List<ITree<T>> child);

        void Remove(ITree<T> child);
        ITree<T> RemoveParent();
    }
}
