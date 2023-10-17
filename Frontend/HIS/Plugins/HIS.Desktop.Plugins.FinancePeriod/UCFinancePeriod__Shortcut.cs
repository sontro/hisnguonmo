using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventec.Common.Controls.EditorLoader;
using MOS.EFMODEL.DataModels;
using HIS.Desktop.LocalStorage.ConfigApplication;
using Inventec.Desktop.Common.Message;
using Inventec.Core;
using MOS.Filter;
using Inventec.Common.Adapter;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Controls.Session;
using Inventec.Common.Logging;

namespace HIS.Desktop.Plugins.FinancePeriod
{
    public partial class UCFinancePeriod : HIS.Desktop.Utility.UserControlBase
    {
        public void RefeshShortcut()
        {
            if (btnRefesh.Enabled)
            {
                btnRefesh_Click(null, null);
            }
        }
    }
}
