using CsJvm.Models;
using CsJvm.Models.ClassFileFormat;
using CsJvm.Models.Heap;

namespace CsJvm.Abstractions.VirtualMachine
{
    /// <summary>
    /// Provides methods to interact with JVM heap
    /// </summary>
    public interface IJavaHeap
    {
        /// <summary>
        /// Tries to create static class
        /// </summary>
        /// <param name="javaClass">Class to create ref</param>
        /// <returns><see langword="true"></see> if success; otherwise <see langword="false"></see></returns>
        bool TryCreateStaticClassRef(JavaClass javaClass);

        /// <summary>
        /// Creates class ref
        /// </summary>
        /// <param name="javaClass"></param>
        /// <returns>Crated <see cref="HeapClassRef"/> reference</returns>
        HeapClassRef CreateClassRef(JavaClass javaClass);

        /// <summary>
        /// Creates array ref
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayType"></param>
        /// <param name="size"></param>
        /// <returns>Crated <see cref="HeapArrayRef"/> reference</returns>
        HeapArrayRef CreateArrayRef(Array array, ArrayTypes arrayType, int size);

        /// <summary>
        /// Creates classes array ref
        /// </summary>
        /// <param name="array">Array</param>
        /// <param name="size">Array size</param>
        /// <returns>Crated <see cref="HeapClassArrayRef"/> reference</returns>
        HeapClassArrayRef CreateClassArrayRef(Array array, int size);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="classRef"></param>
        /// <param name="javaClass"></param>
        /// <returns><see langword="true"></see> if success; otherwise <see langword="false"></see></returns>
        bool TryGetClass(HeapClassRef classRef, out JavaClass? javaClass);

        /// <summary>
        /// Tries get static class
        /// </summary>
        /// <param name="className">Class name</param>
        /// <param name="javaClass">Result class</param>
        /// <returns><see langword="true"></see> if success; otherwise <see langword="false"></see></returns>
        bool TryGetStaticClass(string className, out JavaClass? javaClass);

        /// <summary>
        /// Tries get array
        /// </summary>
        /// <param name="arrayRef">Array reference</param>
        /// <param name="array">Result array</param>
        /// <returns><see langword="true"></see> if success; otherwise <see langword="false"></see></returns>
        bool TryGetArray(HeapArrayRef arrayRef, out Array? array);

        /// <summary>
        /// Tries get classes array
        /// </summary>
        /// <param name="arrayRef">Classes array ref</param>
        /// <param name="array">Result array</param>
        /// <returns><see langword="true"></see> if success; otherwise <see langword="false"></see></returns>
        bool TryGetClassArray(HeapClassArrayRef arrayRef, out Array? array);
    }
}
