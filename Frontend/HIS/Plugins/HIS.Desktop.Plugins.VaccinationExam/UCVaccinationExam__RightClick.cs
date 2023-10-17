using DevExpress.XtraBars;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.LocalStorage.LocalData;
using HIS.Desktop.Plugins.VaccinationExam.Processors;
using Inventec.Common.Adapter;
using Inventec.Core;
using Inventec.Desktop.Common.Message;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.VaccinationExam
{
    public partial class UCVaccinationExam
    {
        void VaccinationExam_MouseRightClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (e.Item is BarButtonItem && this.vaccinationExam_ForProcess != null)
                {
                    var bbtnItem = sender as BarButtonItem;
                    PopupMenuProcessor.ItemType type = (PopupMenuProcessor.ItemType)(e.Item.Tag);
                    switch (type)
                    {
                        case PopupMenuProcessor.ItemType.HuyKetThuc:
                            bbtnHuyKetThucClick();
                            break;
                        case PopupMenuProcessor.ItemType.ThanhToan:
                            bbtnThanhToanClick();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void bbtnHuyKetThucClick()
        {
            try
            {
                ProcessHuyKetThuc(this.vaccinationExam_ForProcess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ProcessHuyKetThuc(V_HIS_VACCINATION_EXAM VaccinationExam)
        {
            try
            {
                if (VaccinationExam != null)
                {
                    bool success = false;
                    WaitingManager.Show();
                    CommonParam param = new CommonParam();
                    var apiResult = new BackendAdapter(param).Post<HIS_VACCINATION_EXAM>("api/HisVaccinationExam/CancelFinish", ApiConsumers.MosConsumer, VaccinationExam.ID, param);
                    if (apiResult != null)
                    {
                        success = true;
                        FillDataToGridVaccinationExam();
                    }
                    WaitingManager.Hide();
                    MessageManager.Show(this.ParentForm, param, success);
                }
            }
            catch (Exception ex)
            {
                WaitingManager.Hide();
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void bbtnThanhToanClick()
        {
            try
            {
                if (this.vaccinationExam_ForProcess != null)
                {
                    WaitingManager.Show();

                    Inventec.Desktop.Common.Modules.Module moduleData = GlobalVariables.currentModuleRaws.Where(o => o.ModuleLink == "HIS.Desktop.Plugins.MedicineVaccinBill").FirstOrDefault();
                    if (moduleData == null) throw new NullReferenceException("Not found module by ModuleLink :" + "HIS.Desktop.Plugins.MedicineVaccinBill");
                    if (!moduleData.IsPlugin || moduleData.ExtensionInfo == null) throw new NullReferenceException("Module '" + "HIS.Desktop.Plugins.MedicineVaccinBill" + "' is not plugins");


                    List<object> listArgs = new List<object>();
                    listArgs.Add(this.CurrentModule);
                    listArgs.Add(this.vaccinationExam_ForProcess.TDL_PATIENT_CODE);

                    var extenceInstance = HIS.Desktop.Utility.PluginInstance.GetPluginInstance(moduleData, listArgs);
                    if (extenceInstance == null) throw new ArgumentNullException("ModuleData is null");

                    WaitingManager.Hide();
                    ((Form)extenceInstance).ShowDialog();
                }
            }
            catch (Exception ex)
            {
                WaitingManager.Hide();
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
