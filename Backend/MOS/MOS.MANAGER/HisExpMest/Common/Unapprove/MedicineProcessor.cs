using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisExpMestMetyReq;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.Unapprove
{
    class MedicineProcessor : BusinessBase
    {
        private HisMedicineBeanUnlockByExpMest beanUnlock;
        private HisExpMestMetyReqDecreaseDdAmount hisExpMestMetyReqDecreaseDdAmount;
        private HisExpMestMedicineUpdate hisExpMestMedicineUpdate;

        internal MedicineProcessor()
            : base()
        {
            this.Init();
        }

        internal MedicineProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.beanUnlock = new HisMedicineBeanUnlockByExpMest(param);
            this.hisExpMestMetyReqDecreaseDdAmount = new HisExpMestMetyReqDecreaseDdAmount(param);
            this.hisExpMestMedicineUpdate = new HisExpMestMedicineUpdate(param);
        }

        internal bool Run(List<HIS_EXP_MEST_MEDICINE> expMestMedicines, HIS_EXP_MEST expMest, ref List<string> sqls)
        {
            try
            {
                if (IsNotNullOrEmpty(expMestMedicines))
                {
                    //Neu phieu xuat tao lenh luc duyet ==> khi huy duyet se huy thong tin lenh
                    //Nguoc lai, chi xoa thong tin duyet chu khong xoa toan bo lenh
                    if (HisExpMestConstant.HAS_REQ_EXP_MEST_TYPE_IDs.Contains(expMest.EXP_MEST_TYPE_ID) && expMest.IS_REQUEST_BY_PACKAGE != Constant.IS_TRUE)
                    {
                        this.DeleteExpMestMedicine(expMest.ID, expMestMedicines, ref sqls);
                    }
                    else
                    {
                        this.RemoveApproveInfo(expMest, expMestMedicines);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        private void DeleteExpMestMedicine(long expMestId, List<HIS_EXP_MEST_MEDICINE> expMestMedicines, ref List<string> sqls)
        {
            List<HIS_EXP_MEST_METY_REQ> metyReqs = new HisExpMestMetyReqGet().GetByExpMestId(expMestId);

            List<long> expMestMedicineIds = expMestMedicines.Select(o => o.ID).ToList();

            if (IsNotNullOrEmpty(expMestMedicineIds))
            {
                if (sqls == null)
                {
                    sqls = new List<string>();
                }

                string sqlUpdateBean = this.beanUnlock.GenSql(expMestMedicineIds);
                string sqlDeleteExpMestMedicine = DAOWorker.SqlDAO.AddInClause(expMestMedicineIds, "UPDATE HIS_EXP_MEST_MEDICINE SET IS_DELETE = 1, EXP_MEST_ID = NULL, MEDICINE_ID = NULL, TDL_MEDI_STOCK_ID = NULL, TDL_MEDICINE_TYPE_ID = NULL WHERE %IN_CLAUSE% ", "ID");

                //Luu y: can cap nhat bean truoc khi xoa exp_mest_medicine (tranh loi FK)
                sqls.Add(sqlUpdateBean);
                sqls.Add(sqlDeleteExpMestMedicine);
            }

            //Neu huy duyet thanh cong thi cap nhat so luong da duyet vao mety_req
            if (IsNotNullOrEmpty(metyReqs))
            {
                ProcessDdAmount(expMestMedicines, metyReqs);
            }
        }

        private void ProcessDdAmount(List<HIS_EXP_MEST_MEDICINE> expMestMedicines, List<HIS_EXP_MEST_METY_REQ> metyReqs)
        {
            if (!IsNotNullOrEmpty(metyReqs)) return;

            Dictionary<long, decimal> decreaseDic = new Dictionary<long, decimal>();

            //Cap nhat so luong da duyet
            foreach (HIS_EXP_MEST_METY_REQ req in metyReqs)
            {
                decimal unapprovalAmount = expMestMedicines
                    .Where(o => o.EXP_MEST_METY_REQ_ID == req.ID)
                    .Sum(o => o.AMOUNT);
                if (unapprovalAmount > 0)
                {
                    decreaseDic.Add(req.ID, unapprovalAmount);
                }
            }
            if (IsNotNullOrEmpty(decreaseDic))
            {
                if (!this.hisExpMestMetyReqDecreaseDdAmount.Run(decreaseDic))
                {
                    throw new Exception("Cap nhat dd_amount that bai. Rollback");
                }
            }
        }

        private void RemoveApproveInfo(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> expMestMedicines)
        {
            if (IsNotNullOrEmpty(expMestMedicines))
            {
                Mapper.CreateMap<HIS_EXP_MEST_MEDICINE, HIS_EXP_MEST_MEDICINE>();
                List<HIS_EXP_MEST_MEDICINE> befores = Mapper.Map<List<HIS_EXP_MEST_MEDICINE>>(expMestMedicines);
                expMestMedicines.ForEach(o =>
                {
                    o.APPROVAL_LOGINNAME = null;
                    o.APPROVAL_TIME = null;
                    o.APPROVAL_USERNAME = null;
                });
                if (!this.hisExpMestMedicineUpdate.UpdateList(expMestMedicines, befores))
                {
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }

                if (expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__CK
                    && expMest.IS_REQUEST_BY_PACKAGE == Constant.IS_TRUE)
                {
                    List<HIS_EXP_MEST_METY_REQ> metyReqs = new HisExpMestMetyReqGet().GetByExpMestId(expMest.ID);
                    this.ProcessDdAmount(expMestMedicines, metyReqs);
                }
            }
        }

        internal void Rollback()
        {
            this.hisExpMestMedicineUpdate.RollbackData();
            this.hisExpMestMetyReqDecreaseDdAmount.Rollback();
        }
    }
}
