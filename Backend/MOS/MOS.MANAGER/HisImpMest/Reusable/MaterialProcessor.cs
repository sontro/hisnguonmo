using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Reusable
{
    class MaterialProcessor : BusinessBase
    {
        private HisImpMestMaterialCreate hisImpMestMaterialCreate;

        internal MaterialProcessor(CommonParam param)
            : base(param)
        {
            this.hisImpMestMaterialCreate = new HisImpMestMaterialCreate(param);
        }

        internal bool Run(HisImpMestReuseSDO data, HIS_IMP_MEST impMest)
        {
            bool result = false;
            try
            {
                List<HIS_IMP_MEST_MATERIAL> createds = new List<HIS_IMP_MEST_MATERIAL>();
                foreach (var item in data.MaterialReuseSDOs)
                {
                    HIS_IMP_MEST_MATERIAL impMate = new HIS_IMP_MEST_MATERIAL();
                    impMate.AMOUNT = 1;
                    impMate.IMP_MEST_ID = impMest.ID;
                    impMate.MATERIAL_ID = item.MaterialId;
                    impMate.REMAIN_REUSE_COUNT = item.ReusCount;
                    impMate.SERIAL_NUMBER = item.SerialNumber;
                    createds.Add(impMate);
                }

                if (!this.hisImpMestMaterialCreate.CreateList(createds))
                {
                    throw new Exception("Tao chi tiet phieu nhap tai su dung that bai");
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void Rollback()
        {
            try
            {
                this.hisImpMestMaterialCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
