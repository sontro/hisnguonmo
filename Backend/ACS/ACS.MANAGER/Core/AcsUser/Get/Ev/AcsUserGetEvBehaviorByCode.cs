using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using System.Linq;

namespace ACS.MANAGER.Core.AcsUser.Get.Ev
{
    class AcsUserGetEvBehaviorByCode : BeanObjectBase, IAcsUserGetEv
    {
        string code;

        internal AcsUserGetEvBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        ACS_USER IAcsUserGetEv.Run()
        {
            try
            {
                AcsUserFilterQuery filter = new AcsUserFilterQuery();
                filter.LOGINNAME__OR__SUB_LOGINNAME = code;
                var users = DAOWorker.AcsUserDAO.Get(filter.Query(), new CommonParam());
                if (users == null || users.Count == 0)
                {
                    //Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => users), users));
                }
                return users.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
