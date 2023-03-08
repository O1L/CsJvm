using CsJvm.Abstractions.Disasm;
using CsJvm.Models;
using CsJvm.Models.ClassFileFormat.ConstantPool;
using CsJvm.Models.Disasm;
using Microsoft.Extensions.Logging;
using System.Text;

namespace CsJvm.Disasm
{
    /// <summary>
    /// Compiled bytecode disassembler
    /// </summary>
    public class Disassembler : IDisasm
    {
        #region Opcodes set
        public static readonly OpcodeData[] _opcodes =
        {
            // Constants ops
            new OpcodeData(1, 0x00, "nop"),
            new OpcodeData(1, 0x01, "aconst_null"),
            new OpcodeData(1, 0x02, "iconst_m1"),
            new OpcodeData(1, 0x03, "iconst_0"),
            new OpcodeData(1, 0x04, "iconst_1"),
            new OpcodeData(1, 0x05, "iconst_2"),
            new OpcodeData(1, 0x06, "iconst_3"),
            new OpcodeData(1, 0x07, "iconst_4"),
            new OpcodeData(1, 0x08, "iconst_5"),
            new OpcodeData(1, 0x09, "lconst_0"),
            new OpcodeData(1, 0x0a, "lconst_1"),
            new OpcodeData(1, 0x0b, "fconst_0"),
            new OpcodeData(1, 0x0c, "fconst_1"),
            new OpcodeData(1, 0x0d, "fconst_2"),
            new OpcodeData(1, 0x0e, "dconst_0"),
            new OpcodeData(1, 0x0f, "dconst_1"),
            new OpcodeData(2, 0x10, "bipush"),
            new OpcodeData(3, 0x11, "sipush"),
            new OpcodeData(2, 0x12, "ldc", DisasmInfoType.Primitive),
            new OpcodeData(3, 0x13, "ldc_w", DisasmInfoType.Primitive),
            new OpcodeData(3, 0x14, "ldc2_w", DisasmInfoType.Primitive),

            // Loads ops
            new OpcodeData(2, 0x15, "iload"),
            new OpcodeData(2, 0x16, "lload"),
            new OpcodeData(2, 0x17, "fload"),
            new OpcodeData(2, 0x18, "dload"),
            new OpcodeData(2, 0x19, "aload"),
            new OpcodeData(1, 0x1a, "iload_0"),
            new OpcodeData(1, 0x1b, "iload_1"),
            new OpcodeData(1, 0x1c, "iload_2"),
            new OpcodeData(1, 0x1d, "iload_3"),
            new OpcodeData(1, 0x1e, "lload_0"),
            new OpcodeData(1, 0x1f, "lload_1"),
            new OpcodeData(1, 0x20, "lload_2"),
            new OpcodeData(1, 0x21, "lload_3"),
            new OpcodeData(1, 0x22, "fload_0"),
            new OpcodeData(1, 0x23, "fload_1"),
            new OpcodeData(1, 0x24, "fload_2"),
            new OpcodeData(1, 0x25, "fload_3"),
            new OpcodeData(1, 0x26, "dload_0"),
            new OpcodeData(1, 0x27, "dload_1"),
            new OpcodeData(1, 0x28, "dload_2"),
            new OpcodeData(1, 0x29, "dload_3"),
            new OpcodeData(1, 0x2a, "aload_0"),
            new OpcodeData(1, 0x2b, "aload_1"),
            new OpcodeData(1, 0x2c, "aload_2"),
            new OpcodeData(1, 0x2d, "aload_3"),
            new OpcodeData(1, 0x2e, "iaload"),
            new OpcodeData(1, 0x2f, "laload"),
            new OpcodeData(1, 0x30, "faload"),
            new OpcodeData(1, 0x31, "daload"),
            new OpcodeData(1, 0x32, "aaload"),
            new OpcodeData(1, 0x33, "baload"),
            new OpcodeData(1, 0x34, "caload"),
            new OpcodeData(1, 0x35, "saload"),

            // Stores ops
            new OpcodeData(2, 0x36, "istore"),
            new OpcodeData(2, 0x37, "lstore"),
            new OpcodeData(2, 0x38, "fstore"),
            new OpcodeData(2, 0x39, "dstore"),
            new OpcodeData(2, 0x3a, "astore"),
            new OpcodeData(1, 0x3b, "istore_0"),
            new OpcodeData(1, 0x3c, "istore_1"),
            new OpcodeData(1, 0x3d, "istore_2"),
            new OpcodeData(1, 0x3e, "istore_3"),
            new OpcodeData(1, 0x3f, "lstore_0"),
            new OpcodeData(1, 0x40, "lstore_1"),
            new OpcodeData(1, 0x41, "lstore_2"),
            new OpcodeData(1, 0x42, "lstore_3"),
            new OpcodeData(1, 0x43, "fstore_0"),
            new OpcodeData(1, 0x44, "fstore_1"),
            new OpcodeData(1, 0x45, "fstore_2"),
            new OpcodeData(1, 0x46, "fstore_3"),
            new OpcodeData(1, 0x47, "dstore_0"),
            new OpcodeData(1, 0x48, "dstore_1"),
            new OpcodeData(1, 0x49, "dstore_2"),
            new OpcodeData(1, 0x4a, "dstore_3"),
            new OpcodeData(1, 0x4b, "astore_0"),
            new OpcodeData(1, 0x4c, "astore_1"),
            new OpcodeData(1, 0x4d, "astore_2"),
            new OpcodeData(1, 0x4e, "astore_3"),
            new OpcodeData(1, 0x4f, "iastore"),
            new OpcodeData(1, 0x50, "lastore"),
            new OpcodeData(1, 0x51, "fastore"),
            new OpcodeData(1, 0x52, "dastore"),
            new OpcodeData(1, 0x53, "aastore"),
            new OpcodeData(1, 0x54, "bastore"),
            new OpcodeData(1, 0x55, "castore"),
            new OpcodeData(1, 0x56, "sastore"),

            // Stack ops
            new OpcodeData(1, 0x57, "pop"),
            new OpcodeData(1, 0x58, "pop2"),
            new OpcodeData(1, 0x59, "dup"),
            new OpcodeData(1, 0x5a, "dup_x1"),
            new OpcodeData(1, 0x5b, "dup_x2"),
            new OpcodeData(1, 0x5c, "dup2"),
            new OpcodeData(1, 0x5d, "dup2_x1"),
            new OpcodeData(1, 0x5e, "dup2_x2"),
            new OpcodeData(1, 0x5f, "swap"),

            // Math ops
            new OpcodeData(1, 0x60, "iadd"),
            new OpcodeData(1, 0x61, "ladd"),
            new OpcodeData(1, 0x62, "fadd"),
            new OpcodeData(1, 0x63, "dadd"),
            new OpcodeData(1, 0x64, "isub"),
            new OpcodeData(1, 0x65, "lsub"),
            new OpcodeData(1, 0x66, "fsub"),
            new OpcodeData(1, 0x67, "dsub"),
            new OpcodeData(1, 0x68, "imul"),
            new OpcodeData(1, 0x69, "lmul"),
            new OpcodeData(1, 0x6a, "fmul"),
            new OpcodeData(1, 0x6b, "dmul"),
            new OpcodeData(1, 0x6c, "idiv"),
            new OpcodeData(1, 0x6d, "ldiv"),
            new OpcodeData(1, 0x6e, "fdiv"),
            new OpcodeData(1, 0x6f, "ddiv"),
            new OpcodeData(1, 0x70, "irem"),
            new OpcodeData(1, 0x71, "lrem"),
            new OpcodeData(1, 0x72, "frem"),
            new OpcodeData(1, 0x73, "drem"),
            new OpcodeData(1, 0x74, "ineg"),
            new OpcodeData(1, 0x75, "lneg"),
            new OpcodeData(1, 0x76, "fneg"),
            new OpcodeData(1, 0x77, "dneg"),
            new OpcodeData(1, 0x78, "ishl"),
            new OpcodeData(1, 0x79, "lshl"),
            new OpcodeData(1, 0x7a, "ishr"),
            new OpcodeData(1, 0x7b, "lshr"),
            new OpcodeData(1, 0x7c, "iushr"),
            new OpcodeData(1, 0x7d, "lushr"),
            new OpcodeData(1, 0x7e, "iand"),
            new OpcodeData(1, 0x7f, "land"),
            new OpcodeData(1, 0x80, "ior"),
            new OpcodeData(1, 0x81, "lor"),
            new OpcodeData(1, 0x82, "ixor"),
            new OpcodeData(1, 0x83, "lxor"),
            new OpcodeData(3, 0x84, "iinc"),

            // Conversions ops
            new OpcodeData(1, 0x85, "i2l"),
            new OpcodeData(1, 0x86, "i2f"),
            new OpcodeData(1, 0x87, "i2d"),
            new OpcodeData(1, 0x88, "l2i"),
            new OpcodeData(1, 0x89, "l2f"),
            new OpcodeData(1, 0x8a, "l2d"),
            new OpcodeData(1, 0x8b, "f2i"),
            new OpcodeData(1, 0x8c, "f2l"),
            new OpcodeData(1, 0x8d, "f2d"),
            new OpcodeData(1, 0x8e, "d2i"),
            new OpcodeData(1, 0x8f, "d2l"),
            new OpcodeData(1, 0x90, "d2f"),
            new OpcodeData(1, 0x91, "i2b"),
            new OpcodeData(1, 0x92, "i2c"),
            new OpcodeData(1, 0x93, "i2s"),

            // Comparisons ops
            new OpcodeData(1, 0x94, "lcmp"),
            new OpcodeData(1, 0x95, "fcmpl"),
            new OpcodeData(1, 0x96, "fcmpg"),
            new OpcodeData(1, 0x97, "dcmpl"),
            new OpcodeData(1, 0x98, "dcmpg"),
            new OpcodeData(3, 0x99, "ifeq", DisasmInfoType.None, true),
            new OpcodeData(3, 0x9a, "ifne", DisasmInfoType.None, true),
            new OpcodeData(3, 0x9b, "iflt", DisasmInfoType.None, true),
            new OpcodeData(3, 0x9c, "ifge", DisasmInfoType.None, true),
            new OpcodeData(3, 0x9d, "ifgt", DisasmInfoType.None, true),
            new OpcodeData(3, 0x9e, "ifle", DisasmInfoType.None, true),
            new OpcodeData(3, 0x9f, "if_icmpeq", DisasmInfoType.None, true),
            new OpcodeData(3, 0xa0, "if_icmpne", DisasmInfoType.None, true),
            new OpcodeData(3, 0xa1, "if_icmplt", DisasmInfoType.None, true),
            new OpcodeData(3, 0xa2, "if_icmpge", DisasmInfoType.None, true),
            new OpcodeData(3, 0xa3, "if_icmpgt", DisasmInfoType.None, true),
            new OpcodeData(3, 0xa4, "if_icmple", DisasmInfoType.None, true),
            new OpcodeData(3, 0xa5, "if_acmpeq", DisasmInfoType.None, true),
            new OpcodeData(3, 0xa6, "if_acmpne", DisasmInfoType.None, true),

            // Control ops
            new OpcodeData(3, 0xa7, "goto", DisasmInfoType.None, true),
            new OpcodeData(3, 0xa8, "jsr"),
            new OpcodeData(2, 0xa9, "ret"),
            new OpcodeData(-1, 0xaa, "tableswitch"), // a variable-length instruction
            new OpcodeData(-1, 0xab, "lookupswitch"), // a variable-length instruction
            new OpcodeData(1, 0xac, "ireturn"),
            new OpcodeData(1, 0xad, "lreturn"),
            new OpcodeData(1, 0xae, "freturn"),
            new OpcodeData(1, 0xaf, "dreturn"),
            new OpcodeData(1, 0xb0, "areturn"),
            new OpcodeData(1, 0xb1, "return"),

            // Reference ops
            new OpcodeData(3, 0xb2, "getstatic", DisasmInfoType.Field),
            new OpcodeData(3, 0xb3, "putstatic", DisasmInfoType.Field),
            new OpcodeData(3, 0xb4, "getfield", DisasmInfoType.Field),
            new OpcodeData(3, 0xb5, "putfield", DisasmInfoType.Field),
            new OpcodeData(3, 0xb6, "invokevirtual", DisasmInfoType.Method),
            new OpcodeData(3, 0xb7, "invokespecial", DisasmInfoType.Method),
            new OpcodeData(3, 0xb8, "invokestatic", DisasmInfoType.Method),
            new OpcodeData(5, 0xb9, "invokeinterface", DisasmInfoType.Method),
            new OpcodeData(5, 0xba, "invokedynamic", DisasmInfoType.Method),
            new OpcodeData(3, 0xbb, "new"),
            new OpcodeData(2, 0xbc, "newarray"),
            new OpcodeData(3, 0xbd, "anewarray"),
            new OpcodeData(1, 0xbe, "arraylength"),
            new OpcodeData(1, 0xbf, "athrow"),
            new OpcodeData(3, 0xc0, "checkcast", DisasmInfoType.Primitive),
            new OpcodeData(3, 0xc1, "instanceof"),
            new OpcodeData(1, 0xc2, "monitorenter"),
            new OpcodeData(1, 0xc3, "monitorexit"),

            // Extended ops
            new OpcodeData(6, 0xc4, "wide"),
            new OpcodeData(4, 0xc5, "multianewarray"),
            new OpcodeData(3, 0xc6, "ifnull", DisasmInfoType.None, true),
            new OpcodeData(3, 0xc7, "ifnonnull", DisasmInfoType.None, true),
            new OpcodeData(5, 0xc8, "goto_w", DisasmInfoType.None, true),
            new OpcodeData(5, 0xc9, "jsr_w"),

            // Reserved ops
            new OpcodeData(0, 0xca, "breakpoint"),
            new OpcodeData(0, 0xcb, "RESERVED"),
            new OpcodeData(0, 0xcc, "RESERVED"),
            new OpcodeData(0, 0xcd, "RESERVED"),
            new OpcodeData(0, 0xce, "RESERVED"),
            new OpcodeData(0, 0xcf, "RESERVED"),
            new OpcodeData(0, 0xd0, "RESERVED"),
            new OpcodeData(0, 0xd1, "RESERVED"),
            new OpcodeData(0, 0xd2, "RESERVED"),
            new OpcodeData(0, 0xd3, "RESERVED"),
            new OpcodeData(0, 0xd4, "RESERVED"),
            new OpcodeData(0, 0xd5, "RESERVED"),
            new OpcodeData(0, 0xd6, "RESERVED"),
            new OpcodeData(0, 0xd7, "RESERVED"),
            new OpcodeData(0, 0xd8, "RESERVED"),
            new OpcodeData(0, 0xd9, "RESERVED"),
            new OpcodeData(0, 0xda, "RESERVED"),
            new OpcodeData(0, 0xdb, "RESERVED"),
            new OpcodeData(0, 0xdc, "RESERVED"),
            new OpcodeData(0, 0xdd, "RESERVED"),
            new OpcodeData(0, 0xde, "RESERVED"),
            new OpcodeData(0, 0xdf, "RESERVED"),
            new OpcodeData(0, 0xe0, "RESERVED"),
            new OpcodeData(0, 0xe1, "RESERVED"),
            new OpcodeData(0, 0xe2, "RESERVED"),
            new OpcodeData(0, 0xe3, "RESERVED"),
            new OpcodeData(0, 0xe4, "RESERVED"),
            new OpcodeData(0, 0xe5, "RESERVED"),
            new OpcodeData(0, 0xe6, "RESERVED"),
            new OpcodeData(0, 0xe7, "RESERVED"),
            new OpcodeData(0, 0xe8, "RESERVED"),
            new OpcodeData(0, 0xe9, "RESERVED"),
            new OpcodeData(0, 0xea, "RESERVED"),
            new OpcodeData(0, 0xeb, "RESERVED"),
            new OpcodeData(0, 0xec, "RESERVED"),
            new OpcodeData(0, 0xed, "RESERVED"),
            new OpcodeData(0, 0xee, "RESERVED"),
            new OpcodeData(0, 0xef, "RESERVED"),
            new OpcodeData(0, 0xf0, "RESERVED"),
            new OpcodeData(0, 0xf1, "RESERVED"),
            new OpcodeData(0, 0xf2, "RESERVED"),
            new OpcodeData(0, 0xf3, "RESERVED"),
            new OpcodeData(0, 0xf4, "RESERVED"),
            new OpcodeData(0, 0xf5, "RESERVED"),
            new OpcodeData(0, 0xf6, "RESERVED"),
            new OpcodeData(0, 0xf7, "RESERVED"),
            new OpcodeData(0, 0xf8, "RESERVED"),
            new OpcodeData(0, 0xf9, "RESERVED"),
            new OpcodeData(0, 0xfa, "RESERVED"),
            new OpcodeData(0, 0xfb, "RESERVED"),
            new OpcodeData(0, 0xfc, "RESERVED"),
            new OpcodeData(0, 0xfd, "RESERVED"),
            new OpcodeData(0, 0xfe, "impdep1"),
            new OpcodeData(0, 0xff, "impdep2")
        };
        #endregion

        /// <summary>
        /// Main logger
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Descriptipons provider
        /// </summary>
        private readonly IDisasmDescriptionProvider _descriptionProvider;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logger">Main logger</param>
        /// <param name="descriptionProvider">Descriptipons provider</param>
        public Disassembler(ILogger<Disassembler> logger, IDisasmDescriptionProvider descriptionProvider)
        {
            _logger = logger;
            _descriptionProvider = descriptionProvider;
        }

        /// <inheritdoc/>
        public OpcodeInfo[] GetOpcodes(byte[]? code, CpInfo[] info)
        {
            if (code == null || info.Length == 0)
                return Array.Empty<OpcodeInfo>();

            try
            {
                var opcodes = new List<OpcodeInfo>(code.Length / 2);
                var pc = 0;
                while (pc < code.Length)
                {
                    // fetch opcode
                    var opcode = code[pc];
                    var found = _opcodes[opcode];

                    if (found.Size == 0)
                        throw new InvalidOperationException("Illegal opcopde found!");

                    // calculate pc increment value; in general it's a fixed instruction length
                    var increment = GetIncrement(found, pc, code);

                    // calculate extended index
                    int? index = found.Size switch
                    {
                        2 => code[pc + 1],
                        3 => (code[pc + 1] << 8) | code[pc + 2],
                        4 => (code[pc + 1] << 8) | code[pc + 2],
                        5 => (code[pc + 1] << 8) | code[pc + 2],
                        6 => (code[pc + 1] << 8) | code[pc + 2],
                        _ => null
                    };

                    // describe const / field / method info
                    var desc = string.Empty;

                    if (index != null)
                        desc = _descriptionProvider.GetInfo(found.InfoType, index.Value, info);

                    // calculate branch target
                    int? branch = null;
                    if (found.IsBranch)
                    {
                        var branchbyte1 = code[pc + 1];
                        var branchbyte2 = code[pc + 2];
                        var branchbyte = (short)((branchbyte1 << 8) | branchbyte2);
                        branch = pc + branchbyte;
                    }

                    // add new data
                    opcodes.Add(new(pc, found.Name, index, branch, desc));

                    // update program counter
                    if (increment != found.Size)
                        pc = increment;
                    else
                        pc += increment;
                }

                return opcodes.ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to disassemble code!");
                return Array.Empty<OpcodeInfo>();
            }

        }

        /// <inheritdoc/>
        public string GetDisasmString(JavaMethod method, CpInfo[] info)
        {
            if (method.CodeAttribute == null)
                return string.Empty;

            var opcodes = GetOpcodes(method.CodeAttribute.Code, info);

            if (opcodes.Length == 0)
                return "No code";

            var sb = new StringBuilder();
            sb.AppendLine($"{method.AccessFlags} {method.Name}");
            sb.AppendLine("Code:");
            Array.ForEach(opcodes, op => sb.AppendLine(op.ToString()));

            return sb.ToString();
        }

        /// <summary>
        /// Gets program counter increment value
        /// </summary>
        /// <param name="instr">Instruction info</param>
        /// <param name="pc">Program counter</param>
        /// <param name="codeAttr">Code attribute</param>
        /// <returns>An integer value to increment program counter</returns>
        private static int GetIncrement(OpcodeData instr, int pc, byte[] code)
        {
            var increment = instr.Size;

            // tableswitch: special handling for variable-length instructions 
            if (instr.Opcode == 0xaa)
            {
                increment = pc + 1;

                // between zero and three bytes must act as padding
                while (increment % 4 != 0)
                    increment++;

                var defaultbyte1 = code[increment++];
                var defaultbyte2 = code[increment++];
                var defaultbyte3 = code[increment++];
                var defaultbyte4 = code[increment++];
                var defaultbyte = (defaultbyte1 << 24) | (defaultbyte2 << 16) | (defaultbyte3 << 8) | defaultbyte4;

                var lowbyte1 = code[increment++];
                var lowbyte2 = code[increment++];
                var lowbyte3 = code[increment++];
                var lowbyte4 = code[increment++];
                var low = (lowbyte1 << 24) | (lowbyte2 << 16) | (lowbyte3 << 8) | lowbyte4;

                var highbyte1 = code[increment++];
                var highbyte2 = code[increment++];
                var highbyte3 = code[increment++];
                var highbyte4 = code[increment++];
                var high = (highbyte1 << 24) | (highbyte2 << 16) | (highbyte3 << 8) | highbyte4;

                var count = high - low + 1;
                var offsets = new int[count];

                for (var i = 0; i < count; i++)
                {
                    offsets[i] = (code[increment++] << 24)
                               | (code[increment++] << 16)
                               | (code[increment++] << 8)
                               | code[increment++];
                }

                // TODO: use offsets info
            }
            // lookupswitch: special handling for variable-length instructions 
            else if (instr.Opcode == 0xab)
            {
                increment = pc + 1;

                // between zero and three bytes must act as padding
                while (increment % 4 != 0)
                    increment++;

                var defaultbyte1 = code[increment++];
                var defaultbyte2 = code[increment++];
                var defaultbyte3 = code[increment++];
                var defaultbyte4 = code[increment++];
                var defaultbyte = (defaultbyte1 << 24) | (defaultbyte2 << 16) | (defaultbyte3 << 8) | defaultbyte4;

                var npairs1 = code[increment++];
                var npairs2 = code[increment++];
                var npairs3 = code[increment++];
                var npairs4 = code[increment++];
                var npairs = (npairs1 << 24) | (npairs2 << 16) | (npairs3 << 8) | npairs4;


                var matches = new int[npairs];
                var offsets = new int[npairs];
                for (var i = 0; i < npairs; i++)
                {
                    matches[i] = (code[increment++] << 24)
                               | (code[increment++] << 16)
                               | (code[increment++] << 8)
                               | code[increment++];

                    offsets[i] = (code[increment++] << 24)
                               | (code[increment++] << 16)
                               | (code[increment++] << 8)
                               | code[increment++];
                }

                // TODO: use offsets info
            }

            return increment;
        }
    }
}
