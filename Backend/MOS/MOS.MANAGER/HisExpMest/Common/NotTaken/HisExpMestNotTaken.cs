using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.NotTaken
{
    class HisExpMestNotTaken : BusinessBase
    {

        private HIS_EXP_MEST beforeUpdate = null;

        private MedicineProcessor medicineProcessor;
        private MaterialProcessor materialProcessor;

        internal HisExpMestNotTaken()
            : base()
        {
            this.Init();
        }

        internal HisExpMestNotTaken(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.medicineProcessor = new MedicineProcessor(param);
            this.materialProcessor = new MaterialProcessor(param);
        }

        internal bool Run(HIS_EXP_MEST data, ref HIS_EXP_MEST resultData)
        {
            bool result = true;
            try
            {
                bool valid = true;
                HIS_EXP_MEST raw = null;
                List<HIS_EXP_MEST_MEDICINE> medicines = null;
                List<HIS_EXP_MEST_MATERIAL> materials = null;
                HisExpMestCheck commonChecker = new HisExpMestCheck(param);
                HisExpMestNotTakenCheck checker = new HisExpMestNotTakenCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && commonChecker.VerifyId(data.ID, ref raw);
                valid = valid && commonChecker.IsUnlock(raw);
                valid = valid && checker.IsExpMestSale(raw);
                valid = valid && checker.IsNotFinished(raw);
                valid = valid && checker.IsNotExpoted(raw, ref medicines, ref materials);
                valid = valid && commonChecker.IsUnNotTaken(raw);
                if (valid)
                {
                    List<string> sqls = new List<string>();

                    this.ProcessExpMest(raw);

                    if (!medicineProcessor.Run(raw, medicines, ref sqls))
                    {
                        throw new Exception("medicineProcessor. Ket thuc nghiep vu");
                    }

                    if (!materialProcessor.Run(raw, materials, ref sqls))
                    {
                        throw new Exception("materialProcessor. Ket thuc nghiep vu");
                    }

                    //Xu ly sql de duoi cung de tranh rollback du lieu
                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Rollback du lieu");
                    }

                    result = true;
                    resultData = raw;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.Rollback();
                result = false;
            }
            return result;
        }

        private void ProcessExpMest(HIS_EXP_MEST raw)
        {
            if (raw != null)
            {

                Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
                HIS_EXP_MEST before = Mapper.Map<HIS_EXP_MEST>(raw);
                raw.IS_NOT_TAKEN = Constant.IS_TRUE;
                if (!DAOWorker.HisExpMestDAO.Update(raw))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.CapNhatThatBai);
                    throw new Exception("Cap nhat thong tin hisExpMest that bai." + LogUtil.TraceData("raw", raw));
                }
                this.beforeUpdate = before;
            }
        }

        private void Rollback()
        {
            try
            {
                if (this.beforeUpdate != null)
                {
                    if (!DAOWorker.HisExpMestDAO.Update(this.beforeUpdate))
                    {
                        LogSystem.Warn("Rollback exp_mest that bai");
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
