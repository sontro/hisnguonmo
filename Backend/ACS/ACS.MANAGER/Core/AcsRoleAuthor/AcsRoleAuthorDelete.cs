using Inventec.Common.Logging;
using Inventec.Core;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.MANAGER.AcsRoleAuthor
{
    partial class AcsRoleAuthorDelete : BusinessBase
    {
        internal AcsRoleAuthorDelete()
            : base()
        {

        }

        internal AcsRoleAuthorDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(ACS_ROLE_AUTHOR data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                AcsRoleAuthorCheck checker = new AcsRoleAuthorCheck(param);
                valid = valid && IsNotNull(data);
                ACS_ROLE_AUTHOR raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.AcsRoleAuthorDAO.Delete(data);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool DeleteList(List<ACS_ROLE_AUTHOR> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                AcsRoleAuthorCheck checker = new AcsRoleAuthorCheck(param);
                List<ACS_ROLE_AUTHOR> listRaw = new List<ACS_ROLE_AUTHOR>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.AcsRoleAuthorDAO.DeleteList(listData);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
