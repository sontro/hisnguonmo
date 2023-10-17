using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MOS.EFMODEL.DataModels;
using MOS.SDO;
using DevExpress.Data;
using System.Collections;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.Utils.Menu;
using HIS.Desktop.Plugins.RehaServiceReqExecute.Base;
using Inventec.Desktop.Common.LanguageManager;
using AutoMapper;
using Inventec.Core;
using Inventec.Desktop.Common.Message;
using Inventec.Common.Adapter;
using HIS.Desktop.ApiConsumer;
using Inventec.Common.Logging;
using Inventec.Common.RichEditor.Base;
using HIS.Desktop.Controls.Session;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;
using HIS.Desktop.Plugins.RehaServiceReqExecute.ADO;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace HIS.Desktop.Plugins.RehaServiceReqExecute
{
    public partial class RehaServiceReqExecuteControl : HIS.Desktop.Utility.UserControlBase
    {
        public void SaveShortCut()
        {
            try
            {
                btnSave.Focus();
                btnSave_Click(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public void FinishShortCut()
        {
            try
            {
                btnFinish_Click(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public void ThemTapShortCut()
        {
            try
            {
                btnThemTTTap.Focus();
                btnThemTTTap_Click(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public void AssignServiceShortCut()
        {
            try
            {
                btnAssignService_Click_1(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public void AssignPreShortCut()
        {
            try
            {
                if (btnAssignPre.Enabled)
                    btnAssignPre_Click(null, null);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
