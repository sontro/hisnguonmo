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

namespace HIS.Desktop.Plugins.Library.PrintTreatmentEndTypeExt.MpsBehavior.Mps000389
{
    public class Mps000389Behavior : PrintDataBase, ILoad
    {
        public Mps000389Behavior(long treatmentId, long? roomId)
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

                MPS.Processor.Mps000389.PDO.Mps000389PDO rdo = new MPS.Processor.Mps000389.PDO.Mps000389PDO(this.Treatment);
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

    }
}
