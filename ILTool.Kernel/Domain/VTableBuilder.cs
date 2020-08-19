using ILTool.Kernel.Descriptions;
using ILTool.Kernel.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.Domain
{
    public class VTableBuilder
    {
        private Dictionary<MethodToken, MethodDesc> _methodsTable;
        private TypeDesc _typeDesc;
        private IInterfaceOffsetTable _ioTable;


        public VTableBuilder(TypeDesc typeDesc, IInterfaceOffsetTable ioTable)
        {
            _methodsTable = new Dictionary<MethodToken, MethodDesc>();
            _typeDesc = typeDesc;
            _ioTable = ioTable;
        }

        private void OverrideMethod(MethodDesc method)
        { 
            MethodDesc toOverride = null;
            if (_methodsTable.ContainsKey(method.Metadata))
                toOverride = _methodsTable[method.Metadata];

            if (toOverride == null || !toOverride.InheritanceAttributes.HasFlag(MethodInheritanceAttributes.Virtual))  //(IsOverride && !toOverride.IsVirtual))
                throw new ArgumentException("No method found in hierarchy to override");

            _methodsTable[method.Metadata] = method;
        }

        private void AddMethod(MethodDesc method)
        {
            _methodsTable.Add(method.Metadata, method);
        }

        private void RemoveBaseMethodIfExists(MethodDesc method)
        {
            if (_methodsTable.ContainsKey(method.Metadata))
                _methodsTable.Remove(method.Metadata); 
        }

        public VTableBuilder CloneBaseVTable(VTable baseVTable)
        {
            //Copy base type vtable methods to the current
            //var keyValuePairs = baseVTable.GetTable().Where(pair => pair.Value.Type == MethodType.Instance).Where(pair => pair.Value.IsVirtual || pair.Value.IsOverride);

            var keyValuePairs = baseVTable.GetTable().Where(pair => !pair.Value.InheritanceAttributes.HasFlag(MethodInheritanceAttributes.Static) && pair.Value.InheritanceAttributes.HasFlag(MethodInheritanceAttributes.Virtual));

            foreach (var pair in keyValuePairs) 
                _methodsTable.Add(pair.Key, pair.Value); 
             
            return this;
        }

        //public void AddTypeRecordsToIoTable()
        //{
        //    var interfaces = _typeDesc.ImplementedInterfaces; 
        //}

        public VTableBuilder ExtendBaseTypeVTable()
        {
            //var instanseMethods = _typeDesc.Methods.Where(m => m.Type == MethodType.Instance);
            var instanseMethods = _typeDesc.Methods.Where(m => !m.Props.HasFlag(MethodOtherProps.SpecialName) && !m.InheritanceAttributes.HasFlag(MethodInheritanceAttributes.Static)).ToList();

            //Replace overridden methods 
            //foreach (MethodDesc method in instanseMethods.Where(m => m.IsOverride))
            foreach (MethodDesc method in instanseMethods.Where(m => !m.InheritanceAttributes.HasFlag(MethodInheritanceAttributes.NewSlot) && m.InheritanceAttributes.HasFlag(MethodInheritanceAttributes.Virtual)))
                OverrideMethod(method);

            //Add introduced virtual (but not final) methods 
            //foreach (MethodDesc method in instanseMethods.Where(m => m.IsVirtual && !m.IsFinal))
            //foreach (MethodDesc method in instanseMethods.Where(m => m.IsVirtual && !m.IsFinal))
            //    _methodsTable.Add(method.Metadata, method);

            //remove class' new and hiding instance methods
            //foreach (MethodDesc method in instanseMethods.Where(m => !m.IsVirtual && !m.IsOverride && !m.IsAbstract))
            var introducedMethodsThatCanHideBaseMethods = instanseMethods.Where(m => !m.InheritanceAttributes.HasFlag(MethodInheritanceAttributes.Abstract))
                                                                         .Where(m => (m.InheritanceAttributes.HasFlag(MethodInheritanceAttributes.NewSlot) && m.InheritanceAttributes.HasFlag(MethodInheritanceAttributes.Virtual)) ||
                                                                                      (!m.InheritanceAttributes.HasFlag(MethodInheritanceAttributes.NewSlot) && !m.InheritanceAttributes.HasFlag(MethodInheritanceAttributes.Virtual)));
            
            //foreach (MethodDesc method in instanseMethods.Where(m => !m.IsVirtual && !m.IsOverride && !m.IsAbstract))
            foreach (MethodDesc method in introducedMethodsThatCanHideBaseMethods)
            {
                //remove the base method hidden by the current (if exists)
                RemoveBaseMethodIfExists(method);
                //Add
                AddMethod(method);
            }

            //Add special methods(CCtor, Ctors, etc)
            //foreach (MethodDesc ctors in _typeDesc.Methods.Where(m => m.Type == MethodType.CCtor || m.Type == MethodType.Ctor))
            foreach (MethodDesc ctors in _typeDesc.Methods.Where(m => m.Props.HasFlag(MethodOtherProps.SpecialName)))
                AddMethod(ctors);

            //Add class methods
            //foreach (MethodDesc method in _typeDesc.Methods.Where(m => m.Type == MethodType.Static))
            foreach (MethodDesc method in _typeDesc.Methods.Where(m => m.InheritanceAttributes.HasFlag(MethodInheritanceAttributes.Static)))
                AddMethod(method);

            return this; 
        }

        public VTable Create()
        {
            VTable vTable = new VTable(_methodsTable, _ioTable);
            return vTable;
        }
    }
}
