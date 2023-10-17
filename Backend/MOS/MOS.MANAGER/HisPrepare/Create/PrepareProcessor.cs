using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisPrepare.Create
{
    class PrepareProcessor : BusinessBase
    {
        private HisPrepareCreate hisPrepareCreate;

        internal PrepareProcessor(CommonParam param)
            : base(param)
        {
            this.hisPrepareCreate = new HisPrepareCreate(param);
        }

        internal bool Run(HisPrepareSDO data, ref HIS_PREPARE hisPrepare)
        {
            bool result = false;
            try
            {
                HIS_PREPARE prepare = new HIS_PREPARE();
                prepare.TREATMENT_ID = data.TreatmentId;
                prepare.REQ_LOGINNAME = data.ReqLoginname;
                prepare.REQ_USERNAME = data.ReqUsername;
                prepare.FROM_TIME = data.FromTime;
                prepare.TO_TIME = data.ToTime;
                prepare.DESCRIPTION = data.Description;

                if (!this.hisPrepareCreate.Create(prepare))
                {
                    throw new Exception("hisPrepareCreate. Ket thuc nghiep vu");
                }
                hisPrepare = prepare;
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

        internal void RollbackData()
        {
            try
            {
                this.hisPrepareCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
