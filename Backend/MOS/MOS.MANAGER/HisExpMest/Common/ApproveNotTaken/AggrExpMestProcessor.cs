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
    class AggrExpMestProcessor : BusinessBase
    {
        private List<HIS_EXP_MEST> beforeExpMests = null;
        private MedicineProcessor medicineProcessor;
        private MaterialProcessor materialProcessor;
        private SereServProcessor sereServProcessor;

        internal AggrExpMestProcessor()
            : base()
        {
            this.Init();
        }

        internal AggrExpMestProcessor(CommonParam param)
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

        internal bool Run(HIS_EXP_MEST aggrExpMest, List<HIS_EXP_MEST> expChildren)
        {
            bool result = false;
            try
            {
                if (aggrExpMest != null)
                {
                    List<string> sqls = new List<string>();
                    if (aggrExpMest.IS_NOT_TAKEN.HasValue && aggrExpMest.IS_NOT_TAKEN.Value == Constant.IS_TRUE)
                    {
                        return true;
                    }
                    if (aggrExpMest.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE)
                        return true;
                    if (!IsNotNullOrEmpty(expChildren))
                    {
                        LogSystem.Info("ExpMestCode: " + aggrExpMest.EXP_MEST_CODE + ". Khong phieu xuat con");
                        return true;
                    }

                    List<HIS_EXP_MEST> allExpMest = new List<HIS_EXP_MEST>();
                    allExpMest.Add(aggrExpMest);
                    allExpMest.AddRange(expChildren);

                    List<long> expMestIds = allExpMest.Select(s => s.ID).ToList();

                    List<HIS_EXP_MEST_MEDICINE> medicines = new HisExpMestMedicineGet().GetByExpMestIds(expMestIds);
                    List<HIS_EXP_MEST_MATERIAL> materials = new HisExpMestMaterialGet().GetByExpMestIds(expMestIds);
                    if ((!IsNotNullOrEmpty(medicines)) && (!IsNotNullOrEmpty(materials)))
                    {
                        LogSystem.Info("ExpMestCode: " + aggrExpMest.EXP_MEST_CODE + ". Khong co chi tiet");
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

                    this.ProcessExpMest(allExpMest);

                    foreach (var expMest in allExpMest)
                    {
                        List<HIS_EXP_MEST_MATERIAL> materialsChildren = IsNotNullOrEmpty(materials) ? materials.Where(o => o.EXP_MEST_ID == expMest.ID).ToList() : null;
                        if (!this.materialProcessor.Run(expMest, materialsChildren, ref sqls))
                        {
                            throw new Exception("materialProcessor. Ket thuc nghiep vu");
                        }

                        List<HIS_EXP_MEST_MEDICINE> medicinesChildren = IsNotNullOrEmpty(medicines) ? medicines.Where(o => o.EXP_MEST_ID == expMest.ID).ToList() : null;
                        if (!this.medicineProcessor.Run(expMest, medicinesChildren, ref sqls))
                        {
                            throw new Exception("medicineProcessor. Ket thuc nghiep vu");
                        }
                    }

                    if (!this.sereServProcessor.Run(allExpMest))
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
                aggrExpMest.IS_NOT_TAKEN = null;
            }
            return result;
        }

        private void ProcessExpMest(List<HIS_EXP_MEST> expMests)
        {
            Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
            List<HIS_EXP_MEST> befores = Mapper.Map<List<HIS_EXP_MEST>>(expMests);
            expMests.ForEach(o => o.IS_NOT_TAKEN = Constant.IS_TRUE);
            if (!DAOWorker.HisExpMestDAO.UpdateList(expMests))
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.CapNhatThatBai);
                throw new Exception("Cap nhat thong tin hisExpMest that bai." + LogUtil.TraceData("expMest", expMests));
            }

            this.beforeExpMests = befores;
        }

        private void Rollback()
        {
            try
            {
                if (this.beforeExpMests != null)
                {
                    if (!DAOWorker.HisExpMestDAO.UpdateList(this.beforeExpMests))
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
