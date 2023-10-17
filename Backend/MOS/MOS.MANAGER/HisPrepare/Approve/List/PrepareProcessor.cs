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

namespace MOS.MANAGER.HisPrepare.Approve.List
{
    class PrepareProcessor : BusinessBase
    {
        private HisPrepareUpdate hisPrepareUpdate;

        internal PrepareProcessor(CommonParam param)
            : base(param)
        {
            this.hisPrepareUpdate = new HisPrepareUpdate(param);
        }

        internal bool Run(HisPrepareApproveListSDO data, List<HIS_PREPARE> listRaw)
        {
            bool result = false;
            try
            {
                long appTime = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                string username = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                Mapper.CreateMap<HIS_PREPARE, HIS_PREPARE>();
                List<HIS_PREPARE> listBefore = Mapper.Map<List<HIS_PREPARE>>(listRaw);
                listRaw.ForEach(o =>
                    {
                        o.APPROVAL_TIME = appTime;
                        o.APPROVAL_LOGINNAME = loginname;
                        o.APPROVAL_USERNAME = username;
                    });

                if (!this.hisPrepareUpdate.UpdateList(listRaw,listBefore))
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

        internal void RolbackData()
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
