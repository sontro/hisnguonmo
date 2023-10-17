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

namespace MOS.MANAGER.HisPrepare.Update
{
    class PrepareProcessor : BusinessBase
    {
        private HisPrepareUpdate hisPrepareUpdate;

        internal PrepareProcessor(CommonParam param)
            : base(param)
        {
            this.hisPrepareUpdate = new HisPrepareUpdate(param);
        }

        internal bool Run(HisPrepareSDO data, HIS_PREPARE raw)
        {
            bool result = false;
            try
            {
                Mapper.CreateMap<HIS_PREPARE, HIS_PREPARE>();
                HIS_PREPARE before = Mapper.Map<HIS_PREPARE>(raw);
                raw.REQ_LOGINNAME = data.ReqLoginname;
                raw.REQ_USERNAME = data.ReqUsername;
                raw.FROM_TIME = data.FromTime;
                raw.TO_TIME = data.ToTime;
                raw.DESCRIPTION = data.Description;

                if (String.IsNullOrWhiteSpace(raw.REQ_LOGINNAME))
                {
                    raw.REQ_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    raw.REQ_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                }

                if (!this.hisPrepareUpdate.Update(raw, before))
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
