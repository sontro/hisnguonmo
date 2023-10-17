using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisPrepare.Approve.One
{
    class PrepareProcessor : BusinessBase
    {
        private HisPrepareUpdate hisPrepareUpdate;

        internal PrepareProcessor(CommonParam param)
            : base(param)
        {
            this.hisPrepareUpdate = new HisPrepareUpdate(param);
        }

        internal bool Run(HisPrepareApproveSDO data, HIS_PREPARE hisPrepare)
        {
            bool result = false;
            try
            {
                Mapper.CreateMap<HIS_PREPARE, HIS_PREPARE>();
                HIS_PREPARE before = Mapper.Map<HIS_PREPARE>(hisPrepare);
                hisPrepare.APPROVAL_TIME = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                hisPrepare.APPROVAL_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                hisPrepare.APPROVAL_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                if (!this.hisPrepareUpdate.Update(hisPrepare, before))
                {
                    throw new Exception("hisPrepareUpdate. Ket thuc nghiep vu");
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

        internal void RollbackData()
        {
            try
            {
                this.hisPrepareUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
