using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisAdr.SDO.Update
{
    class AdrProcessor : BusinessBase
    {
        private HisAdrUpdate hisAdrUpdate;

        internal AdrProcessor()
            : base()
        {
            this.hisAdrUpdate = new HisAdrUpdate(param);
        }

        internal AdrProcessor(CommonParam param)
            : base(param)
        {
            this.hisAdrUpdate = new HisAdrUpdate(param);
        }

        internal bool Run(HisAdrSDO data, HIS_ADR before, ref HIS_ADR adr)
        {
            bool result = false;
            try
            {
                if (data != null && data.Adr != null)
                {
                    data.Adr.TREATMENT_ID = before.TREATMENT_ID;
                    if (!this.hisAdrUpdate.Update(data.Adr, before))
                    {
                        throw new Exception("Ket thuc nghiep vu");
                    }
                    adr = data.Adr;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal void RollbackData()
        {
            try
            {
                this.hisAdrUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
