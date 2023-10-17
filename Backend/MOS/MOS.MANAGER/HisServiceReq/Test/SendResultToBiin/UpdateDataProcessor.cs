using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Test.SendResultToBiin
{
    class UpdateDataProcessor
    {
        public bool Run(List<HIS_SERVICE_REQ> serviceReq)
        {
            try
            {
                if (serviceReq != null && serviceReq.Count > 0)
                {
                    List<string> lstSql = new List<string>();
                    foreach (var item in serviceReq)
                    {
                        lstSql.Add(string.Format("UPDATE HIS_SERVICE_REQ SET BIIN_TEST_RESULT = '{1}' WHERE ID ='{0}'", item.ID, item.BIIN_TEST_RESULT));
                    }
                    return DAOWorker.SqlDAO.Execute(lstSql);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }
    }
}
