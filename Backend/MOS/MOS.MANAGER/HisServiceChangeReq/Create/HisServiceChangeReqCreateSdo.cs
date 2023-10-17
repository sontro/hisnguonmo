using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Common.ObjectChecker;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceChangeReq.Create
{
    /// <summary>
    /// Xu ly nghiep vu tao yeu cau doi dich vu
    /// </summary>
    class HisServiceChangeReqCreateSdo : BusinessBase
    {
        internal HisServiceChangeReqCreateSdo()
            : base()
        {
            this.Init();
        }

        internal HisServiceChangeReqCreateSdo(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
        }

        internal bool Run(HisServiceChangeReqSDO data, ref HIS_SERVICE_CHANGE_REQ resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                
                if (valid)
                {
                    HIS_SERVICE_REQ serviceReq = null;
                    HIS_SERE_SERV sereServ = null;
                    V_HIS_SERVICE alterService = null;
                    WorkPlaceSDO workPlaceSdo = null;

                    HisServiceChangeReqCreateSdoCheck checker = new HisServiceChangeReqCreateSdoCheck(param);
                    HisSereServCheck sereServChecker = new HisSereServCheck(param);
                    HisServiceReqCheck serviceReqChecker = new HisServiceReqCheck(param);

                    List<HIS_SERVICE_CHANGE_REQ> existsServiceChangeReqs = null;

                    valid = valid && checker.IsValidData(data, ref serviceReq, ref sereServ, ref alterService);
                    valid = valid && sereServChecker.HasExecute(sereServ);
                    valid = valid && sereServChecker.HasNoBill(sereServ);
                    valid = valid && serviceReqChecker.IsNotStarted(serviceReq);
                    valid = valid && serviceReqChecker.HasExecute(serviceReq);
                    valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlaceSdo);
                    valid = valid && checker.IsWorkingAtRoom(serviceReq.EXECUTE_ROOM_ID, data.WorkingRoomId);
                    valid = valid && checker.HasNoApprovedChangeReq(sereServ, ref existsServiceChangeReqs);

                    if (valid)
                    {
                        //Xoa yeu cau doi dich vu cu
                        if (IsNotNullOrEmpty(existsServiceChangeReqs)
                            && !DAOWorker.HisServiceChangeReqDAO.TruncateList(existsServiceChangeReqs))
                        {
                            LogSystem.Warn("Xoa y/c doi dich vu cu that bai");
                            return false;
                        }

                        HIS_SERVICE_CHANGE_REQ toInsert = new HIS_SERVICE_CHANGE_REQ();
                        toInsert.ALTER_SERVICE_ID = data.AlterServiceId;
                        toInsert.PATIENT_TYPE_ID = data.PatientTypeId;
                        toInsert.PRIMARY_PATIENT_TYPE_ID = data.PrimaryPatientTypeId;
                        toInsert.REQUEST_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        toInsert.REQUEST_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                        toInsert.SERE_SERV_ID = data.SereServId;
                        toInsert.TDL_SERVICE_REQ_ID = sereServ.SERVICE_REQ_ID.Value;

                        if (DAOWorker.HisServiceChangeReqDAO.Create(toInsert))
                        {
                            result = true;
                            resultData = toInsert;
                        }

                        if (result)
                        {
                            new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisServiceChangeReq_YeuCauDoiDichVu, sereServ.TDL_SERVICE_NAME, alterService.SERVICE_NAME).TreatmentCode(serviceReq.TDL_TREATMENT_CODE).ServiceReqCode(serviceReq.SERVICE_REQ_CODE).Run();
                        }
                    }

                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
