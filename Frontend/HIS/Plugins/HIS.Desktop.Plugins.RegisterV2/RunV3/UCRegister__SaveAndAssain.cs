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
using HIS.Desktop.Plugins.Library.RegisterConfig;
using System.Reflection;

namespace HIS.Desktop.Plugins.RegisterV2.Run2
{
    public partial class UCRegister : UserControlBase
    {
		private List<string> lstModuleLinkApply;

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

                if(!IsApplyFormClosingOption(moduleData.ModuleLink))
                {
                    Inventec.Common.Logging.LogSystem.Debug("ExamServiceReqExecute.btnAssignService_Click.4");
                    var extenceInstance = PluginInstance.GetPluginInstance(HIS.Desktop.Utility.PluginInstance.GetModuleWithWorkingRoom(moduleData, this.currentModule.RoomId, this.currentModule.RoomTypeId), listArgs);

                    if (extenceInstance == null) throw new ArgumentNullException("moduleData is null");
                    ((Form)extenceInstance).ShowDialog();
                }
                else
                {
                    if (lstModuleLinkApply.FirstOrDefault(o => o == moduleData.ModuleLink) != null)
                    {
                        if (GlobalVariables.FormAssignService != null)
                        {
                            GlobalVariables.FormAssignService.WindowState = FormWindowState.Maximized;
                            GlobalVariables.FormAssignService.ShowInTaskbar = true;
                            Type classType = GlobalVariables.FormAssignService.GetType();
                            MethodInfo methodInfo = classType.GetMethod("ReloadModuleByInputData");
                            methodInfo.Invoke(GlobalVariables.FormAssignService, new object[] { currentModule, assignServiceADO });
                            GlobalVariables.FormAssignService.Activate();
                        }
                        else
                        {
                            GlobalVariables.FormAssignService = (Form)PluginInstance.GetPluginInstance(HIS.Desktop.Utility.PluginInstance.GetModuleWithWorkingRoom(moduleData, this.currentModule.RoomId, this.currentModule.RoomTypeId), listArgs);
                            GlobalVariables.FormAssignService.ShowInTaskbar = true;
                            if (GlobalVariables.FormAssignService == null) throw new ArgumentNullException("moduleData is null");
                            GlobalVariables.FormAssignService.Show();


                            Type classType = GlobalVariables.FormAssignService.GetType();
                            MethodInfo methodInfo = classType.GetMethod("ChangeIsUseApplyFormClosingOption");
                            methodInfo.Invoke(GlobalVariables.FormAssignService, new object[] { true });
                        }
                    }
                }
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
