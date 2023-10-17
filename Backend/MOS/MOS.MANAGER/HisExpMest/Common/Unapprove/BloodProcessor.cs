using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestBlood;
using MOS.MANAGER.HisExpMestBltyReq;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisServiceReq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.MANAGER.HisBlood.Update;

namespace MOS.MANAGER.HisExpMest.Common.Unapprove
{
    class BloodProcessor : BusinessBase
    {
        private HisBloodUnlock bloodUnlock;
        private HisExpMestBltyReqDecreaseDdAmount hisExpMestBltyReqDecreaseDdAmount;
        private HisExpMestBloodUpdate hisExpMestBloodUpdate;

        internal BloodProcessor()
            : base()
        {
            this.Init();
        }

        internal BloodProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.bloodUnlock = new HisBloodUnlock(param);
            this.hisExpMestBltyReqDecreaseDdAmount = new HisExpMestBltyReqDecreaseDdAmount(param);
            this.hisExpMestBloodUpdate = new HisExpMestBloodUpdate(param);
        }

        internal bool Run(List<HIS_EXP_MEST_BLOOD> expMestBloods, HIS_EXP_MEST expMest, ref List<string> sqls)
        {
            try
            {
                if (IsNotNullOrEmpty(expMestBloods))
                {
                    //Neu phieu xuat tao lenh luc duyet ==> khi huy duyet se huy thong tin lenh
                    //Nguoc lai, chi xoa thong tin duyet chu khong xoa toan bo lenh
                    if (HisExpMestConstant.HAS_REQ_EXP_MEST_TYPE_IDs.Contains(expMest.EXP_MEST_TYPE_ID))
                    {
                        this.DeleteExpMestBlood(expMest.ID, expMestBloods, ref sqls);
                    }
                    else
                    {
                        this.RemoveApproveInfo(expMestBloods);
                    }
                    this.ProcessDVKT(expMest.SERVICE_REQ_ID);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }


        private void ProcessDVKT(long? serviceReqId)
        {
            if (HisServiceReqCFG.IS_ALLOW_PROCESSING_TEST_SERVICE_REQ_WHEN_APPROVE_BLOOD && serviceReqId.HasValue)
            {
                List<HIS_SERVICE_REQ> dvkts = new HisServiceReqGet().GetByParentId(serviceReqId.Value);
                List<HIS_SERVICE_REQ> dvktNotInReqStatus = IsNotNullOrEmpty(dvkts) ? dvkts.Where(o => o.SERVICE_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL).ToList() : null;
                if (IsNotNullOrEmpty(dvktNotInReqStatus))
                {
                    dvktNotInReqStatus.ForEach(o =>
                    {
                        o.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL;
                        //o.EXE = null;
                        o.EXECUTE_LOGINNAME = null;
                        o.EXECUTE_USERNAME = null;
                    });

                    if (!DAOWorker.HisServiceReqDAO.UpdateList(dvktNotInReqStatus))
                    {
                        throw new Exception("Cap nhat trang thai y lenh dinh kem that bai");
                    }
                }
            }
        }

        private void DeleteExpMestBlood(long expMestId, List<HIS_EXP_MEST_BLOOD> expMestBloods, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(expMestBloods))
            {
                List<long> bloodIds = expMestBloods.Select(o => o.BLOOD_ID).ToList();
                this.bloodUnlock.Run(bloodIds);

                List<long> expMestBloodIds = expMestBloods.Select(o => o.ID).ToList();
                if (IsNotNull(expMestBloodIds))
                {
                    if (sqls == null)
                    {
                        sqls = new List<string>();
                    }

                    string sqlDeleteExpMestBlood = DAOWorker.SqlDAO.AddInClause(expMestBloodIds, "DELETE HIS_EXP_MEST_BLOOD WHERE %IN_CLAUSE% ", "ID");
                    sqls.Add(sqlDeleteExpMestBlood);
                }
            }

            List<HIS_EXP_MEST_BLTY_REQ> metyReqs = new HisExpMestBltyReqGet().GetByExpMestId(expMestId);
            //Neu huy duyet thanh cong thi cap nhat so luong da duyet vao mety_req
            if (IsNotNullOrEmpty(metyReqs))
            {
                Dictionary<long, decimal> decreaseDic = new Dictionary<long, decimal>();

                //Cap nhat so luong da duyet
                foreach (HIS_EXP_MEST_BLTY_REQ req in metyReqs)
                {
                    decimal unapprovalAmount = expMestBloods
                        .Where(o => o.EXP_MEST_BLTY_REQ_ID == req.ID).Count();
                    if (unapprovalAmount > 0)
                    {
                        decreaseDic.Add(req.ID, unapprovalAmount);
                    }
                }
                if (IsNotNullOrEmpty(decreaseDic))
                {
                    if (!this.hisExpMestBltyReqDecreaseDdAmount.Run(decreaseDic))
                    {
                        throw new Exception("Cap nhat dd_amount that bai. Rollback");
                    }
                }
            }
        }

        private void RemoveApproveInfo(List<HIS_EXP_MEST_BLOOD> expMestBloods)
        {
            if (IsNotNullOrEmpty(expMestBloods))
            {
                Mapper.CreateMap<HIS_EXP_MEST_BLOOD, HIS_EXP_MEST_BLOOD>();
                List<HIS_EXP_MEST_BLOOD> befores = Mapper.Map<List<HIS_EXP_MEST_BLOOD>>(expMestBloods);
                expMestBloods.ForEach(o =>
                {
                    o.APPROVAL_LOGINNAME = null;
                    o.APPROVAL_TIME = null;
                    o.APPROVAL_USERNAME = null;
                });
                if (!this.hisExpMestBloodUpdate.UpdateList(expMestBloods, befores))
                {
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }
            }
        }

        internal void Rollback()
        {
            this.hisExpMestBloodUpdate.RollbackData();
            this.hisExpMestBltyReqDecreaseDdAmount.Rollback();
        }
    }
}
