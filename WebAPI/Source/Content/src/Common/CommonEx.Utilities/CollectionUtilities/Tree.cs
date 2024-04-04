namespace CommonEx.Utilities.CollectionUtilities
{
    public class Tree<T> : ITree<T>
    {
        #region Property

        public ITree<T> Parent { get; set; }

        public T Item { get; set; }

        public List<ITree<T>> Children { get; set; } = new List<ITree<T>>();

        #endregion


        #region Construction

        public Tree()
        {

        }
        public Tree(T item)
        {
            Item = item;
            Children = new List<ITree<T>>();
        }
        public Tree(T item, List<ITree<T>> children)
        {
            Item = item;
            Children = children;
        }

        #endregion


        #region Operator

        /// <summary>
        /// Has Child Tree
        /// </summary>
        public bool HasChild { get { return Children != null && Children.Count != 0; } }

        /// <summary>
        /// Is Root Node
        /// </summary>
        public bool IsRoot { get { return Parent != null; } }

        /// <summary>
        /// Add Leaf
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            Item = item;
        }
        /// <summary>
        /// Add Child Tree
        /// </summary>
        /// <param name="child"></param>
        public void Add(ITree<T> child)
        {
            Children.Add(child);
        }
        /// <summary>
        /// Add Child Trees
        /// </summary>
        /// <param name="children"></param>
        public void AddRange(List<ITree<T>> children)
        {
            Children.AddRange(children);
        }

        /// <summary>
        /// Remove Child Tree
        /// </summary>
        /// <param name="child"></param>
        public void Remove(ITree<T> child)
        {
            if (Children.Count == 0)
            {
                return;
            }

            Children.Remove(child);
            child.RemoveParent();
        }

        /// <summary>
        /// Remove Parent
        /// </summary>
        public ITree<T> RemoveParent()
        {
            if (Parent != null)
            {
                Parent = null;
            }
            return this;
        }

        #endregion
    }
}
