using CsJvm.Models.ClassFileFormat;

namespace CsJvm.Models.Heap
{
    public struct HeapArrayRef
    {
        public int Index { get; set; }

        public ArrayTypes PrimitiveType { get; set; }

        public int Size { get; set; }


        public override string ToString() => $"{Index}: {PrimitiveType} [{Size}]";
    }
}
