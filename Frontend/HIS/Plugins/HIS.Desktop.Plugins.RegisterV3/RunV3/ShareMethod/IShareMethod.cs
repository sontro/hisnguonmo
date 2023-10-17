using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace HIS.Desktop.Plugins.RegisterV3.Run3
{
    interface IShareMethod
    {
        void FocusShowpopup(LookUpEdit cboEditor, bool isSelectFirstRow);

        void InitComboCommon(Control cboEditor, object data, string valueMember, string displayMember, string displayMemberCode);
    }
}
