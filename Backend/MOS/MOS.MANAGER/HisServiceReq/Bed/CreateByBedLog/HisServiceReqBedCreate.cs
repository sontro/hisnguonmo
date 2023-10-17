using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisRoom;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisBedLog;
using MOS.MANAGER.Token;
using MOS.MANAGER.EventLogUtil;
using AutoMapper;
using MOS.MANAGER.HisPatientTypeAlter;

namespace MOS.MANAGER.HisServiceReq.Bed.CreateByBedLog
{
    /// <summary>
    /// Chi dinh dich vu giuong dua vao thong tin su dung giuong (bed_log)
    /// </summary>
    partial class HisServiceReqBedCreate : BusinessBase
    {
        private HisServiceReqProcessor hisServiceReqProcessor;
        private HisSereServProcessor hisSereServProcessor;
        private HisBedLogProcessor hisBedLogProcessor;

        internal HisServiceReqBedCreate()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqBedCreate(CommonParam create)
            : base(create)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisServiceReqProcessor = new HisServiceReqProcessor(param);
            this.hisSereServProcessor = new HisSereServProcessor(param);
            this.hisBedLogProcessor = new HisBedLogProcessor(param);
        }

        internal bool Run(HisBedServiceReqSDO sdo, ref HisServiceReqListResultSDO resultData)
        {
            bool result = false;
            try
            {
                WorkPlaceSDO workPlace = null;
                HIS_TREATMENT treatment = null;
                List<HIS_PATIENT_TYPE_ALTER> ptas = null;

                HisServiceReqBedCheck checker = new HisServiceReqBedCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);

                string sessionCode = Guid.NewGuid().ToString();
                
                bool valid = true;
                valid = valid && this.HasWorkPlaceInfo(sdo.RequestRoomId, ref workPlace);
                valid = valid && treatmentChecker.VerifyId(sdo.TreatmentId, ref treatment);
                valid = valid && treatmentChecker.IsUnLock(treatment);
                valid = valid && treatmentChecker.IsUnTemporaryLock(treatment);
                valid = valid && treatmentChecker.IsUnpause(treatment);
                valid = valid && treatmentChecker.IsUnLockHein(treatment);
                valid = valid && checker.IsValidPatientType(sdo, ref ptas);
                valid = valid && checker.IsValidInstructionTime(sdo, treatment);
                
                if (valid)
                {
                    Dictionary<HIS_SERVICE_REQ, List<HIS_SERE_SERV>> dicSereServ = null;
                    Dictionary<long, List<HIS_SERVICE_REQ>> dicBedLog = null;
                    this.hisServiceReqProcessor.Run(sdo, treatment, sessionCode, workPlace, ptas, ref dicSereServ, ref dicBedLog);
                    this.hisSereServProcessor.Run(treatment, dicSereServ, workPlace, ptas);

                    //De cuoi cung, vi ko co ham rollback
                    this.hisBedLogProcessor.Run(dicBedLog);

                    this.PassResult(dicSereServ, ref resultData);

                    HisServiceReqLog.Run(resultData.ServiceReqs, resultData.SereServs, LibraryEventLog.EventLog.Enum.HisServiceReq_ChiDinhDichVuTheoLichSuSuDungGiuong);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                this.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void PassResult(Dictionary<HIS_SERVICE_REQ, List<HIS_SERE_SERV>> dicSereServ, ref HisServiceReqListResultSDO resultData)
        {
            if (dicSereServ != null)
            {
                List<HIS_SERVICE_REQ> reqs = dicSereServ.Keys.ToList();
                List<HIS_SERE_SERV> sereServs = new List<HIS_SERE_SERV>();

                foreach (HIS_SERVICE_REQ req in reqs)
                {
                    List<HIS_SERE_SERV> ss = dicSereServ[req];
                    sereServs.AddRange(ss);
                }

                resultData = new HisServiceReqListResultSDO();
                resultData.ServiceReqs = new HisServiceReqGet().GetViewFromTable(reqs);
                resultData.SereServs = new HisSereServGet(param).GetViewFromTable(sereServs);
            }
        }

        internal void RollbackData()
        {
            this.hisSereServProcessor.Rollback();
            this.hisServiceReqProcessor.Rollback();
        }
    }
}
