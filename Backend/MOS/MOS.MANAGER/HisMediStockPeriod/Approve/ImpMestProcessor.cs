using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisImpMest;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMediStockPeriod.Approve
{
    class ImpMestProcessor : BusinessBase
    {
        private HisImpMestCreate hisImpMestCreate;

        internal ImpMestProcessor(CommonParam param)
            : base(param)
        {
            this.hisImpMestCreate = new HisImpMestCreate(param);
        }

        internal bool Run(HisMestPeriodApproveSDO data, List<HisMaterialWithPatySDO> ImpMaterials, List<HisMedicineWithPatySDO> ImpMedicines, HIS_MEDI_STOCK_PERIOD period, ref HIS_IMP_MEST impMest)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(ImpMaterials) || IsNotNullOrEmpty(ImpMedicines))
                {
                    HIS_IMP_MEST imp = new HIS_IMP_MEST();
                    imp.DESCRIPTION = data.Description;
                    imp.IMP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST;
                    imp.IMP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__KK;
                    imp.MEDI_STOCK_ID = period.MEDI_STOCK_ID;
                    imp.REQ_ROOM_ID = data.WorkingRoomId;
                    imp.SOURCE_MEST_PERIOD_ID = period.ID;

                    if (!this.hisImpMestCreate.Create(imp))
                    {
                        throw new Exception("hisImpMestCreate. Ket thuc nghiep vu");
                    }
                    impMest = imp;
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
            this.hisImpMestCreate.RollbackData();
        }
    }
}
