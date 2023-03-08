using CsJvm.Abstractions.Instructions;
using CsJvm.Abstractions.VirtualMachine;
using CsJvm.VirtualMachine.Attributes;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace CsJvm.VirtualMachine
{
    /// <summary>
    /// Opcodes decoder
    /// </summary>
    public class OpcodesDecoder : IOpcodesDecoder
    {
        /// <summary>
        /// Main logger
        /// </summary>
        private readonly ILogger _logger;

        /// JVM instructions implementation
        /// </summary>
        private readonly IOpcodes _opcodesImpl;

        /// <summary>
        /// Opcodes map. Index is the opcode signature.
        /// </summary>
        private readonly (MethodInfo Method, string Name)[] _opcodes = new (MethodInfo Method, string Name)[0xff + 1];

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logger">Main logger</param>
        /// <param name="opcodesImpl">Opcodes implementation</param>
        public OpcodesDecoder(ILogger<OpcodesDecoder> logger, IOpcodes opcodesImpl)
        {
            _logger = logger;
            _opcodesImpl = opcodesImpl;
            GenerateOpcodesMap();
        }

        /// <inheritdoc/>
        public void Execute(IJavaThread thread)
        {
            // fetch opcode
            var opcode = thread.CurrentMethod.Code[thread.ProgramCounter];

            // update PC after opcode decoding (generic opcode size is 1 byte)
            thread.ProgramCounter++;

            // call the opcode
            try
            {
                _opcodes[opcode].Method.Invoke(_opcodesImpl, new object[] { thread });
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Cannot execute opcode: {opcode}", opcode);
                throw;
            }
        }

        /// <summary>
        /// Generates opcodes map using reflection
        /// </summary>
        private void GenerateOpcodesMap()
        {
            Array.ForEach(_opcodesImpl.GetType().GetMethods(), method =>
            {
                var ca = method.GetCustomAttribute<OpcodeAttribute>();
                if (ca != null)
                    _opcodes[ca.Opcode] = (method, ca.Name);
            });
        }
    }
}
