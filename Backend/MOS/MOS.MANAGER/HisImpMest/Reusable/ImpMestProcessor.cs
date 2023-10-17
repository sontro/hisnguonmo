using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Reusable
{
    class ImpMestProcessor : BusinessBase
    {
        private HisImpMestCreate hisImpMestCreate;

        internal ImpMestProcessor(CommonParam param)
            : base(param)
        {
            this.hisImpMestCreate = new HisImpMestCreate(param);
        }

        internal bool Run(HisImpMestReuseSDO data, ref HIS_IMP_MEST recentImpMest)
        {
            bool result = false;
            try
            {
                HIS_IMP_MEST impMest = new HIS_IMP_MEST();
                impMest.IMP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST;
                impMest.IMP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__TSD;
                impMest.MEDI_STOCK_ID = data.MediStockId;
                impMest.REQ_ROOM_ID = data.RequestRoomId;
                impMest.DESCRIPTION = data.Description;

                if (!this.hisImpMestCreate.Create(impMest))
                {
                    throw new Exception("Tao Phieu nhap that bai");
                }
                result = true;
                recentImpMest = impMest;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal void Rollback()
        {
            try
            {
                this.hisImpMestCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
