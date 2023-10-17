using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Plugins.Library.PrintTreatmentEndTypeExt.Base;
using Inventec.Common.Adapter;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.PrintTreatmentEndTypeExt.MpsBehavior.Mps000297
{
    public class Mps000297Behavior : PrintDataBase, ILoad
    {
        public HIS_SERE_SERV sereServ;

        public Mps000297Behavior(long treatmentId, long? roomId)
            : base(treatmentId, roomId)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        bool ILoad.Run(string mpsCode, string fileName, bool printNow)
        {
            bool result = false;
            try
            {
                this.LoadData();
                //this.LoadSereServ();

                MPS.Processor.Mps000297.PDO.Mps000297PDO rdo = new MPS.Processor.Mps000297.PDO.Mps000297PDO(this.Treatment, this.PatientTypeAlter, this.Patient, this.sereServ);
                Inventec.Common.SignLibrary.ADO.InputADO inputADO = new HIS.Desktop.Plugins.Library.EmrGenerate.EmrGenerateProcessor().GenerateInputADOWithPrintTypeCode(this.Treatment != null ? this.Treatment.TREATMENT_CODE : "", mpsCode, roomId);
                if (printNow)
                    result = MPS.MpsPrinter.Run(new MPS.ProcessorBase.Core.PrintData(mpsCode, fileName, rdo, MPS.ProcessorBase.PrintConfig.PreviewType.PrintNow, "") { EmrInputADO = inputADO });
                else
                    result = MPS.MpsPrinter.Run(new MPS.ProcessorBase.Core.PrintData(mpsCode, fileName, rdo, MPS.ProcessorBase.PrintConfig.PreviewType.Show, "") { EmrInputADO = inputADO });
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }

        public void LoadSereServ()
        {
            try
            {
                CommonParam param = new CommonParam();
                MOS.Filter.HisSereServFilter filter = new MOS.Filter.HisSereServFilter();
                filter.TREATMENT_ID = this.TreatmentId;
                filter.TDL_SERVICE_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH;
                filter.ORDER_FIELD = "TDL_INTRUCTION_TIME";
                filter.ORDER_DIRECTION = "ASC";

                var sereServs = new BackendAdapter(param)
                    .Get<List<MOS.EFMODEL.DataModels.HIS_SERE_SERV>>("api/HisSereServ/Get", ApiConsumers.MosConsumer, filter, param);

                if (sereServs != null && sereServs.Count > 0)
                {
                    this.sereServ = sereServs.OrderBy(p => p.TDL_INTRUCTION_TIME).ToList().FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
