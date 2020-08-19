using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel.Metadata
{
    public enum FieldAccessibility
    {
        /// <summary>
        /// Public accessibility. §II.16.1.1
        /// </summary>
        Public,

        /// <summary>
        /// Assembly accessibility. §II.16.1.1
        /// </summary>
        Assembly,
         
        /// <summary>
        /// Family accessibility. §II.16.1.1
        /// </summary>
        Family,

        /// <summary>
        /// Family and Assembly accessibility. §II.16.1.1
        /// </summary>
        FamAndAssem,

        /// <summary>
        /// Family or Assembly accessibility. §II.16.1.1
        /// </summary>
        FamOrAssem,

        /// <summary>
        /// Private accessibility. §II.16.1.1
        /// </summary>
        Private,

        /// <summary>
        /// 
        /// </summary>
        PrivateScope
    }

    [Flags]
    public enum FieldContractAttr
    {
        /// <summary>
        /// initonly marks fields which are constant after they are initialized
        /// </summary>
        Initonly = 1,

        /// <summary>
        /// literal specifies that this field represents a constant value
        /// </summary>
        Literal = 2 ,
         
        /// <summary>
        /// static specifies that the field is associated with the type itself rather than with an instance of the type
        /// </summary>
        Static = 5 ,
        
        /// <summary>
        /// 
        /// </summary>
        Notserialized =11
    }

    public class FieldDef
    {
        public string Name { get; set; }
        public FieldAccessibility Access { get; set; }
        public FieldContractAttr Contract { get; set; }
        public ClassToken TypeToken { get; set; }
    }
}
