namespace Queene.Core.MovesGenerating
{
    public interface IList<T>
    {
        int Count { get; }

        void Add(T item);
        void RemoveAtIndex(int index);
        void SetAtIndex(int index, T value);
        T GetAtIndex(int index);
        T[] Get();
        void Clear();
    }
}
