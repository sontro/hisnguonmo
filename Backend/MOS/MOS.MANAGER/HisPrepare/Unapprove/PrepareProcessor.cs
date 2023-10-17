using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisPrepare.Unapprove
{
    class PrepareProcessor : BusinessBase
    {
        private HisPrepareUpdate hisPrepareUpdate;

        internal PrepareProcessor(CommonParam param)
            : base(param)
        {
            this.hisPrepareUpdate = new HisPrepareUpdate(param);
        }

        internal bool Run(HIS_PREPARE prepare)
        {
            bool result = false;
            try
            {
                Mapper.CreateMap<HIS_PREPARE, HIS_PREPARE>();
                HIS_PREPARE before = Mapper.Map<HIS_PREPARE>(prepare);
                prepare.APPROVAL_LOGINNAME = null;
                prepare.APPROVAL_TIME = null;
                prepare.APPROVAL_USERNAME = null;

                if (!this.hisPrepareUpdate.Update(prepare, before))
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
