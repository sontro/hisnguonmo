using DevExpress.XtraLayout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.RegisterV2
{
    class LayoutControlUtil
    {
        internal static LayoutControlItem AddToLayout(UserControl ucControl, bool textVisible, System.Drawing.Size size, System.Drawing.Size textSize, SizeConstraintsType sizeConstraintsType, System.Drawing.Size maxSize, System.Drawing.Size minSize)
        {
            int dem = 0;
            return  new LayoutControlItem
            {
                Control = ucControl,
                Name = String.Format("{0}{1}", ucControl.Name, dem),
                TextVisible = textVisible,
                Size = size,
                TextSize = textSize,
                SizeConstraintsType = sizeConstraintsType,
                MaxSize = maxSize,
                MinSize = minSize
            };
        }

        internal static void Move(LayoutControlItem itemMove, LayoutControlItem itemCenter, DevExpress.XtraLayout.Utils.InsertType insertType)
        {
            itemMove.Move(itemCenter, insertType);
        }
    }
}
