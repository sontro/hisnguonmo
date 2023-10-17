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
using HIS.Desktop.LocalStorage.LocalData;
using HIS.Desktop.ADO;
using Inventec.Desktop.Common.Message;

namespace HIS.Desktop.Plugins.RegisterV3.Run3
{
    public partial class UCRegister : UserControlBase
    {
        private void SaveAndAssain()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = GlobalVariables.currentModuleRaws.Where(o => o.ModuleLink == "HIS.Desktop.Plugins.AssignService").FirstOrDefault();
                if (moduleData == null) throw new NullReferenceException("Not found module by ModuleLink = 'HIS.Desktop.Plugins.AssignService'");
                if (!moduleData.IsPlugin || moduleData.ExtensionInfo == null) throw new NullReferenceException("Module 'HIS.Desktop.Plugins.AssignService' is not plugins");

                List<object> listArgs = new List<object>();
                AssignServiceADO assignServiceADO = new AssignServiceADO(GetTreatmentIdFromResultData(), 0, 0, null);
                if (this._isPatientAppointmentCode == true && !String.IsNullOrEmpty(this.appointmentCode) && this._TreatmnetIdByAppointmentCode > 0)
                {
                    assignServiceADO.PreviusTreatmentId = this._TreatmnetIdByAppointmentCode;
                }
                assignServiceADO.IsAutoEnableEmergency = true;
                this.GetPatientInfoFromResultData(ref assignServiceADO);
                listArgs.Add(assignServiceADO);
                Inventec.Desktop.Common.Modules.Module module = PluginInstance.GetModuleWithWorkingRoom(moduleData, currentModule.RoomId, currentModule.RoomTypeId);
                var extenceInstance = HIS.Desktop.Utility.PluginInstance.GetPluginInstance(module, listArgs);
                if (extenceInstance == null) throw new ArgumentNullException("moduleData is null");

                ((Form)extenceInstance).ShowDialog();
            }
            catch (NullReferenceException ex)
            {
                WaitingManager.Hide();
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
