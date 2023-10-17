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
using DevExpress.Utils.Menu;
using Inventec.Desktop.Common.Message;
using HIS.Desktop.LocalStorage.LocalData;

namespace HIS.Desktop.Plugins.TestServiceReqExcute
{
    public partial class UCTestServiceReqExcute : UserControlBase
    {
        private void BtnRefreshForFormOther()
        {
            try
            {

                Inventec.Common.Logging.LogSystem.Debug("BtnRefreshForFormOther");
                drBtnOther.DropDownControl = null;
                //LoadTreatmentByPatient();
                //this.FillDataToButtonPrintAndAutoPrint();
                this.InitDrButtonOther();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private async Task InitDrButtonOther()
        {
            try
            {
                DXPopupMenu menu = new DXPopupMenu();
                DXMenuItem itemTutruc = new DXMenuItem("Kê đơn tủ trực", new EventHandler(onClickTutruc));
                menu.Items.Add(itemTutruc);

                if (this.checkNoiTru == true)
                {
                    DXMenuItem itemNoiTru = new DXMenuItem("Kê đơn nội trú", new EventHandler(onClickNoiTru));
                    menu.Items.Add(itemNoiTru);
                }
                
                DXMenuItem itemVatTuHaoPhi = new DXMenuItem("Vật tư hao phí", new EventHandler(onClickVatTuHaoPhi));
                menu.Items.Add(itemVatTuHaoPhi);

                DXMenuItem itemAssignPaan = new DXMenuItem("Chỉ định giải phẫu bệnh lý", new EventHandler(onClickAssignPaan));
                menu.Items.Add(itemAssignPaan);

                drBtnOther.DropDownControl = menu;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void onClickAssignPaan(object sender, EventArgs e)
        {
            try
            {
                if (this.currentServiceReq != null)
                {

                    Inventec.Desktop.Common.Modules.Module moduleData = GlobalVariables.currentModuleRaws.Where(o => o.ModuleLink == "HIS.Desktop.Plugins.AssignPaan").FirstOrDefault();
                    if (moduleData == null) Inventec.Common.Logging.LogSystem.Error("khong tim thay moduleLink = HIS.Desktop.Plugins.AssignPaan");
                    if (moduleData.IsPlugin && moduleData.ExtensionInfo != null)
                    {
                        List<object> listArgs = new List<object>();
                        listArgs.Add(this.currentServiceReq.TREATMENT_ID);
                        listArgs.Add(this.currentServiceReq.ID);

                        listArgs.Add(PluginInstance.GetModuleWithWorkingRoom(moduleData, this.currentModule.RoomId, this.currentModule.RoomTypeId));
                        var extenceInstance = PluginInstance.GetPluginInstance(PluginInstance.GetModuleWithWorkingRoom(moduleData, this.currentModule.RoomId, this.currentModule.RoomTypeId), listArgs);
                        if (extenceInstance == null) throw new ArgumentNullException("moduleData is null");

                        ((Form)extenceInstance).ShowDialog();
                    }

                }
            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void onClickVatTuHaoPhi(object sender, EventArgs e)
        {
            try
            {
                if (this.check == true)
                {
                    if (_adoVatTuHaoPhi != null)
                    {
                        WaitingManager.Show();
                        Inventec.Desktop.Common.Modules.Module moduleData = GlobalVariables.currentModuleRaws.Where(o => o.ModuleLink == "HIS.Desktop.Plugins.HisServiceReqMaty").FirstOrDefault();
                        if (moduleData == null) Inventec.Common.Logging.LogSystem.Error("khong tim thay moduleLink = HIS.Desktop.Plugins.HisServiceReqMaty");
                        if (moduleData.IsPlugin && moduleData.ExtensionInfo != null)
                        {
                            List<object> listArgs = new List<object>();
                            listArgs.Add(_adoVatTuHaoPhi.SERE_SERV_ID);
                            listArgs.Add(PluginInstance.GetModuleWithWorkingRoom(moduleData, this.currentModule.RoomId, this.currentModule.RoomTypeId));
                            var extenceInstance = PluginInstance.GetPluginInstance(PluginInstance.GetModuleWithWorkingRoom(moduleData, this.currentModule.RoomId, this.currentModule.RoomTypeId), listArgs);
                            if (extenceInstance == null) throw new ArgumentNullException("moduleData is null");

                            ((Form)extenceInstance).ShowDialog();
                        }
                        WaitingManager.Hide();
                    }
                }
            }
            catch (Exception ex)
            {
                WaitingManager.Hide();
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void onClickNoiTru(object sender, EventArgs e)
        {
            btnKeDonThuoc_Click(KeType.KeNoiTru);
        }

        private void onClickTutruc(object sender, EventArgs e)
        {
            btnKeDonThuoc_Click(KeType.KeTuTruc);
        }

    }
}
