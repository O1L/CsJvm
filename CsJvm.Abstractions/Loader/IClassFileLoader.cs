﻿using CsJvm.Models.ClassFileFormat;

namespace CsJvm.Abstractions.Loader
{
    /// <summary>
    /// Provides an ability to parse and load <see cref="ClassFile"/> files
    /// </summary>
    public interface IClassFileLoader
    {
        /// <summary>
        /// Load specified .class file
        /// </summary>
        /// <param name="reader"><see cref="BinaryReader"/> instance of current data reader</param>
        /// <returns><see langword="true"></see> if success; otherwise <see langword="false"></see></returns>
        bool TryLoad(BinaryReader? reader, out ClassFile? classFile);
    }
}
