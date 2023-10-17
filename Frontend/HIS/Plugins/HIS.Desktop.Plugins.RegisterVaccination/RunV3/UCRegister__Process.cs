using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HIS.Desktop.Utility;
using HIS.Desktop.Plugins.Library.RegisterConfig;
using MOS.EFMODEL.DataModels;
using DevExpress.XtraLayout.Utils;

namespace HIS.Desktop.Plugins.RegisterVaccination.Run3
{
    public partial class UCRegister : UserControlBase
    {
        private void DefaultGroupCaseType()
        {

            try
            {
                //cboCaseType.EditValue = null;
                //chkOneMonth.Checked = false;
                //chkSick.Checked = false;
                //layoutControlItem25.Visibility = LayoutVisibility.Never;
                //layoutControlItem13.Visibility = LayoutVisibility.Never;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
			
        }

        private void FillDataDefaultToControl()
        {
            try
            {
                GetCaseType();
                this.InitComboCommon(this.cboCashierRoom, this.GetCashierRoomByUser(), "ID", "CASHIER_ROOM_NAME", "CASHIER_ROOM_CODE");
                //this.InitComboCommon(this.cboRoom, new List<V_HIS_EXECUTE_ROOM>(), "ID", "EXECUTE_ROOM_NAME", "EXECUTE_ROOM_CODE");
                //this.InitCombo1Column(this.cboCaseType, this.listCaseTypeAdos, "ID", "NAME");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void GetCaseType()
        {

            try
            {
                //listCaseTypeAdos = new List<ADO.CaseTypeADO>();
                //listCaseTypeAdos.Add(new ADO.CaseTypeADO(1, "Trẻ em"));
                //listCaseTypeAdos.Add(new ADO.CaseTypeADO(2, "Phụ nữ sau sinh"));
                //listCaseTypeAdos.Add(new ADO.CaseTypeADO(3, "Khác"));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }

        }

        private void SetDefaultCashierRoom()
        {
            try
            {
                var listCashier = GetCashierRoomByUser();
                if (listCashier != null && listCashier.Count == 1)
                {
                    this.cboCashierRoom.EditValue = listCashier.First().ID;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
