using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Auto;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.MANAGER.HisExpMestBltyReq;
using MOS.MANAGER.HisExpMestMatyReq;
using MOS.MANAGER.HisExpMestMetyReq;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Chms.UpdateChms
{
    class HisExpMestChmsUpdate : BusinessBase
    {
        private HisExpMestUpdate hisExpMestUpdate;
        private MaterialProcessor materialProcessor;
        private MedicineProcessor medicineProcessor;
        private HisExpMestBltyReqMaker hisExpMestBltyReqMaker;
        private HisExpMestBltyReqUpdate hisExpMestBltyReqUpdate;
        private HisExpMestAutoProcess hisExpMestAutoProcess;

        private HisExpMestResultSDO recentResultSDO;

        internal HisExpMestChmsUpdate()
            : base()
        {
            this.Init();
        }

        internal HisExpMestChmsUpdate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestBltyReqMaker = new HisExpMestBltyReqMaker(param);
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
            this.materialProcessor = new MaterialProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.hisExpMestBltyReqUpdate = new HisExpMestBltyReqUpdate(param);
            this.hisExpMestAutoProcess = new HisExpMestAutoProcess(param);
        }

        internal bool Update(HisExpMestChmsSDO data, ref HisExpMestResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_EXP_MEST expMest = null;
                HisExpMestChmsCheck checker = new HisExpMestChmsCheck(param);
                HisExpMestCheck commonChecker = new HisExpMestCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && commonChecker.VerifyId(data.ExpMestId ?? 0, ref expMest);
                valid = valid && checker.ValidData(data);
                valid = valid && checker.IsAllowStatusUpdate(expMest);
                valid = valid && checker.IsAllowUpdate(data, expMest);
                valid = valid && commonChecker.HasToExpMestReason(data.ExpMestReasonId);
                if (valid)
                {
                    List<HIS_EXP_MEST_MATY_REQ> expMestMatyReqs = null;
                    List<HIS_EXP_MEST_METY_REQ> expMestMetyReqs = null;
                    List<HIS_EXP_MEST_BLTY_REQ> expMestBltyReqs = null;
                    List<HIS_EXP_MEST_MEDICINE> expMestMedicines = null;
                    List<HIS_EXP_MEST_MATERIAL> expMestMaterials = null;

                    List<string> sqls = new List<string>();

                    Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
                    HIS_EXP_MEST before = Mapper.Map<HIS_EXP_MEST>(expMest);
                    expMest.DESCRIPTION = data.Description;
                    expMest.RECIPIENT = data.Recipient;
                    expMest.RECEIVING_PLACE = data.ReceivingPlace;
                    expMest.EXP_MEST_REASON_ID = data.ExpMestReasonId;

                    if (!this.hisExpMestUpdate.Update(expMest, before))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    if (!this.materialProcessor.Run(data, expMest, ref expMestMatyReqs, ref expMestMaterials, ref sqls))
                    {
                        throw new Exception("materialProcessor. Ket thuc nghiep vu");
                    }
                    if (!this.medicineProcessor.Run(data, expMest, ref expMestMetyReqs, ref expMestMedicines, ref sqls))
                    {
                        throw new Exception("medicineProcessor. Ket thuc nghiep vu");
                    }
                    this.ProcessExpMestBltyReq(data, expMest, ref expMestBltyReqs, ref sqls);

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("sqls: " + sqls.ToString());
                    }

                    this.PassResult(expMest, expMestMatyReqs, expMestMetyReqs, expMestBltyReqs, expMestMedicines, expMestMaterials, ref resultData);

                    new EventLogGenerator(EventLog.Enum.HisExpMest_SuaPhieuXuat).ExpMestCode(expMest.EXP_MEST_CODE).Run();

                    if (this.IsProcessAuto(data))
                    {
                        this.ProcessAuto(expMest);
                    }

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

        private void ProcessExpMestBltyReq(HisExpMestChmsSDO data, HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_BLTY_REQ> expMestBltyReqs, ref List<string> sqls)
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
                sqls.Add(deleteReq);
            }
            if (IsNotNullOrEmpty(listReq))
            {
                expMestBltyReqs = listReq;
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

        private void PassResult(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MATY_REQ> expMatyReqs, List<HIS_EXP_MEST_METY_REQ> expMetyReqs, List<HIS_EXP_MEST_BLTY_REQ> expBltyReqs, List<HIS_EXP_MEST_MEDICINE> medicines, List<HIS_EXP_MEST_MATERIAL> materials, ref HisExpMestResultSDO resultData)
        {
            if (this.recentResultSDO != null)
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
                resultData.ExpMaterials = materials;
                resultData.ExpMedicines = medicines;
            }
        }

        private bool IsProcessAuto(HisExpMestChmsSDO data)
        {
            try
            {
                if (!IsNotNullOrEmpty(data.Materials)) return true;
                if (HisMaterialTypeCFG.DATA.Any(o => o.IS_REUSABLE == Constant.IS_TRUE && data.Materials.Any(a => a.MaterialTypeId == o.ID)))
                {
                    LogSystem.Warn("Co Yeu cau vat tu tai su dung. Khong xu ly tu dong duyet");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
        }


        private void RollBack()
        {
            this.materialProcessor.Rollback();
            this.medicineProcessor.Rollback();
            this.hisExpMestBltyReqMaker.Rollback();
            this.hisExpMestBltyReqUpdate.RollbackData();
            this.hisExpMestUpdate.RollbackData();
        }
    }
}
