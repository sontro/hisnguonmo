using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Auto;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.MANAGER.HisExpMestBltyReq;
using MOS.MANAGER.HisExpMestMatyReq;
using MOS.MANAGER.HisExpMestMetyReq;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Depa
{
    class HisExpMestDepaUpdate : BusinessBase
    {
        private HisExpMestUpdate hisExpMestUpdate;
        private HisExpMestMatyReqMaker hisExpMestMatyReqMaker;
        private HisExpMestMetyReqMaker hisExpMestMetyReqMaker;
        private HisExpMestBltyReqMaker hisExpMestBltyReqMaker;
        private HisExpMestMetyReqUpdate hisExpMestMetyReqUpdate;
        private HisExpMestMatyReqUpdate hisExpMestMatyReqUpdate;
        private HisExpMestBltyReqUpdate hisExpMestBltyReqUpdate;
        private HisExpMestAutoProcess hisExpMestAutoProcess;

        private HisExpMestResultSDO recentResultSDO;

        List<string> sqlDeletes = new List<string>();

        internal HisExpMestDepaUpdate()
            : base()
        {
            this.Init();
        }

        internal HisExpMestDepaUpdate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestBltyReqMaker = new HisExpMestBltyReqMaker(param);
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
            this.hisExpMestMatyReqMaker = new HisExpMestMatyReqMaker(param);
            this.hisExpMestMetyReqMaker = new HisExpMestMetyReqMaker(param);
            this.hisExpMestBltyReqUpdate = new HisExpMestBltyReqUpdate(param);
            this.hisExpMestMatyReqUpdate = new HisExpMestMatyReqUpdate(param);
            this.hisExpMestMetyReqUpdate = new HisExpMestMetyReqUpdate(param);
            this.hisExpMestAutoProcess = new HisExpMestAutoProcess(param);
        }

        internal bool Update(HisExpMestDepaSDO data, ref HisExpMestResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_EXP_MEST expMest = null;
                HisExpMestDepaCheck checker = new HisExpMestDepaCheck(param);
                HisExpMestCheck commonChecker = new HisExpMestCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && commonChecker.VerifyId(data.ExpMestId ?? 0, ref expMest);
                valid = valid && checker.ValidData(data);
                valid = valid && commonChecker.HasNoNationalCode(expMest);
                valid = valid && checker.IsAllowStatusUpdate(expMest);
                valid = valid && checker.IsAllowUpdate(data, expMest);
                valid = valid && commonChecker.HasToExpMestReason(data.ExpMestReasonId);
                if (valid)
                {
                    List<HIS_EXP_MEST_MATY_REQ> expMestMatyReqs = null;
                    List<HIS_EXP_MEST_METY_REQ> expMestMetyReqs = null;
                    List<HIS_EXP_MEST_BLTY_REQ> expMestBltyReqs = null;

                    Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
                    HIS_EXP_MEST before = Mapper.Map<HIS_EXP_MEST>(expMest);
                    expMest.DESCRIPTION = data.Description;
                    expMest.EXP_MEST_REASON_ID = data.ExpMestReasonId;
                    expMest.REMEDY_COUNT = data.RemedyCount;

                    if (!this.hisExpMestUpdate.Update(expMest, before))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    this.ProcessExpMestMatyReq(data, expMest, ref expMestMatyReqs);

                    this.ProcessExpMestMetyReq(data, expMest, ref expMestMetyReqs);

                    this.ProcessExpMestBltyReq(data, expMest, ref expMestBltyReqs);

                    this.ProcessDeleteReq();

                    this.ProcessAuto(expMest);

                    this.PassResult(expMest, expMestMatyReqs, expMestMetyReqs, expMestBltyReqs, ref resultData);

                    new EventLogGenerator(EventLog.Enum.HisExpMest_SuaPhieuXuat).ExpMestCode(expMest.EXP_MEST_CODE).Run();

                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                this.RollBack();
                result = false;
                param.HasException = true;
            }
            return result;
        }

        private void ProcessExpMestMatyReq(HisExpMestDepaSDO data, HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MATY_REQ> expMestMatyReqs)
        {
            List<HIS_EXP_MEST_MATY_REQ> listReq = new List<HIS_EXP_MEST_MATY_REQ>();
            List<HIS_EXP_MEST_MATY_REQ> oldExpMestMatyReqs = new HisExpMestMatyReqGet().GetByExpMestId(data.ExpMestId.Value);
            List<HIS_EXP_MEST_MATY_REQ> newExpMestMatyReqs = null;
            List<HIS_EXP_MEST_MATY_REQ> deleteExpMestMatyReqs = null;
            List<HIS_EXP_MEST_MATY_REQ> updateExpMestMatyReqs = null;
            List<HIS_EXP_MEST_MATY_REQ> beforeUpdates = null;
            if (IsNotNullOrEmpty(data.Materials))
            {
                List<ExpMaterialTypeSDO> expMestMatyReqCreates = data.Materials.Where(o => !o.ExpMestMatyReqId.HasValue).ToList();
                if (IsNotNullOrEmpty(expMestMatyReqCreates))
                {
                    //Tao exp_mest_mety_req
                    if (!this.hisExpMestMatyReqMaker.Run(expMestMatyReqCreates, expMest, ref newExpMestMatyReqs))
                    {
                        throw new Exception("ExpMestMatyReqMaker Rollback du lieu. Ket thuc nghiep vu");
                    }
                    listReq.AddRange(newExpMestMatyReqs);
                }

                List<ExpMaterialTypeSDO> expMestMatyReqUpdates = data.Materials.Where(o => o.ExpMestMatyReqId.HasValue).ToList();
                if (IsNotNullOrEmpty(expMestMatyReqUpdates))
                {
                    updateExpMestMatyReqs = new List<HIS_EXP_MEST_MATY_REQ>();
                    beforeUpdates = new List<HIS_EXP_MEST_MATY_REQ>();
                    Mapper.CreateMap<HIS_EXP_MEST_MATY_REQ, HIS_EXP_MEST_MATY_REQ>();
                    foreach (var item in expMestMatyReqUpdates)
                    {
                        HIS_EXP_MEST_MATY_REQ req = oldExpMestMatyReqs != null ? oldExpMestMatyReqs.FirstOrDefault(o => o.ID == item.ExpMestMatyReqId.Value) : null;
                        if (req == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("ExpMestMatyReqId Invalid: " + item.ExpMestMatyReqId.Value);
                        }
                        if (req.AMOUNT == item.Amount && req.NUM_ORDER == item.NumOrder)
                        {
                            listReq.Add(req);
                        }
                        else
                        {
                            beforeUpdates.Add(req);
                            HIS_EXP_MEST_MATY_REQ reqUpdate = Mapper.Map<HIS_EXP_MEST_MATY_REQ>(req);
                            reqUpdate.AMOUNT = item.Amount;
                            reqUpdate.NUM_ORDER = item.NumOrder;
                            updateExpMestMatyReqs.Add(reqUpdate);
                        }
                    }

                    if (IsNotNullOrEmpty(updateExpMestMatyReqs))
                    {
                        if (!this.hisExpMestMatyReqUpdate.UpdateList(updateExpMestMatyReqs, beforeUpdates))
                        {
                            throw new Exception("Ket thuc nghiep vu. Rollbac du lieu");
                        }
                        listReq.AddRange(updateExpMestMatyReqs);
                    }
                }
            }

            deleteExpMestMatyReqs = oldExpMestMatyReqs != null ? (listReq != null ? oldExpMestMatyReqs.Where(o => !listReq.Exists(e => e.ID == o.ID)).ToList() : null) : null;
            if (IsNotNullOrEmpty(deleteExpMestMatyReqs))
            {
                bool valid = true;
                HisExpMestMatyReqCheck reqChecker = new HisExpMestMatyReqCheck(param);
                foreach (var delete in deleteExpMestMatyReqs)
                {
                    valid = valid && reqChecker.IsUnLock(delete);
                }
                if (!valid)
                {
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }
                string deleteReq = DAOWorker.SqlDAO.AddInClause(deleteExpMestMatyReqs.Select(s => s.ID).ToList(), "DELETE HIS_EXP_MEST_MATY_REQ WHERE %IN_CLAUSE% ", "ID");
                this.sqlDeletes.Add(deleteReq);
                if (IsNotNullOrEmpty(listReq))
                {
                    expMestMatyReqs = listReq;
                }
            }
        }

        private void ProcessExpMestMetyReq(HisExpMestDepaSDO data, HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_METY_REQ> expMestMetyReqs)
        {
            List<HIS_EXP_MEST_METY_REQ> listReq = new List<HIS_EXP_MEST_METY_REQ>();
            List<HIS_EXP_MEST_METY_REQ> oldExpMestMetyReqs = new HisExpMestMetyReqGet().GetByExpMestId(data.ExpMestId.Value);
            List<HIS_EXP_MEST_METY_REQ> newExpMestMetyReqs = null;
            List<HIS_EXP_MEST_METY_REQ> deleteExpMestMetyReqs = null;
            List<HIS_EXP_MEST_METY_REQ> updateExpMestMetyReqs = null;
            List<HIS_EXP_MEST_METY_REQ> beforeUpdates = null;
            if (IsNotNullOrEmpty(data.Medicines))
            {
                List<ExpMedicineTypeSDO> expMestMetyReqCreates = data.Medicines.Where(o => !o.ExpMestMetyReqId.HasValue).ToList();
                if (IsNotNullOrEmpty(expMestMetyReqCreates))
                {
                    //Tao exp_mest_mety_req
                    if (!this.hisExpMestMetyReqMaker.Run(expMestMetyReqCreates, expMest, ref newExpMestMetyReqs))
                    {
                        throw new Exception("ExpMestMetyReqMaker Rollback du lieu. Ket thuc nghiep vu");
                    }
                    listReq.AddRange(newExpMestMetyReqs);
                }

                List<ExpMedicineTypeSDO> expMestMetyReqUpdates = data.Medicines.Where(o => o.ExpMestMetyReqId.HasValue).ToList();
                if (IsNotNullOrEmpty(expMestMetyReqUpdates))
                {
                    updateExpMestMetyReqs = new List<HIS_EXP_MEST_METY_REQ>();
                    beforeUpdates = new List<HIS_EXP_MEST_METY_REQ>();
                    Mapper.CreateMap<HIS_EXP_MEST_METY_REQ, HIS_EXP_MEST_METY_REQ>();
                    foreach (var item in expMestMetyReqUpdates)
                    {
                        HIS_EXP_MEST_METY_REQ req = oldExpMestMetyReqs != null ? oldExpMestMetyReqs.FirstOrDefault(o => o.ID == item.ExpMestMetyReqId.Value) : null;
                        if (req == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("ExpMestMetyReqId Invalid: " + item.ExpMestMetyReqId.Value);
                        }
                        if (req.AMOUNT == item.Amount && req.NUM_ORDER == item.NumOrder)
                        {
                            listReq.Add(req);
                        }
                        else
                        {
                            beforeUpdates.Add(req);
                            HIS_EXP_MEST_METY_REQ reqUpdate = Mapper.Map<HIS_EXP_MEST_METY_REQ>(req);
                            reqUpdate.AMOUNT = item.Amount;
                            reqUpdate.NUM_ORDER = item.NumOrder;
                            updateExpMestMetyReqs.Add(reqUpdate);
                        }
                    }

                    if (IsNotNullOrEmpty(updateExpMestMetyReqs))
                    {
                        if (!this.hisExpMestMetyReqUpdate.UpdateList(updateExpMestMetyReqs, beforeUpdates))
                        {
                            throw new Exception("Ket thuc nghiep vu. Rollbac du lieu");
                        }
                        listReq.AddRange(updateExpMestMetyReqs);
                    }
                }
            }

            deleteExpMestMetyReqs = oldExpMestMetyReqs != null ? (listReq != null ? oldExpMestMetyReqs.Where(o => !listReq.Exists(e => e.ID == o.ID)).ToList() : null) : null;
            if (IsNotNullOrEmpty(deleteExpMestMetyReqs))
            {
                bool valid = true;
                HisExpMestMetyReqCheck reqChecker = new HisExpMestMetyReqCheck(param);
                foreach (var delete in deleteExpMestMetyReqs)
                {
                    valid = valid && reqChecker.IsUnLock(delete);
                }
                if (!valid)
                {
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }
                string deleteReq = DAOWorker.SqlDAO.AddInClause(deleteExpMestMetyReqs.Select(s => s.ID).ToList(), "DELETE HIS_EXP_MEST_METY_REQ WHERE %IN_CLAUSE% ", "ID");
                this.sqlDeletes.Add(deleteReq);
            }
            if (IsNotNullOrEmpty(listReq))
            {
                expMestMetyReqs = listReq;
            }
        }

        private void ProcessExpMestBltyReq(HisExpMestDepaSDO data, HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_BLTY_REQ> expMestBltyReqs)
        {
            List<HIS_EXP_MEST_BLTY_REQ> listReq = new List<HIS_EXP_MEST_BLTY_REQ>();
            List<HIS_EXP_MEST_BLTY_REQ> oldExpMestBltyReqs = new HisExpMestBltyReqGet().GetByExpMestId(data.ExpMestId.Value);
            List<HIS_EXP_MEST_BLTY_REQ> newExpMestBltyReqs = null;
            List<HIS_EXP_MEST_BLTY_REQ> deleteExpMestBltyReqs = null;
            List<HIS_EXP_MEST_BLTY_REQ> updateExpMestBltyReqs = null;
            List<HIS_EXP_MEST_BLTY_REQ> beforeUpdates = null;
            if (IsNotNullOrEmpty(data.Bloods))
            {
                List<ExpBloodTypeSDO> expMestBltyReqCreates = data.Bloods.Where(o => !o.ExpMestBltyReqId.HasValue).ToList();
                if (IsNotNullOrEmpty(expMestBltyReqCreates))
                {
                    //Tao exp_mest_mety_req
                    if (!this.hisExpMestBltyReqMaker.Run(expMestBltyReqCreates, expMest, ref newExpMestBltyReqs))
                    {
                        throw new Exception("ExpMestBltyReqMaker Rollback du lieu. Ket thuc nghiep vu");
                    }
                    listReq.AddRange(newExpMestBltyReqs);
                }

                List<ExpBloodTypeSDO> expMestBltyReqUpdates = data.Bloods.Where(o => o.ExpMestBltyReqId.HasValue).ToList();
                if (IsNotNullOrEmpty(expMestBltyReqUpdates))
                {
                    updateExpMestBltyReqs = new List<HIS_EXP_MEST_BLTY_REQ>();
                    beforeUpdates = new List<HIS_EXP_MEST_BLTY_REQ>();
                    Mapper.CreateMap<HIS_EXP_MEST_BLTY_REQ, HIS_EXP_MEST_BLTY_REQ>();
                    foreach (var item in expMestBltyReqUpdates)
                    {
                        HIS_EXP_MEST_BLTY_REQ req = oldExpMestBltyReqs != null ? oldExpMestBltyReqs.FirstOrDefault(o => o.ID == item.ExpMestBltyReqId.Value) : null;
                        if (req == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("ExpMestBltyReqId Invalid: " + item.ExpMestBltyReqId.Value);
                        }
                        if (req.AMOUNT == item.Amount && req.NUM_ORDER == item.NumOrder)
                        {
                            listReq.Add(req);
                        }
                        else
                        {
                            beforeUpdates.Add(req);
                            HIS_EXP_MEST_BLTY_REQ reqUpdate = Mapper.Map<HIS_EXP_MEST_BLTY_REQ>(req);
                            reqUpdate.AMOUNT = item.Amount;
                            reqUpdate.NUM_ORDER = item.NumOrder;
                            updateExpMestBltyReqs.Add(reqUpdate);
                        }
                    }

                    if (IsNotNullOrEmpty(updateExpMestBltyReqs))
                    {
                        if (!this.hisExpMestBltyReqUpdate.UpdateList(updateExpMestBltyReqs, beforeUpdates))
                        {
                            throw new Exception("Ket thuc nghiep vu. Rollbac du lieu");
                        }
                        listReq.AddRange(updateExpMestBltyReqs);
                    }
                }
            }

            deleteExpMestBltyReqs = oldExpMestBltyReqs != null ? (listReq != null ? oldExpMestBltyReqs.Where(o => !listReq.Exists(e => e.ID == o.ID)).ToList() : null) : null;
            if (IsNotNullOrEmpty(deleteExpMestBltyReqs))
            {
                bool valid = true;
                HisExpMestBltyReqCheck reqChecker = new HisExpMestBltyReqCheck(param);
                foreach (var delete in deleteExpMestBltyReqs)
                {
                    valid = valid && reqChecker.IsUnLock(delete);
                }
                if (!valid)
                {
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }
                string deleteReq = DAOWorker.SqlDAO.AddInClause(deleteExpMestBltyReqs.Select(s => s.ID).ToList(), "DELETE HIS_EXP_MEST_BLTY_REQ WHERE %IN_CLAUSE% ", "ID");
                this.sqlDeletes.Add(deleteReq);
            }
            if (IsNotNullOrEmpty(listReq))
            {
                expMestBltyReqs = listReq;
            }
        }

        private void ProcessDeleteReq()
        {
            if (IsNotNullOrEmpty(this.sqlDeletes))
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in this.sqlDeletes)
                {
                    sb.Append(item).Append(";");
                }
                string sql = String.Format("BEGIN {0} END;", sb.ToString());
                if (!DAOWorker.SqlDAO.Execute(sql))
                {
                    throw new Exception("Xoa Req that bai. Rollback du lieu. Sql: " + sql);
                }
            }
        }

        /// <summary>
        /// Tu dong phe duyet, thuc xuat trong truong hop kho co cau hinh tu dong
        /// </summary>
        private void ProcessAuto(HIS_EXP_MEST expMest)
        {
            try
            {
                this.hisExpMestAutoProcess.Run(expMest, ref this.recentResultSDO);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void PassResult(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATY_REQ> expMatyReqs, List<HIS_EXP_MEST_METY_REQ> expMetyReqs, List<HIS_EXP_MEST_BLTY_REQ> expBltyReqs, ref HisExpMestResultSDO resultData)
        {
            if (this.recentResultSDO!=null)
            {
                resultData = this.recentResultSDO;
            }
            else
            {
                resultData = new HisExpMestResultSDO();
                resultData.ExpMest = expMest;
                resultData.ExpMatyReqs = expMatyReqs;
                resultData.ExpMetyReqs = expMetyReqs;
                resultData.ExpBltyReqs = expBltyReqs;
            }
        }

        private void RollBack()
        {
            this.hisExpMestMatyReqMaker.Rollback();
            this.hisExpMestMetyReqMaker.Rollback();
            this.hisExpMestBltyReqMaker.Rollback();
            this.hisExpMestBltyReqUpdate.RollbackData();
            this.hisExpMestMatyReqUpdate.RollbackData();
            this.hisExpMestMetyReqUpdate.RollbackData();
            this.hisExpMestUpdate.RollbackData();
        }
    }
}
