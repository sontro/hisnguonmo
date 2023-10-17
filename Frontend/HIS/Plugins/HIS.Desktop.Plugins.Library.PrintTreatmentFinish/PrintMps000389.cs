using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.PrintTreatmentFinish
{
    class PrintMps000389
    {
         MPS.Processor.Mps000389.PDO.Mps000389PDO mps000389RDO { get; set; }

        public PrintMps000389(string printTypeCode, string fileName, ref bool result, MOS.EFMODEL.DataModels.V_HIS_PATIENT HisPatient, MOS.EFMODEL.DataModels.HIS_TREATMENT HisTreatment, bool _printNow, long? roomId)
        {
            try
            {
                if (HisTreatment == null || HisTreatment.ID <= 0)
                {
                    result = false;
                    return;
                }

                mps000389RDO = new MPS.Processor.Mps000389.PDO.Mps000389PDO(
                   HisTreatment
                   );

                result = Print.RunPrint(printTypeCode, fileName, mps000389RDO, null
                    , result, _printNow, roomId);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
