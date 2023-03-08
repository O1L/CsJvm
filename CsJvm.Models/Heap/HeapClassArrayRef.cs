namespace CsJvm.Models.Heap
{
    public struct HeapClassArrayRef
    {
        public int Index { get; set; }

        public int Size { get; set; }


        public override string ToString() => $"{Index}: {Size}";
    }
}
