using System.Diagnostics.CodeAnalysis;

namespace CsJvm.Models.Heap
{
    public struct HeapClassRef
    {
        public int Index { get; set; }

        public string Name { get; set; }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj is not HeapClassRef classRef)
                return false;

            return classRef.Index == Index &&
                   classRef.Name == Name;
        }

        public override int GetHashCode()
            => HashCode.Combine(Index, Name);

        public static bool operator ==(HeapClassRef left, HeapClassRef right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(HeapClassRef left, HeapClassRef right)
        {
            return !(left == right);
        }

        public override string ToString() => $"{Index}: {Name}";
    }
}
