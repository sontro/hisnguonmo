using HIS.Desktop.Print;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace HIS.Desktop.Plugins.Library.PrintServiceReq
{
    class InPhieuXetNghiemDomSoi
    {
        public InPhieuXetNghiemDomSoi(string printTypeCode, string fileName, ADO.ChiDinhDichVuADO chiDinhDichVuADO,
            Dictionary<long, List<MOS.EFMODEL.DataModels.V_HIS_SERVICE_REQ>> dicServiceReqData,
            Dictionary<long, List<MOS.EFMODEL.DataModels.V_HIS_SERE_SERV>> dicSereServData,
            bool printNow, ref bool result, long? roomId, MPS.ProcessorBase.PrintConfig.PreviewType? PreviewType,
            Action<int, Inventec.Common.FlexCelPrint.Ado.PrintMergeAdo> savedData,
            Action<string> cancelPrint, Action<Inventec.Common.SignLibrary.DTO.DocumentSignedUpdateIGSysResultDTO> DlgSendResultSigned)
        {
            try
            {
                var lstServieReq_27 = dicServiceReqData[IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN].ToList();
                if (lstServieReq_27 == null || lstServieReq_27.Count != 1) return;

                var patientADO = PrintGlobalStore.GetPatientById(chiDinhDichVuADO.treament.PATIENT_ID);

                MPS.Processor.Mps000027.PDO.Mps000027PDO mps000027RDO = new MPS.Processor.Mps000027.PDO.Mps000027PDO(
                    patientADO,
                    lstServieReq_27.FirstOrDefault(),
                    chiDinhDichVuADO.Ratio);
                Print.PrintData(printTypeCode, fileName, mps000027RDO, printNow, ref result, roomId, false, PreviewType, lstServieReq_27.Count, savedData, lstServieReq_27.FirstOrDefault().TREATMENT_CODE, DlgSendResultSigned);
            }
            catch (Exception ex)
            {
                cancelPrint(printTypeCode);
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
