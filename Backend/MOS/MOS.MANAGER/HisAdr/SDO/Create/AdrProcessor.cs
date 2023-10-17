using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisAdr.SDO.Create
{
    class AdrProcessor : BusinessBase
    {
        private HisAdrCreate hisAdrCreate;
        internal AdrProcessor()
            : base()
        {
            this.hisAdrCreate = new HisAdrCreate(param);
        }

        internal AdrProcessor(CommonParam param)
            : base(param)
        {
            this.hisAdrCreate = new HisAdrCreate(param);
        }

        internal bool Run(HisAdrSDO data, ref HIS_ADR hisAdr)
        {
            bool result = false;
            try
            {
                if (data != null && data.Adr != null)
                {
                    if (!this.hisAdrCreate.Create(data.Adr))
                    {
                        throw new Exception("Ket thuc nghiep vu");
                    }
                    hisAdr = data.Adr;
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
                this.hisAdrCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
