using HIS.Desktop.ApiConsumer;
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
        private void LoadBieuMauGiayRaVien(string printTypeCode, string fileName, ref bool result)
        {
            try
            {
   

                HisTreatmentFilter treatmentFilter = new HisTreatmentFilter();
                treatmentFilter.ID = this.HisServiceReqView.TREATMENT_ID;
                HIS_TREATMENT treatment = new BackendAdapter(new CommonParam())
                    .Get<List<MOS.EFMODEL.DataModels.HIS_TREATMENT>>("api/HisTreatment/Get", ApiConsumers.MosConsumer, treatmentFilter, new CommonParam()).FirstOrDefault();

                PrintTreatmentFinishProcessor printTreatmentFinishProcessor = new PrintTreatmentFinishProcessor(treatment, currentModuleBase != null ? currentModuleBase.RoomId : 0);
                printTreatmentFinishProcessor.Print(MPS.Processor.Mps000008.PDO.Mps000008PDO.printTypeCode,false);
     

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
