using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.MANAGER.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.ConfirmAndGetDetail
{
    class ExpMestConfirmAndGetDetail : BusinessBase
    {
        private HisExpMestUpdate expMestUpdateProcessor;

        internal ExpMestConfirmAndGetDetail()
            : base()
        {
            this.Init();
        }

        internal ExpMestConfirmAndGetDetail(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.expMestUpdateProcessor = new HisExpMestUpdate(param);
        }

        internal bool Run(long id, ref ExpMestDetailResultSDO resultData)
        {
            bool result = false;
            try 
	        {
                HIS_EXP_MEST expMest = null;
                HisExpMestCheck commonChecker = new HisExpMestCheck(param);

                bool valid = true;
                valid = valid && commonChecker.VerifyId(id, ref expMest);
                if (valid)
                {
                    Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
                    HIS_EXP_MEST beforeExpMest = Mapper.Map<HIS_EXP_MEST>(expMest);

                    expMest.IS_CONFIRM = Constant.IS_TRUE;

                    if (!this.expMestUpdateProcessor.Update(expMest, beforeExpMest))
                    {
                        throw new Exception("Cap nhat trang thai xac nhan phieu tong hop that bai. Rollback du lieu");
                    }

                    List<V_HIS_EXP_MEST_MATERIAL> materials = new HisExpMestMaterialGet().GetViewByAggrExpMestId(expMest.ID);
                    List<V_HIS_EXP_MEST_MEDICINE> medicines = new HisExpMestMedicineGet().GetViewByAggrExpMestId(expMest.ID);
                    resultData = new ExpMestDetailResultSDO();
                    resultData.ExpMest = expMest;
                    resultData.ExpMestMaterials = materials;
                    resultData.ExpMestMedicines = medicines;
                    result = true;
                }
	        }
	        catch (Exception ex)
	        {
                this.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
	        }

            return result;
        }

        private void RollbackData()
        {
            this.expMestUpdateProcessor.RollbackData();
        }
    }
}
