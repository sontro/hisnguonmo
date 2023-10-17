using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HIS.Desktop.LocalStorage.BackendData;
using MOS.EFMODEL.DataModels;
using HIS.UC.SecondaryIcd;
using Inventec.Core;
using Inventec.Common.Controls.EditorLoader;
using DevExpress.XtraEditors;
using MOS.Filter;
using Inventec.Common.Adapter;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Utility;

namespace HIS.Desktop.Plugins.ApprovalSurgery
{
    public partial class UCApprovalSurgery : UserControlBase
    {
        public void FindShortCut()
        {
            if (btnFind.Enabled)
            {
                btnFind_Click(null, null);
            }
            if (btnTimKiem.Enabled)
            {
                btnTimKiem_Click(null, null);
            }
        }

        public void SaveShortCut()
        {
            if (btnSave.Enabled)
            {
                btnSave_Click(null, null);
            }
        }

        public void In()
        {
            if (btnIn.Enabled)
            {
                btnIn_Click(null, null);
            }
        }

    }
}
