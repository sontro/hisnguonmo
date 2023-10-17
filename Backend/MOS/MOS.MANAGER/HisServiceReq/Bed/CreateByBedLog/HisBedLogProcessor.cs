using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Bed.CreateByBedLog
{
    class HisBedLogProcessor: BusinessBase
    {
        internal HisBedLogProcessor()
            : base()
        {
        }

        internal HisBedLogProcessor(CommonParam param)
            : base(param)
        {
        }

        internal void Run(Dictionary<long, List<HIS_SERVICE_REQ>> dicData)
        {
            if (dicData != null && dicData.Count > 0)
            {
                List<string> sqls = new List<string>();
                foreach (var item in dicData)
                {
                    List<long> serviceReqIds = item.Value.Select(s=>s.ID).ToList();
                    string sql = string.Format("UPDATE HIS_SERVICE_REQ SET BED_LOG_ID = {0} WHERE  %IN_CLAUSE%", item.Key);
                    sql = DAOWorker.SqlDAO.AddInClause(serviceReqIds, sql, "ID");
                    sqls.Add(sql);
                }

                if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                {
                    throw new Exception("Cap nhat HIS_SERVICE_REQ that bai. rollback du lieu");
                }
            }
        }
    }
}
