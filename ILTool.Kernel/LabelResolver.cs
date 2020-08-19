using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILTool.Kernel
{
    public class LabelResolver : ILabelResolver
    {
        private Dictionary<string, int> _table;

        public LabelResolver()
        {
            _table = new Dictionary<string, int>();
        }

        public void ResolveInstructionLabels(ILInstruction[] instructions)
        {
            for (int i = 0; i < instructions.Length; i++)
            {
                //Adding labels
                if (instructions[i].Label != null)
                    _table.Add(instructions[i].Label, i);
            }
            //Replacing labels with instruction numbers
            for (int i = 0; i < instructions.Length; i++)
            {
                //Adding labels
                if (instructions[i].Operand is Label)
                {
                    var lname = (instructions[i].Operand as Label).Name;
                    if (_table.ContainsKey(lname))
                        instructions[i].Operand = _table[lname];
                    else
                        throw new InvalidOperationException("Label with this name was not declared");
                }
            }
        }

        public Int32 GetNumber(string lname)
        {
            lname = lname.Trim().ToLower();
            Int32 lblNum;
            if (_table.ContainsKey(lname))
                lblNum = _table[lname];
            else
                throw new InvalidOperationException("Label with this name was not declared");
            return lblNum;
        }
    }
}
