using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.CareCreate
{
    public partial class CareCreate : HIS.Desktop.Utility.FormBase
    {
        internal static void LoadDataToComboCareType(DevExpress.XtraEditors.LookUpEdit cboCareType, List<MOS.EFMODEL.DataModels.HIS_CARE_TYPE> data)
        {
            try
            {
                cboCareType.Properties.DataSource = data;
                cboCareType.Properties.DisplayMember = "CARE_TYPE_NAME";
                cboCareType.Properties.ValueMember = "ID";
                cboCareType.Properties.ForceInitialize();
                cboCareType.Properties.Columns.Clear();
                cboCareType.Properties.Columns.Add(new LookUpColumnInfo("CARE_TYPE_CODE", "", 100));
                cboCareType.Properties.Columns.Add(new LookUpColumnInfo("CARE_TYPE_NAME", "", 200));
                cboCareType.Properties.ShowHeader = false;
                cboCareType.Properties.ImmediatePopup = true;
                cboCareType.Properties.DropDownRows = 10;
                cboCareType.Properties.PopupWidth = 300;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        internal static void LoadDataToComboCareType(DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cboCareType, List<MOS.EFMODEL.DataModels.HIS_CARE_TYPE> data)
        {
            try
            {
                cboCareType.DataSource = data;
                cboCareType.DisplayMember = "CARE_TYPE_NAME";
                cboCareType.ValueMember = "ID";
                cboCareType.ForceInitialize();
                cboCareType.Columns.Clear();
                cboCareType.Columns.Add(new LookUpColumnInfo("CARE_TYPE_CODE", "", 100));
                cboCareType.Columns.Add(new LookUpColumnInfo("CARE_TYPE_NAME", "", 200));
                cboCareType.ShowHeader = false;
                cboCareType.ImmediatePopup = true;
                cboCareType.DropDownRows = 10;
                cboCareType.PopupWidth = 300;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        internal static void LoadDataToComboCareType(DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit cboCareType, List<MOS.EFMODEL.DataModels.HIS_CARE_TYPE> data)
        {
            try
            {
                cboCareType.DataSource = data;
                cboCareType.DisplayMember = "CARE_TYPE_NAME";
                cboCareType.ValueMember = "ID";
                cboCareType.NullText = "";

                cboCareType.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
                cboCareType.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
                cboCareType.ImmediatePopup = true;
                cboCareType.View.Columns.Clear();

                GridColumn aColumnCode = cboCareType.View.Columns.AddField("CARE_TYPE_CODE");
                aColumnCode.Caption = "Mã";
                aColumnCode.Visible = false;
                aColumnCode.VisibleIndex = 1;
                aColumnCode.Width = 100;

                GridColumn aColumnName = cboCareType.View.Columns.AddField("CARE_TYPE_NAME");
                aColumnName.Caption = "Tên";
                aColumnName.Visible = true;
                aColumnName.VisibleIndex = 2;
                aColumnName.Width = 400;

                cboCareType.View.OptionsView.ShowColumnHeaders = false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }


    }
}
