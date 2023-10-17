using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using HIS.Desktop.LocalStorage.BackendData;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HIS.Desktop.Plugins.TransDepartment.Loader
{
    public class TreatmentTypeLoader
    {
        public static void LoadDataToComboTreatmentType(DevExpress.XtraEditors.LookUpEdit cboDepartment)
        {
            try
            {
                cboDepartment.Properties.DataSource = BackendDataWorker.Get<HIS_TREATMENT_TYPE>();
                cboDepartment.Properties.DisplayMember = "TREATMENT_TYPE_NAME";
                cboDepartment.Properties.ValueMember = "ID";
                cboDepartment.Properties.ForceInitialize();
                cboDepartment.Properties.Columns.Clear();
                cboDepartment.Properties.Columns.Add(new LookUpColumnInfo("TREATMENT_TYPE_CODE", "", 100));
                cboDepartment.Properties.Columns.Add(new LookUpColumnInfo("TREATMENT_TYPE_NAME", "", 200));
                cboDepartment.Properties.ShowHeader = false;
                cboDepartment.Properties.ImmediatePopup = true;
                cboDepartment.Properties.DropDownRows = 10;
                cboDepartment.Properties.PopupWidth = 300;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

      

    }
}
