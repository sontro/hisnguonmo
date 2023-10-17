using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Nodes.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventec.Desktop.Plugins.MedicalStore.CheckBoxProcessor
{
    class CheckBoxProcessor : TreeListOperation
    {
        public List<TreeListNode> CheckedNodes = new List<TreeListNode>();
        public CheckBoxProcessor() : base() { }
        public override void Execute(TreeListNode node)
        {
            if (node.CheckState != CheckState.Unchecked)
                CheckedNodes.Add(node);
        }
    }
}
