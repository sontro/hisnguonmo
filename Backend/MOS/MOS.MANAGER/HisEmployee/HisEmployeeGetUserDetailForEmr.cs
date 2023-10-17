using ACS.EFMODEL.DataModels;
using ACS.Filter;
using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.AcsUser;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.TDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisEmployee
{
    partial class HisEmployeeGet : BusinessBase
    {
        internal List<GetUserDetailForEmrTDO> GetUserDetailForEmr()
        {
            try
            {
                List<GetUserDetailForEmrTDO> resultData = new List<GetUserDetailForEmrTDO>();
                if (IsNotNullOrEmpty(HisEmployeeCFG.DATA))
                {
                    List<string> listLoginNames = HisEmployeeCFG.DATA.Select(s => s.LOGINNAME).ToList();

                    foreach (var emp in HisEmployeeCFG.DATA)
                    {
                        GetUserDetailForEmrTDO tdo = new GetUserDetailForEmrTDO();
                        tdo.ID = emp.ID;
                        tdo.IS_ADMIN = emp.IS_ADMIN.HasValue && emp.IS_ADMIN.Value == Constant.IS_TRUE ? true : false;
                        tdo.IS_ACTIVE = emp.IS_ACTIVE.HasValue && emp.IS_ACTIVE.Value == Constant.IS_TRUE ? true : false;
                        tdo.DIPLOMA = !string.IsNullOrWhiteSpace(emp.DIPLOMA) ? emp.DIPLOMA : "";
                        tdo.LOGINNAME = !string.IsNullOrWhiteSpace(emp.LOGINNAME) ? emp.LOGINNAME : "";
                        tdo.USERNAME = !string.IsNullOrWhiteSpace(emp.TDL_USERNAME) ? emp.TDL_USERNAME : "";
                        resultData.Add(tdo);
                    }
                }
                return resultData;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
