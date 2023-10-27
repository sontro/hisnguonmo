using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.MANAGER.Core.AcsRoleUser
{
    partial class AcsRoleUserBO : BusinessObjectBase
    {
        internal List<object> GetDynamic(object filter)
        {
            List<object> result = new List<object>();
            try
            {
                IDelegacyT delegacy = new AcsRoleUserGetDynamic(param, filter);
                result = delegacy.Execute<List<object>>();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = new List<object>();
            }
            return result;
        }

        internal T GetForTree<T>(object data)
        {
            T result = default(T);
            try
            {
                IDelegacyT delegacy = new AcsRoleUserGetForTree(param, data);
                result = delegacy.Execute<T>();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = default(T);
            }
            return result;
        }

        internal bool UpdateWithRole(object data)
        {
            bool result = false;
            try
            {
                IDelegacy delegacy = new AcsRoleUserUpdateWithRole(param, data);
                result = delegacy.Execute();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
