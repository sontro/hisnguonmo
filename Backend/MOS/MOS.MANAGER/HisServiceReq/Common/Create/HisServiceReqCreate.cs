using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisServiceReq.Common;
using MOS.MANAGER.HisServiceReq.Test;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceReq
{
    class HisServiceReqCreate : BusinessBase
    {
        private List<HIS_SERVICE_REQ> recentHisServiceReqs = new List<HIS_SERVICE_REQ>();

        internal HisServiceReqCreate()
            : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramCreate"></param>
        internal HisServiceReqCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
        }

        /// <summary>
        /// Insert du lieu EvHisServiceReq
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool Create(HIS_SERVICE_REQ data, HIS_TREATMENT rawTreatment)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceReqUtil.SetTdl(data, rawTreatment); //Luu du thua du lieu
                HisServiceReqTestUtil.AddTestData(data);

                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);

                WorkPlaceSDO workPlace = TokenManager.GetWorkPlace(data.REQUEST_ROOM_ID);

                if (workPlace == null)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.KhongCoThongTinPhongLamViec);
                    throw new Exception("Khong co thong tin workplace.");
                }
                data.REQUEST_ROOM_ID = workPlace.RoomId;
                data.REQUEST_DEPARTMENT_ID = workPlace.DepartmentId;

                if (string.IsNullOrWhiteSpace(data.REQUEST_LOGINNAME))
                {
                    data.REQUEST_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    data.REQUEST_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    data.REQUEST_USER_TITLE = HisEmployeeUtil.GetTitle(data.REQUEST_LOGINNAME);
                }

                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsAllowAssignService(data);
                valid = valid && checker.IsValidInstructionTime(data.INTRUCTION_TIME, rawTreatment);
                valid = valid && treatmentChecker.IsUnLock(rawTreatment);
                valid = valid && treatmentChecker.IsUnTemporaryLock(rawTreatment);
                valid = valid && treatmentChecker.IsUnpause(rawTreatment);
                valid = valid && treatmentChecker.IsUnLockHein(rawTreatment);

                if (valid)
                {
                    data.PRIORITY = data.PRIORITY.HasValue ? data.PRIORITY.Value : 0;//Neu null thi xet =0 de phuc vu viec order tren cac man hinh xu ly
                    data.REQ_WORKING_SHIFT_ID = TokenManager.GetWorkingShift();

                    HisServiceReqNumOrderBase.SetNumOrderBase(data);

                    if (!DAOWorker.HisServiceReqDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceReq_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisServiceReq that bai." + LogUtil.TraceData("HisServiceReq", data));
                    }
                    //cap nhat lai counter
                    HisRoomCounterCFG.AddCount(new List<HIS_SERVICE_REQ>() { data });
                    this.recentHisServiceReqs.Add(data);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
                Logging("Co exception khi tao HisServiceReq. Buoc tao yeu cau dich vu chi tiet tiep theo se khong thuc hien duoc.", LogType.Error);
            }
            return result;
        }

        internal bool CreateList(List<HIS_SERVICE_REQ> listData, HIS_TREATMENT treatment)
        {
            return this.CreateList(listData, treatment, true);
        }

        internal bool CreateList(List<HIS_SERVICE_REQ> listData, HIS_TREATMENT rawTreatment, bool setWorkPlaceInfo)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);

                foreach (var data in listData)
                {
                    HisServiceReqUtil.SetTdl(data, rawTreatment);
                    HisServiceReqTestUtil.AddTestData(data);
                    data.REQ_WORKING_SHIFT_ID = TokenManager.GetWorkingShift();

                    if (setWorkPlaceInfo)
                    {
                        WorkPlaceSDO workPlace = null;
                        if (this.HasWorkPlaceInfo(data.REQUEST_ROOM_ID, ref workPlace))
                        {
                            data.REQUEST_ROOM_ID = workPlace.RoomId;
                            data.REQUEST_DEPARTMENT_ID = workPlace.DepartmentId;
                        }
                    }

                    if (string.IsNullOrWhiteSpace(data.REQUEST_LOGINNAME))
                    {
                        data.REQUEST_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        data.REQUEST_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                        data.REQUEST_USER_TITLE = HisEmployeeUtil.GetTitle(data.REQUEST_LOGINNAME);
                    }

                    data.PRIORITY = data.PRIORITY.HasValue ? data.PRIORITY.Value : 0;//Neu null thi xet =0 de phuc vu viec order tren cac man hinh xu ly

                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.IsAllowAssignService(data);
                    valid = valid && treatmentChecker.IsUnLock(rawTreatment);
                    valid = valid && treatmentChecker.IsUnTemporaryLock(rawTreatment);
                    valid = valid && treatmentChecker.IsUnpause(rawTreatment);
                    valid = valid && treatmentChecker.IsUnLockHein(rawTreatment);
                }
                if (valid)
                {
                    HisServiceReqNumOrderBase.SetNumOrderBase(listData);

                    if (!DAOWorker.HisServiceReqDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceReq_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisServiceReq that bai." + LogUtil.TraceData("HisServiceReq", listData));
                    }

                    //cap nhat lai counter
                    HisRoomCounterCFG.AddCount(listData);

                    this.recentHisServiceReqs.AddRange(listData);
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

        internal bool CreateList(List<HIS_SERVICE_REQ> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                foreach (var data in listData)
                {

                    WorkPlaceSDO workPlace = null;
                    if (this.HasWorkPlaceInfo(data.REQUEST_ROOM_ID, ref workPlace))
                    {
                        data.REQUEST_ROOM_ID = workPlace.RoomId;
                        data.REQUEST_DEPARTMENT_ID = workPlace.DepartmentId;
                    }
                    if (string.IsNullOrWhiteSpace(data.REQUEST_LOGINNAME))
                    {
                        data.REQUEST_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        data.REQUEST_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                        data.REQUEST_USER_TITLE = HisEmployeeUtil.GetTitle(data.REQUEST_LOGINNAME);
                    }

                    data.PRIORITY = data.PRIORITY.HasValue ? data.PRIORITY.Value : 0;//Neu null thi xet =0 de phuc vu viec order tren cac man hinh xu ly

                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.IsAllowAssignService(data);
                    data.REQ_WORKING_SHIFT_ID = TokenManager.GetWorkingShift();
                }
                if (valid)
                {
                    HisServiceReqNumOrderBase.SetNumOrderBase(listData);

                    if (!DAOWorker.HisServiceReqDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceReq_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisServiceReq that bai." + LogUtil.TraceData("HisServiceReq", listData));
                    }

                    //cap nhat lai counter
                    HisRoomCounterCFG.AddCount(listData);

                    this.recentHisServiceReqs.AddRange(listData);
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

        internal bool CreateListWithoutCheckAnyThings(List<HIS_SERVICE_REQ> listData)
        {
            bool result = false;
            try
            {

                if (IsNotNullOrEmpty(listData) && !DAOWorker.HisServiceReqDAO.CreateList(listData))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceReq_ThemMoiThatBai);
                    throw new Exception("Them moi thong tin HisServiceReq that bai." + LogUtil.TraceData("HisServiceReq", listData));
                }

                this.recentHisServiceReqs.AddRange(listData);
                result = true;

            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Rollback du lieu yeu cau dich vu:
        /// </summary>
        /// <param name="hisServiceReq"></param>
        /// <returns></returns>
        internal void RollbackData()
        {
            //Rollback du lieu HisServiceReq
            if (IsNotNullOrEmpty(this.recentHisServiceReqs))
            {
                if (!DAOWorker.HisServiceReqDAO.TruncateList(this.recentHisServiceReqs))
                {
                    LogSystem.Warn("Rollback thong tin HisServiceReq that bai. Can kiem tra lai log.");
                }
                this.recentHisServiceReqs = null;
            }
        }
    }
}
