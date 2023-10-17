using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraTab;
using HIS.Desktop.Base;
using Inventec.Common.Logging;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Common.Modules;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraBars;
using HIS.Desktop.LocalStorage.LocalData;
using System.Linq;
using HIS.Desktop.ModuleExt;
using Inventec.Desktop.Common.Message;
using HIS.Desktop.LocalStorage.HisConfig;
using Inventec.Core;
using Inventec.Common.Adapter;
using HIS.Desktop.ApiConsumer;
using System.Diagnostics;
using System.IO;
using HIS.Desktop.Utilities.RemoteSupport;

namespace HIS.Desktop.Modules.Main
{
    public partial class frmMain : RibbonForm
    {
        void ProcessRemoteByRemoteDesktopSoftware()
        {
            try
            {
                var module = GlobalVariables.CurrentModuleSelected;
                frmRemoteSupportCreate frmRemoteSupportCreate = new frmRemoteSupportCreate(module);
                frmRemoteSupportCreate.ShowDialog();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }            
        }

    }
}