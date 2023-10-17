using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MOS.UTILITY;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceRoom;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisTreatment;

namespace MOS.MANAGER.HisServiceReq.Ration
{
    partial class HisServiceReqRationCreate : BusinessBase
    {
        private HisServiceReqProcessor hisServiceReqProcessor;
        private HisSereServRationProcessor hisSereServRationProcessor;
        private HisTreatmentUpdate hisTreatmentUpdate;

        internal HisServiceReqRationCreate()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqRationCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisServiceReqProcessor = new HisServiceReqProcessor(param);
            this.hisSereServRationProcessor = new HisSereServRationProcessor(param);
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
        }

        internal bool Run(HisRationServiceReqSDO data, bool checkWorkPlaceInfo, ref HisServiceReqListResultSDO resultData)
        {
            bool result = false;
            try
            {
                List<HIS_TREATMENT> treatments = new List<HIS_TREATMENT>();
                string sessionCode = Guid.NewGuid().ToString();
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HisServiceReqRationCheck checker = new HisServiceReqRationCheck(param);
                List<V_HIS_SERVICE_PATY> servicePaties = null;
                List<RationRequest> rationRequests = null;
                Preparer preparer = new Preparer();
                bool valid = true;
                //WorkPlaceSDO workPlace = null;

                long reqRoomId = 0;
                long reqDepartmentId = 0;
                long reqBranchId = 0;

                if (checkWorkPlaceInfo)
                {
                    WorkPlaceSDO workPlace = null;
                    valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                    if (valid)
                    {
                        reqRoomId = workPlace.RoomId;
                        reqDepartmentId = workPlace.DepartmentId;
                        reqBranchId = workPlace.BranchId;
                    }
                }
                else
                {
                    V_HIS_ROOM room = HisRoomCFG.DATA.Where(o => o.ID == data.RequestRoomId).FirstOrDefault();
                    if (room == null)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("request_room_id ko hop le.");
                        valid = false;
                    }
                    reqRoomId = room.ID;
                    reqDepartmentId = room.DEPARTMENT_ID;
                    reqBranchId = room.BRANCH_ID;
                }

                valid = valid && checker.IsValidData(data);
                valid = valid && preparer.PrepareData(data, ref rationRequests);
                valid = valid && checker.IsNotClosing(rationRequests);
                valid = valid && treatmentChecker.VerifyIds(data.TreatmentIds, treatments);
                valid = valid && treatmentChecker.IsUnLock(treatments);
                valid = valid && treatmentChecker.IsUnTemporaryLock(treatments);
                valid = valid && treatmentChecker.IsUnpause(treatments);
                valid = valid && treatmentChecker.IsUnLockHein(treatments);
                valid = valid && checker.IsProcessable(data.RationServices);
                valid = valid && checker.IsValidServicePaty(data, reqRoomId, reqDepartmentId, treatments, ref servicePaties);

                if (valid)
                {
                    List<HIS_SERVICE_REQ> serviceReqs = null;
                    List<HIS_SERE_SERV_RATION> sereServRations = null;
                    Dictionary<HIS_SERVICE_REQ, List<RationRequest>> SR_RATIONREQ_MAP = new Dictionary<HIS_SERVICE_REQ, List<RationRequest>>();
                    if (!this.hisServiceReqProcessor.Run(data, treatments, reqDepartmentId, rationRequests, SR_RATIONREQ_MAP, ref serviceReqs)
                        || !this.hisSereServRationProcessor.Run(treatments, serviceReqs, rationRequests, SR_RATIONREQ_MAP, ref sereServRations)
                        || !this.ProcessTreatment(data.IsForAutoCreateRation, treatments)
                        )
                    {
                        throw new Exception("Rollback data");
                    }
                    this.PassResult(serviceReqs, sereServRations, ref resultData);
                    result = true;
                    this.ProcessEventLog(serviceReqs, sereServRations, data.HalfInFirstDay);
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

        private void ProcessEventLog(List<HIS_SERVICE_REQ> serviceReqs, List<HIS_SERE_SERV_RATION> sereServRations, bool halfInFirstDay)
        {
            if (IsNotNullOrEmpty(serviceReqs) && IsNotNullOrEmpty(sereServRations))
            {
                Thread threadUpdate = new Thread(new ParameterizedThreadStart(HisServiceReqRationUtil.RationLog));
                try
                {
                    RationThreadData threadData = new RationThreadData();
                    threadData.ServiceReqs = serviceReqs;
                    threadData.SereServRations = sereServRations;
                    threadData.IsHalfInFirstDay = halfInFirstDay;
                    threadUpdate.Priority = ThreadPriority.Normal;
                    threadUpdate.Start(threadData);
                }
                catch (Exception ex)
                {
                    threadUpdate.Abort();
                    LogSystem.Error(ex);
                }
            }
        }
        

        private bool ProcessTreatment(bool isForAutoCreateRation, List<HIS_TREATMENT> treatments)
        {
            bool result = true;
            try
            {
                if (isForAutoCreateRation && IsNotNullOrEmpty(treatments))
                {
                    Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                    List<HIS_TREATMENT> oldTreatments = Mapper.Map<List<HIS_TREATMENT>>(treatments);

                    treatments.ForEach(o => o.HAS_AUTO_CREATE_RATION = Constant.IS_TRUE);
                    if (!this.hisTreatmentUpdate.UpdateList(treatments, oldTreatments))
                    {
                        result = false;
                        LogSystem.Error("Cap nhat ho so khi chi dinh suat an that bai");
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
        }


        private void PassResult(List<HIS_SERVICE_REQ> serviceReqs, List<HIS_SERE_SERV_RATION> sereServRations, ref HisServiceReqListResultSDO resultData)
        {
            resultData = new HisServiceReqListResultSDO();

            resultData.ServiceReqs = new HisServiceReqGet().GetViewFromTable(serviceReqs);
            resultData.SereServRations = sereServRations;
        }

        /// <summary>
        /// Rollback du lieu
        /// </summary>
        internal void RollbackData()
        {
            this.hisTreatmentUpdate.RollbackData();
            this.hisServiceReqProcessor.Rollback();
            this.hisSereServRationProcessor.Rollback();
        }
    }
}
