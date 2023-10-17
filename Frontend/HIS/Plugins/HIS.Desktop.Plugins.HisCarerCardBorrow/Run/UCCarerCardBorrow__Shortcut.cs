using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Inventec.Desktop.Common.Message;
using MOS.EFMODEL.DataModels;
using HIS.Desktop.LocalStorage.BackendData;
using DevExpress.XtraEditors.Controls;
using MOS.SDO;
using MOS.Filter;
using Inventec.Core;
using HIS.Desktop.ApiConsumer;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ViewInfo;

using DevExpress.Utils.Menu;
using Inventec.Common.Adapter;

using HIS.Desktop.Controls.Session;
using DevExpress.Data;
using System.Collections;
using DevExpress.XtraGrid.Views.Base;
using HIS.Desktop.Utility;

namespace HIS.Desktop.Plugins.HisCarerCardBorrow.Run
{
    public partial class UCCarerCardBorrow : UserControlBase 
    {
        public void BtnFind()
        {
            try
            {
                if (btnFind.Enabled)
                {
                    btnFind.Focus();
                    btnFind_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
        public void BtnAddShortcut()
        {
            try
            {
                if (btnBorrow.Enabled)
                {
                    btnBorrow.Focus();
                    btnBorrow_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
        public void BtnRefresh()
        {
            try
            {
                if (btnRefresh.Enabled)
                {
                    btnRefresh.Focus();
                    btnRefresh_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
