using CsJvm.Abstractions.Disasm;
using CsJvm.Disasm.DisasmDescriptions;
using CsJvm.Models.ClassFileFormat.ConstantPool;
using CsJvm.Models.Disasm;

namespace CsJvm.Disasm
{
    /// <summary>
    /// Descriptions provider
    /// </summary>
    public class DisasmDescriptionProvider : IDisasmDescriptionProvider
    {
        /// <inheritdoc/>
        public string GetInfo(DisasmInfoType infoType, int index, CpInfo[] cpInfo) =>
            infoType switch
            {
                DisasmInfoType.Primitive => DisasmDescription.GetInfo(new ConstInfoDescription(index, cpInfo)),
                DisasmInfoType.Field => DisasmDescription.GetInfo(new FieldInfoDescription(index, cpInfo)),
                DisasmInfoType.Method => DisasmDescription.GetInfo(new MethodInfoDescription(index, cpInfo)),
                _ => string.Empty
            };
    }
}
