using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.Descriptions
{ 
    public enum MemberAccessibility
    {
        //
        // Summary:
        //     Indicates that the member cannot be referenced.
        PrivateScope = 0,
        //
        // Summary:
        //     Indicates that the method is accessible only to the current class.
        Private = 1,
        //
        // Summary:
        //     Indicates that the method is accessible to members of this type and its derived
        //     types that are in this assembly only.
        FamilyAndAssembly = 2,
        //
        // Summary:
        //     Indicates that the method is accessible to any class of this assembly.
        Assembly = 3,
        //
        // Summary:
        //     Indicates that the method is accessible only to members of this class and its
        //     derived classes.
        Family = 4,
        //
        // Summary:
        //     Indicates that the method is accessible to derived classes anywhere, as well
        //     as to any class in the assembly.
        FamilyOrAssembly = 5,
        //
        // Summary:
        //     Indicates that the method is accessible to any object for which this object is
        //     in scope.
        Public = 6,
    }

    [Flags]
    public enum MethodOtherProps
    {
        NotSet = 0,
        //
        // Summary:
        //     Indicates that the managed method is exported by thunk to unmanaged code.
        UnmanagedExport = 8,
        //
        // Summary:
        //     Indicates that the method hides by name and signature; otherwise, by name only.
        HideBySig = 128,
        //
        // Summary:
        //     Retrieves vtable attributes.
        VtableLayoutMask = 256,
        //
        // Summary:
        //     Indicates that the method implementation is forwarded through PInvoke (Platform
        //     Invocation Services).
        PinvokeImpl = 8192,
        //
        // Summary:
        //     Indicates that the method can only be overridden when it is also accessible.
        CheckAccessOnOverride = 512,
        //
        // Summary:
        //     Indicates that the common language runtime checks the name encoding.
        RTSpecialName = 4096,
        //
        // Summary:
        //     Indicates that the method has security associated with it. Reserved flag for
        //     runtime use only.
        HasSecurity = 16384,
        //
        // Summary:
        //     Indicates that the method calls another method containing security code. Reserved
        //     flag for runtime use only.
        RequireSecObject = 32768,
        //
        // Summary:
        //     Indicates a reserved flag for runtime use only.
        ReservedMask = 53248,
        //
        // Summary:
        //     Indicates that the method is special. The name describes how this method is special.
        SpecialName = 2048,
    }

    [Flags]
    public enum MethodInheritanceAttributes
    {
        NotSet = 0,

        Global = 1,
        //
        // Summary:
        //     Indicates that the method is defined on the type; otherwise, it is defined per
        //     instance.
        Static = 16,
        //
        // Summary:
        //     Indicates that the method cannot be overridden.
        Final = 32,
        //
        // Summary:
        //     Indicates that the method is virtual.
        Virtual = 64,
        //
        // Summary:
        //     Indicates that the method always gets a new slot in the vtable.
        NewSlot = 256,
        //
        // Summary:
        //     Indicates that the class does not provide an implementation of this method.
        Abstract = 1024
    }


    //public enum MethodType
    //{
    //    Global = 0,
    //    Static = 1,
    //    Instance = 2,
    //    Ctor = 3,
    //    CCtor = 4
    //}
}
