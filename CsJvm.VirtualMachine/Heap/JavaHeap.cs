using CsJvm.Abstractions.VirtualMachine;
using CsJvm.Models;
using CsJvm.Models.ClassFileFormat;
using CsJvm.Models.Heap;
using System.Collections.Concurrent;

namespace CsJvm.VirtualMachine.Heap
{
    /// <summary>
    /// JVM heap implementation
    /// </summary>
    public class JavaHeap : IJavaHeap
    {
        /// <summary>
        /// Static classes
        /// </summary>
        private readonly ConcurrentDictionary<string, JavaClass> _staticClasses = new();

        /// <summary>
        /// Classes instances
        /// </summary>
        private readonly ConcurrentDictionary<HeapClassRef, JavaClass> _classes = new();

        /// <summary>
        /// Arrays instances
        /// </summary>
        private readonly ConcurrentDictionary<HeapArrayRef, Array> _arrays = new();

        /// <summary>
        /// Classes arrays instances
        /// </summary>
        private readonly ConcurrentDictionary<HeapClassArrayRef, Array> _classArrays = new();

        /// <inheritdoc/>
        public bool TryCreateStaticClassRef(JavaClass javaClass)
            => _staticClasses.TryAdd(javaClass.Name, javaClass);


        /// <inheritdoc/>
        public HeapClassRef CreateClassRef(JavaClass javaClass)
        {
            var index = new HeapClassRef()
            {
                Index = _classes.Count + 1,
                Name = javaClass.Name
            };

            _classes.TryAdd(index, javaClass);

            return index;
        }

        /// <inheritdoc/>
        public HeapArrayRef CreateArrayRef(Array array, ArrayTypes arrayType, int size)
        {
            var index = new HeapArrayRef()
            {
                Index = _arrays.Count + 1,
                PrimitiveType = arrayType,
                Size = size
            };

            _arrays.TryAdd(index, array);

            return index;
        }

        /// <inheritdoc/>
        public HeapClassArrayRef CreateClassArrayRef(Array array, int size)
        {
            var index = new HeapClassArrayRef()
            {
                Index = _classArrays.Count + 1,
                Size = size
            };

            _classArrays.TryAdd(index, array);

            return index;
        }

        /// <inheritdoc/>
        public bool TryGetClass(HeapClassRef classRef, out JavaClass? javaClass)
            => _classes.TryGetValue(classRef, out javaClass);

        /// <inheritdoc/>
        public bool TryGetStaticClass(string className, out JavaClass? javaClass)
            => _staticClasses.TryGetValue(className, out javaClass);

        /// <inheritdoc/>
        public bool TryGetArray(HeapArrayRef arrayRef, out Array? array)
            => _arrays.TryGetValue(arrayRef, out array);

        /// <inheritdoc/>
        public bool TryGetClassArray(HeapClassArrayRef arrayRef, out Array? array)
            => _classArrays.TryGetValue(arrayRef, out array);
    }
}
