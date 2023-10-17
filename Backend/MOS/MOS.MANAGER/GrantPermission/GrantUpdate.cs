using Inventec.Common.Logging;
using Inventec.Core;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.GrantPermission
{
    class GrantUpdate : BusinessBase
    {
		
        internal GrantUpdate()
            : base()
        {

        }

        internal GrantUpdate(CommonParam param)
            : base(param)
        {
        }

        internal bool Run(GrantPermissionSDO data)
        {
            bool result = false;
            try
            {
                GrantPermissionCheck checker = new GrantPermissionCheck(param);
                bool valid = true;
                valid = valid && checker.IsValid(data);
                if (valid)
                {
                    string loginNames = data.LoginNames == null ? "" : string.Join(",", data.LoginNames);
                    string sql = string.Format("UPDATE {0} SET ALLOW_UPDATE_LOGINNAMES = :param1 WHERE ID = :param2", data.Table.ToString());

                    if (DAOWorker.SqlDAO.Execute(sql, loginNames, data.DataId))
                    {
                        return true;
                    }
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
