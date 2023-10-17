using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.ApproveNotTaken
{
    class ExpMestProcessor : BusinessBase
    {
        private HIS_EXP_MEST beforeExpMest = null;
        private MedicineProcessor medicineProcessor;
        private MaterialProcessor materialProcessor;
        private SereServProcessor sereServProcessor;

        internal ExpMestProcessor()
            : base()
        {
            this.Init();
        }

        internal ExpMestProcessor(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.medicineProcessor = new MedicineProcessor(param);
            this.materialProcessor = new MaterialProcessor(param);
            this.sereServProcessor = new SereServProcessor(param);
        }

        internal bool Run(HIS_EXP_MEST expMest)
        {
            bool result = false;
            try
            {
                if (expMest != null)
                {
                    List<string> sqls = new List<string>();
                    if (expMest.IS_NOT_TAKEN.HasValue && expMest.IS_NOT_TAKEN.Value == Constant.IS_TRUE)
                    {
                        return true;
                    }
                    if (expMest.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE)
                        return true;
                    List<HIS_EXP_MEST_MEDICINE> medicines = new HisExpMestMedicineGet().GetByExpMestId(expMest.ID);
                    List<HIS_EXP_MEST_MATERIAL> materials = new HisExpMestMaterialGet().GetByExpMestId(expMest.ID);
                    if ((!IsNotNullOrEmpty(medicines)) && (!IsNotNullOrEmpty(materials)))
                    {
                        LogSystem.Info("ExpMestCode: " + expMest.EXP_MEST_CODE + ". Khong co chi tiet");
                        return true;
                    }

                    if (IsNotNullOrEmpty(medicines) && medicines.Exists(e => e.IS_EXPORT.HasValue || (e.TH_AMOUNT.HasValue && e.TH_AMOUNT.Value > 0)))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_DaCoDuLieuThucXuat);
                        return false;
                    }

                    if (IsNotNullOrEmpty(materials) && materials.Exists(e => e.IS_EXPORT.HasValue || (e.TH_AMOUNT.HasValue && e.TH_AMOUNT.Value > 0)))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_DaCoDuLieuThucXuat);
                        return false;
                    }

                    this.ProcessExpMest(expMest);

                    if (!this.materialProcessor.Run(expMest, materials, ref sqls))
                    {
                        throw new Exception("materialProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.medicineProcessor.Run(expMest, medicines, ref sqls))
                    {
                        throw new Exception("medicineProcessor. Ket thuc nghiep vu");
                    }

                    if (!this.sereServProcessor.Run(expMest))
                    {
                        throw new Exception("sereServProcessor. Ket thuc nghiep vu");
                    }
                    //Xu ly sql de duoi cung de tranh rollback du lieu
                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Rollback du lieu");
                    }

                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.Rollback();
                result = false;
                expMest.IS_NOT_TAKEN = null;
            }
            return result;
        }

        private void ProcessExpMest(HIS_EXP_MEST expMest)
        {
            Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
            HIS_EXP_MEST before = Mapper.Map<HIS_EXP_MEST>(expMest);
            expMest.IS_NOT_TAKEN = Constant.IS_TRUE;
            if (!DAOWorker.HisExpMestDAO.Update(expMest))
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.CapNhatThatBai);
                throw new Exception("Cap nhat thong tin hisExpMest that bai." + LogUtil.TraceData("expMest", expMest));
            }
            this.beforeExpMest = before;
        }

        private void Rollback()
        {
            try
            {
                if (this.beforeExpMest != null)
                {
                    if (!DAOWorker.HisExpMestDAO.Update(this.beforeExpMest))
                    {
                        LogSystem.Error("Rollback du lieu that bai. Kiem tra lai du lieu");
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
