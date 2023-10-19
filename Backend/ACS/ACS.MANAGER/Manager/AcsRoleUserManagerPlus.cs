using ACS.MANAGER.Core.AcsRoleUser;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Manager
{
    public partial class AcsRoleUserManager : Inventec.Backend.MANAGER.ManagerBase
    {
        public T GetForTree<T>(object data)
        {
            T result = default(T);
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                AcsRoleUserBO bo = new AcsRoleUserBO();
                bo.CopyCommonParamInfoGet(param);
                result = bo.GetForTree<T>(data);
                CopyCommonParamInfo(bo);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = default(T);
            }
            return result;
        }

        public List<object> GetDynamic(object data)
        {
            List<object> result = default(List<object>);
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                AcsRoleUserBO bo = new AcsRoleUserBO();
                bo.CopyCommonParamInfoGet(param);
                result = bo.GetDynamic(data);
                CopyCommonParamInfo(bo);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = default(List<object>);
            }
            return result;
        }

        public object UpdateWithRole(object data)
        {
            object result = false;
            try
            {
                if (!IsNotNull(data)) throw new ArgumentNullException("data");
                AcsRoleUserBO bo = new AcsRoleUserBO();
                if (bo.UpdateWithRole(data))
                {
                    result = true;
                }
                CopyCommonParamInfo(bo);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
