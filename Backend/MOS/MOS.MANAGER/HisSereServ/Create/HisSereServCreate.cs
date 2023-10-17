using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatment;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServ
{
    class HisSereServCreate : BusinessBase
    {
        private List<HIS_SERE_SERV> recentHisSereServs = new List<HIS_SERE_SERV>();

        internal HisSereServCreate()
            : base()
        {

        }

        internal HisSereServCreate(Inventec.Core.CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool CreateList(List<HIS_SERE_SERV> listData, HIS_SERVICE_REQ serviceReq, bool verifyTreatment)
        {
            return this.CreateList(listData, new List<HIS_SERVICE_REQ>() { serviceReq }, verifyTreatment);
        }

        internal bool CreateList(List<HIS_SERE_SERV> listData, List<HIS_SERVICE_REQ> serviceReqs, bool verifyTreatment)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServCheck checker = new HisSereServCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                foreach (var data in listData)
                {
                    HIS_SERVICE_REQ serviceReq = serviceReqs.Where(o => o.ID == data.SERVICE_REQ_ID).FirstOrDefault();
                    HisSereServUtil.SetTdl(data, serviceReq);
                    valid = valid && checker.VerifyRequireField(data);

                    //check Treatment neu co y/c
                    //(de tranh truong hop check nhieu lan (check khi xu ly treatment, service_req, lan sere_serv...)) ==> tang hieu nang
                    if (verifyTreatment)
                    {
                        //chi cho phep cap nhat khi treatment ko bi khoa
                        HIS_SERVICE_REQ hisServiceReq = new HisServiceReqGet().GetById(data.SERVICE_REQ_ID.Value);
                        if (hisServiceReq == null)
                        {
                            MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("SERVICE_REQ_ID khong ton tai trong DB" + LogUtil.TraceData("data", data));
                        }
                        HIS_TREATMENT hisTreatment = null;
                        //chi cho phep them moi khi treatment ko bi khoa va ko bi tam dung
                        valid = valid && treatmentChecker.IsUnLock(serviceReq.TREATMENT_ID, ref hisTreatment);
                        valid = valid && treatmentChecker.IsUnTemporaryLock(hisTreatment);
                        valid = valid && treatmentChecker.IsUnpause(hisTreatment);
                        valid = valid && treatmentChecker.IsUnLockHein(hisTreatment);
                    }

                    if (!data.PARENT_ID.HasValue) //ko thuoc goi nao thi set truong IS_OUT_PARENT_FEE = null
                    {
                        data.IS_OUT_PARENT_FEE = null;
                    }
                }
                if (valid)
                {
                    if (!DAOWorker.HisSereServDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServ_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSereServ that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisSereServs.AddRange(listData);
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

        internal bool Create(HIS_SERE_SERV data, HIS_SERVICE_REQ serviceReq, bool verifyTreatment)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServUtil.SetTdl(data, serviceReq);
                HisSereServCheck checker = new HisSereServCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                valid = IsNotNull(data);
                valid = valid && checker.VerifyRequireField(data);

                //check Treatment neu co y/c
                //(de tranh truong hop check nhieu lan (check khi xu ly treatment, service_req, lan sere_serv...)) ==> tang hieu nang
                if (verifyTreatment)
                {
                    //chi cho phep cap nhat khi treatment ko bi khoa
                    HIS_SERVICE_REQ hisServiceReq = new HisServiceReqGet().GetById(data.SERVICE_REQ_ID.Value);
                    if (hisServiceReq == null)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("SERVICE_REQ_ID khong ton tai trong DB" + LogUtil.TraceData("data", data));
                    }
                    HIS_TREATMENT hisTreatment = null;
                    //chi cho phep them moi khi treatment ko bi khoa va ko bi tam dung
                    valid = valid && treatmentChecker.IsUnLock(serviceReq.TREATMENT_ID, ref hisTreatment);
                    valid = valid && treatmentChecker.IsUnTemporaryLock(hisTreatment);
                    valid = valid && treatmentChecker.IsUnpause(hisTreatment);
                    valid = valid && treatmentChecker.IsUnLockHein(hisTreatment);
                }

                if (!data.PARENT_ID.HasValue) //ko thuoc goi nao thi set truong IS_OUT_PARENT_FEE = null
                {
                    data.IS_OUT_PARENT_FEE = null;
                }
                if (valid)
                {
                    if (!DAOWorker.HisSereServDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServ_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSereServ that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisSereServs.Add(data);
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

        internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.recentHisSereServs))
            {
                if (!DAOWorker.HisSereServDAO.DeleteList(this.recentHisSereServs))
                {
                    LogSystem.Warn("Rollback (delete chu ko phai truncate) du lieu HisSereServ that bai, can kiem tra lai." + LogUtil.TraceData("HisSereServs", this.recentHisSereServs));
                }
            }
        }
    }
}
