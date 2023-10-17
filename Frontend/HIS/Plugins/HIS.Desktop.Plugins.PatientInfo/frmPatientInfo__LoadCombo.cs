using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.LocalStorage.BackendData;
using Inventec.Common.Adapter;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.PatientInfo
{
    public partial class frmPatientInfo : HIS.Desktop.Utility.FormBase
    {
        void FillDataToLookupedit(LookUpEdit cboEditor, string displayMember, string valueMember, string displayCodeMember, object datasource)
        {
            try
            {
                cboEditor.Properties.DataSource = datasource;
                cboEditor.Properties.DisplayMember = displayMember;
                cboEditor.Properties.ValueMember = valueMember;
                cboEditor.Properties.ForceInitialize();
                cboEditor.Properties.Columns.Clear();
                cboEditor.Properties.Columns.Add(new LookUpColumnInfo(displayCodeMember, "", 50));
                cboEditor.Properties.Columns.Add(new LookUpColumnInfo(displayMember, "", 100));
                cboEditor.Properties.ShowHeader = false;
                cboEditor.Properties.ImmediatePopup = true;
                cboEditor.Properties.DropDownRows = 20;
                cboEditor.Properties.PopupWidth = 300;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }

        }

        void FillDataToLookupedit1(LookUpEdit cboEditor, string valueMember, string displayCodeMember, object datasource)
        {
            try
            {
                cboEditor.Properties.DataSource = datasource;
                cboEditor.Properties.DisplayMember = displayCodeMember;
                cboEditor.Properties.ValueMember = valueMember;
                cboEditor.Properties.ForceInitialize();
                cboEditor.Properties.Columns.Clear();
                cboEditor.Properties.Columns.Add(new LookUpColumnInfo(displayCodeMember, "", 50));
                cboEditor.Properties.ShowHeader = false;
                cboEditor.Properties.ImmediatePopup = true;
                cboEditor.Properties.DropDownRows = 10;
                cboEditor.Properties.PopupWidth = 50;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }

        }

        void FillDataToLookupedit(DevExpress.XtraEditors.LookUpEdit cboEditor, string displayMember, string valueMember, object datasource)
        {
            try
            {
                cboEditor.Properties.DataSource = datasource;
                cboEditor.Properties.DisplayMember = displayMember;
                cboEditor.Properties.ValueMember = valueMember;
                cboEditor.Properties.ForceInitialize();
                cboEditor.Properties.Columns.Clear();
                //cboEditor.Properties.Columns.Add(new LookUpColumnInfo(displayCodeMember, "", 50));
                cboEditor.Properties.Columns.Add(new LookUpColumnInfo(displayMember, "", 100));
                cboEditor.Properties.ShowHeader = false;
                cboEditor.Properties.ImmediatePopup = true;
                cboEditor.Properties.DropDownRows = 20;
                cboEditor.Properties.PopupWidth = 300;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }

        }

        private void FocusShowPopup(DateEdit cboEditor)
        {
            cboEditor.Focus();
            cboEditor.ShowPopup();
        }

        private void FocusShowPopup(LookUpEdit cboEditor)
        {
            cboEditor.Focus();
            cboEditor.ShowPopup();
        }
        private void FocusMoveText(TextEdit txt)
        {
            try
            {
                txt.Focus();
                txt.SelectAll();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }


    }
}
