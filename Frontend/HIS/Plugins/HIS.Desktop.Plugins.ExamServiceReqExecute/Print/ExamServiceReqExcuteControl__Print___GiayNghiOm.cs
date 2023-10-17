using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Plugins.Library.PrintTreatmentEndTypeExt;
using HIS.Desktop.Plugins.Library.PrintTreatmentFinish;
using HIS.Desktop.Utility;
using Inventec.Common.Adapter;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using System;
using System.Collections.Generic;
using System.Linq;


namespace HIS.Desktop.Plugins.ExamServiceReqExecute
{
    public partial class ExamServiceReqExecuteControl : UserControlBase
    {
        private void LoadBieuMauGiayNghiOm(string printTypeCode, string fileName, ref bool result)
        {
            try
            {
                PrintTreatmentEndTypeExtProcessor printTreatmentEndTypeExtProcessor = new PrintTreatmentEndTypeExtProcessor(this.treatmentId, ReloadMenuTreatmentEndTypeExt, CreateMenu.TYPE.DYNAMIC, currentModuleBase != null ? currentModuleBase.RoomId : 0);

                printTreatmentEndTypeExtProcessor.Print(HIS.Desktop.Plugins.Library.PrintTreatmentEndTypeExt.Base.PrintTreatmentEndTypeExPrintType.TYPE.NGHI_OM, PrintTreatmentEndTypeExtProcessor.OPTION.PRINT);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void LoadBieuMauGiayHenMo(string printTypeCode, string fileName, ref bool result)
        {
            try
            {
                PrintTreatmentEndTypeExtProcessor printTreatmentEndTypeExtProcessor = new PrintTreatmentEndTypeExtProcessor(this.treatmentId, ReloadMenuTreatmentEndTypeExt, CreateMenu.TYPE.DYNAMIC, currentModuleBase != null ? currentModuleBase.RoomId : 0);

                printTreatmentEndTypeExtProcessor.Print(HIS.Desktop.Plugins.Library.PrintTreatmentEndTypeExt.Base.PrintTreatmentEndTypeExPrintType.TYPE.HEN_MO, PrintTreatmentEndTypeExtProcessor.OPTION.PRINT);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
