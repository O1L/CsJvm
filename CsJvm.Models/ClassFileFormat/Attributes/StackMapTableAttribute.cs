namespace CsJvm.Models.ClassFileFormat.Attributes
{
    public partial class StackMapTableAttribute : AttributeInfo
    {
        public ushort NumberOfEntries { get; set; }

        public StackMapFrame[] Entries { get; set; } = Array.Empty<StackMapFrame>();
    }

    #region stack_map_frame
    public abstract class StackMapFrame
    {
        public abstract byte FrameType { get; set; }
    }

    public class FSameFrame : StackMapFrame
    {
        public override byte FrameType { get; set; }
    }

    public class SameLocals1StackItemFrame : StackMapFrame
    {
        public override byte FrameType { get; set; }
        public VerificationTypeInfo[] Stack { get; set; } = Array.Empty<VerificationTypeInfo>();
    }

    public class SameLocals1StackItemFrameExtended : StackMapFrame
    {
        public override byte FrameType { get; set; }
        public ushort OffsetDelta { get; set; }
        public VerificationTypeInfo[] Stack { get; set; } = Array.Empty<VerificationTypeInfo>();
    }

    public class ChopFrame : StackMapFrame
    {
        public override byte FrameType { get; set; }
        public ushort OffsetDelta { get; set; }
    }

    public class SameFrameExtended : StackMapFrame
    {
        public override byte FrameType { get; set; }
        public ushort OffsetDelta { get; set; }
    }

    public class AppendFrame : StackMapFrame
    {
        public override byte FrameType { get; set; }
        public ushort OffsetDelta { get; set; }
        public VerificationTypeInfo[] Locals { get; set; } = Array.Empty<VerificationTypeInfo>();
    }

    public class FullFrame : StackMapFrame
    {
        public override byte FrameType { get; set; }
        public ushort OffsetDelta { get; set; }
        public ushort NumberOfLocals { get; set; }
        public VerificationTypeInfo[] Locals { get; set; } = Array.Empty<VerificationTypeInfo>();
        public ushort NumberOfStackItems { get; set; }
        public VerificationTypeInfo[] Stack { get; set; } = Array.Empty<VerificationTypeInfo>();
    }

    public enum SameTypes
    {
        SAME,
        SAME_LOCALS_1_STACK_ITEM = 64,
        SAME_LOCALS_1_STACK_ITEM_EXTENDED = 247,
        CHOP = 248,
        SAME_FRAME_EXTENDED = 251,
        APPEND = 252,
        FULL_FRAME = 255
    }


    #endregion

    #region verification_type_info
    public abstract class VerificationTypeInfo
    {
        public abstract byte Tag { get; set; }
    }

    public class TopVariableInfo : VerificationTypeInfo
    {
        public override byte Tag { get; set; }
    }

    public class IntegerVariableInfo : VerificationTypeInfo
    {
        public override byte Tag { get; set; }
    }

    public class FloatVariableInfo : VerificationTypeInfo
    {
        public override byte Tag { get; set; }
    }

    public class LongVariableInfo : VerificationTypeInfo
    {
        public override byte Tag { get; set; }
    }

    public class DoubleVariableInfo : VerificationTypeInfo
    {
        public override byte Tag { get; set; }
    }

    public class NullVariableInfo : VerificationTypeInfo
    {
        public override byte Tag { get; set; }
    }

    public class ObjectVariableInfo : VerificationTypeInfo
    {
        public override byte Tag { get; set; }
        public ushort CPoolIndex { get; set; }
    }

    public class UninitializedVariableInfo : VerificationTypeInfo
    {
        public override byte Tag { get; set; }
        public ushort Offset { get; set; }
    }

    public enum VariableTags
    {
        ITEM_Top = 0,
        ITEM_Integer = 1,
        ITEM_Float = 2,
        ITEM_Double = 3,
        ITEM_Long = 4,
        ITEM_Null = 5,
        ITEM_UninitializedThis = 6,
        ITEM_Object = 7,
        ITEM_Uninitialized = 8
    }

    #endregion
}
