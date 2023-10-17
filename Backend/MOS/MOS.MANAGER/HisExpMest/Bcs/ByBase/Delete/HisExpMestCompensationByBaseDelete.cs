using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Base.BaseCompensationByBase.Delete
{
    class HisExpMestCompensationByBaseDelete : BusinessBase
    {
        private ExpMestProcessor expMestProcessor;
        private MatyReqProcessor matyReqProcessor;
        private MetyReqProcessor metyReqProcessor;

        internal HisExpMestCompensationByBaseDelete()
            : base()
        {
            this.Init();
        }

        internal HisExpMestCompensationByBaseDelete(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.expMestProcessor = new ExpMestProcessor(param);
            this.matyReqProcessor = new MatyReqProcessor(param);
            this.metyReqProcessor = new MetyReqProcessor(param);
        }

        internal bool Run(HisExpMestSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_EXP_MEST raw = null;
                HisExpMestCheck commonChecker = new HisExpMestCheck(param);
                HisExpMestCompensationByBaseCheck checker = new HisExpMestCompensationByBaseCheck(param);
                valid = valid && commonChecker.VerifyRequireField(data);
                valid = valid && commonChecker.VerifyId(data.ExpMestId, ref raw);
                valid = valid && commonChecker.IsUnlock(raw);
                valid = valid && commonChecker.IsUnNotTaken(raw);
                valid = valid && commonChecker.HasNotInExpMestAggr(raw);//thuoc phieu nhap tong hop, ko cho xoa
                valid = valid && commonChecker.VerifyStatusForDelete(raw);
                valid = valid && commonChecker.HasNoNationalCode(raw);
                valid = valid && commonChecker.HasNotBill(raw);
                valid = valid && checker.IsCompensationBase(raw);
                valid = valid && this.ValidData(raw);
                if (valid)
                {
                    List<string> sqls = new List<string>();

                    if (!this.matyReqProcessor.Run(raw, ref sqls))
                    {
                        throw new Exception("matyReqProcessor. Ket thuc nghiep vu");
                    }
                    if (!this.metyReqProcessor.Run(raw, ref sqls))
                    {
                        throw new Exception("metyReqProcessor. Ket thuc nghiep vu");
                    }
                    if (!this.expMestProcessor.Run(raw, ref sqls))
                    {
                        throw new Exception("expMestProcessor. Ket thuc nghiep vu");
                    }

                    if (!DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("sqls:" + sqls.ToString());
                    }

                    new EventLogGenerator(EventLog.Enum.HisExpMest_HuyPhieuXuat).ExpMestCode(raw.EXP_MEST_CODE).Run();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        private bool ValidData(HIS_EXP_MEST raw)
        {
            bool valid = true;
            try
            {
                List<HIS_EXP_MEST_MEDICINE> medicines = new HisExpMestMedicineGet().GetByExpMestId(raw.ID);
                List<HIS_EXP_MEST_MATERIAL> materials = new HisExpMestMaterialGet().GetByExpMestId(raw.ID);
                if (IsNotNullOrEmpty(medicines) || IsNotNullOrEmpty(materials))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("Phieu xua bcs da ton tai du lieu duyet");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

    }
}
