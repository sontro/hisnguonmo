using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.LocalData;
using HIS.Desktop.Utility;
using HIS.UC.WorkPlace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.Register.Run
{
    public partial class UCRegister : UserControlBase
    {
        async void InitWorkPlaceControl()
        {
            try
            {
                Inventec.Common.Logging.LogSystem.Debug("InitWorkPlaceControl Start!");
                this.workPlaceProcessor = new WorkPlaceProcessor(new Inventec.Core.CommonParam());
                WorkPlaceInitADO workPlaceInitADO = new WorkPlaceInitADO();
                if (HIS.Desktop.Plugins.Library.RegisterConfig.AppConfigs.CheDoHienThiNoiLamViecManHinhDangKyTiepDon == 1)
                {
                    workPlaceInitADO.Template = WorkPlaceProcessor.Template.Textbox1;
                    this.workPlaceTemplate = WorkPlaceProcessor.Template.Textbox1;
                }
                else
                {
                    workPlaceInitADO.Template = WorkPlaceProcessor.Template.Combo1;
                    this.workPlaceTemplate = WorkPlaceProcessor.Template.Combo1;
                }
                workPlaceInitADO.FocusMoveout = WorkPlaceFocusMoveout;
                workPlaceInitADO.PlusClick = WorkPlacePlusClick;
                workPlaceInitADO.WorlPlaces = BackendDataWorker.Get<MOS.EFMODEL.DataModels.HIS_WORK_PLACE>().Where(p => p.IS_ACTIVE == 1).ToList();
                this.ucWorkPlace = (await this.workPlaceProcessor.Generate(workPlaceInitADO).ConfigureAwait(false)) as UserControl;
                if (this.ucWorkPlace != null)
                {
                    this.ucWorkPlace.Dock = DockStyle.Fill;
                    this.pnlWorkPlace.Controls.Add(this.ucWorkPlace);
                }
                Inventec.Common.Logging.LogSystem.Debug("InitWorkPlaceControl Finished!");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        void WorkPlaceFocusMoveout()
        {
            try
            {
                this.txtMilitaryRankCode.Focus();
                this.txtMilitaryRankCode.SelectAll();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        void WorkPlacePlusClick()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = GlobalVariables.currentModuleRaws.Where(o => o.ModuleLink == "HIS.Desktop.Plugins.HisWorkPlace").FirstOrDefault();
                if (moduleData == null) throw new NullReferenceException("Not found module by ModuleLink = 'HIS.Desktop.Plugins.HisWorkPlace'");
                if (!moduleData.IsPlugin || moduleData.ExtensionInfo == null) throw new NullReferenceException("Module 'HIS.Desktop.Plugins.HisWorkPlace' is not plugins");

                List<object> listArgs = new List<object>();
                var extenceInstance = HIS.Desktop.Utility.PluginInstance.GetPluginInstance(PluginInstance.GetModuleWithWorkingRoom(moduleData, this.currentModule.RoomId, this.currentModule.RoomTypeId), listArgs);
                if (extenceInstance == null) throw new ArgumentNullException("moduleData is null");

                ((Form)extenceInstance).ShowDialog();

                BackendDataWorker.Reset<MOS.EFMODEL.DataModels.HIS_WORK_PLACE>();
                this.workPlaceProcessor.Reload(WorkPlaceProcessor.Template.Combo1, BackendDataWorker.Get<MOS.EFMODEL.DataModels.HIS_WORK_PLACE>().Where(p => p.IS_ACTIVE == 1).ToList());
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

    }
}
