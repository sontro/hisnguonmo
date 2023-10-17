using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatment;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServ
{
    partial class HisSereServUpdate : BusinessBase
    {
        private List<HIS_SERE_SERV> beforeUpdateHisSereServs = new List<HIS_SERE_SERV>();

        internal HisSereServUpdate()
            : base()
        {

        }

        internal HisSereServUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        /// <summary>
        /// Cap nhat ket qua cua sere_serv (conclude, description)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool UpdateResult(HIS_SERE_SERV data, ref HIS_SERE_SERV resultData)
        {
            bool result = false;
            try
            {
                HIS_SERE_SERV raw = new HisSereServGet().GetById(data.ID);
                Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                //luu lai de phuc vu rollback
                HIS_SERE_SERV beforeUpdateRaw = Mapper.Map<HIS_SERE_SERV>(raw);
                raw.EKIP_ID = data.EKIP_ID;

                if (this.Update(raw, beforeUpdateRaw))
                {
                    resultData = raw;
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

        /// <summary>
        /// Cap nhat ket qua cua sere_serv (conclude, description)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool UpdateResult(HIS_SERE_SERV data)
        {
            HIS_SERE_SERV resultData = new HIS_SERE_SERV();
            return this.UpdateResult(data, ref resultData);
        }

        internal bool UpdateList(List<HIS_SERE_SERV> listData, List<HIS_SERE_SERV> beforeUpdates, bool verifyTreatment)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData) && IsNotNullOrEmpty(beforeUpdates);
                HisSereServCheck checker = new HisSereServCheck(param);
                valid = valid && checker.IsUnLock(beforeUpdates);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                foreach (var data in listData)
                {
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
                        HIS_TREATMENT treatment = null;
                        valid = valid && treatmentChecker.IsUnLock(hisServiceReq.TREATMENT_ID, ref treatment);
                        valid = valid && treatmentChecker.IsUnTemporaryLock(treatment);
                        valid = valid && treatmentChecker.IsUnLockHein(treatment);
                    }

                    if (!data.PARENT_ID.HasValue) //ko thuoc goi nao thi set truong IS_OUT_PARENT_FEE = null
                    {
                        data.IS_OUT_PARENT_FEE = null;
                    }
                }
                if (valid)
                {
                    this.beforeUpdateHisSereServs.AddRange(beforeUpdates);
                    if (!DAOWorker.HisSereServDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServ_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSereServ that bai." + LogUtil.TraceData("listData", listData));
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

        /// <summary>
        /// </summary>
        /// <param name="listRaw"></param>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        internal bool UpdateInvoiceId(List<HIS_SERE_SERV> listRaw, long? invoiceId)
        {
            bool result = false;
            try
            {
                bool valid = true;
                
                valid = IsNotNullOrEmpty(listRaw);
                HisSereServCheck checker = new HisSereServCheck(param);
                valid = valid && checker.IsUnLock(listRaw);
                valid = valid && (!invoiceId.HasValue || (checker.HasNoInvoice(listRaw) && checker.HasBill(listRaw) && checker.HasExecute(listRaw)));
                if (valid)
                {
                    Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                    List<HIS_SERE_SERV> before = Mapper.Map<List<HIS_SERE_SERV>>(listRaw);
                    this.beforeUpdateHisSereServs.AddRange(before);

                    listRaw.ForEach(o => o.INVOICE_ID = invoiceId);
                    if (!DAOWorker.HisSereServDAO.UpdateList(listRaw))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServ_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSereServ that bai." + LogUtil.TraceData("listRaw", listRaw));
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

        internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.beforeUpdateHisSereServs))
            {
                if (!DAOWorker.HisSereServDAO.UpdateList(this.beforeUpdateHisSereServs))
                {
                    LogSystem.Warn("Rollback du lieu HisSereServ that bai, can kiem tra lai." + LogUtil.TraceData("HisSereServs", this.beforeUpdateHisSereServs));
                }
                else
                {
                    this.beforeUpdateHisSereServs = null;
                }
            }
        }

        internal bool Update(HIS_SERE_SERV newData, HIS_SERE_SERV currentData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServCheck checker = new HisSereServCheck(param);
                valid = valid && checker.IsUnLock(newData);
                if (valid)
                {
                    this.beforeUpdateHisSereServs.Add(currentData);//luu lai de phuc vu rollback

                    if (!newData.PARENT_ID.HasValue) //ko thuoc goi nao thi set truong IS_OUT_PARENT_FEE = null
                    {
                        newData.IS_OUT_PARENT_FEE = null;
                    }

                    if (!DAOWorker.HisSereServDAO.Update(newData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServ_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSereServ that bai." + LogUtil.TraceData("newData", newData));
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

        internal bool UpdateRaw(List<HIS_SERE_SERV> listRaw)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServCheck checker = new HisSereServCheck(param);
                foreach (HIS_SERE_SERV raw in listRaw)
                {
                    valid = valid && checker.IsUnLock(raw);
                    valid = valid && checker.HasNoHeinApproval(raw); //da duyet ho so Bao hiem thi ko cho phep sua

                    if (!raw.PARENT_ID.HasValue) //ko thuoc goi nao thi set truong IS_OUT_PARENT_FEE = null
                    {
                        raw.IS_OUT_PARENT_FEE = null;
                    }
                }
                if (valid)
                {
                    if (!DAOWorker.HisSereServDAO.UpdateList(listRaw))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServ_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSereServ that bai." + LogUtil.TraceData("listRaw", listRaw));
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
