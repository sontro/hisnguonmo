using Inventec.Common.Controls.EditorLoader;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.Bordereau.ChooseEquipmentSet
{
    public partial class frmEquipmentSet : Form
    {
        private void LoadComboEquipment()
        {
            try
            {
                List<HIS_EQUIPMENT_SET> listEquipment = HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get<HIS_EQUIPMENT_SET>(false, true);

                List<ColumnInfo> columnInfos = new List<ColumnInfo>();
                columnInfos.Add(new ColumnInfo("EQUIPMENT_SET_NAME", "", 250, 2));
                ControlEditorADO controlEditorADO = new ControlEditorADO("EQUIPMENT_SET_NAME", "ID", columnInfos, false, 250);
                ControlEditorLoader.Load(cboEquipmentSet, listEquipment, controlEditorADO);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void LoadDataDefault()
        {
            try
            {
                cboEquipmentSet.EditValue = equipmentId.HasValue ? equipmentId : null;
                spinNumOrder.Value = (decimal) (numOrder.HasValue ? numOrder : 1);
                if (controlType == CONTROL_TYPE.HIDE_NUMORDER)
                {
                    //spinNumOrder.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
			
        }
    }
}
