using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace MOS.MANAGER.HisSereServTein
{
    class MaterialNormationData
    {
        public long ID { get; set; }
        public long? MACHINE_ID { get; set; }
        public long SERVICE_ID { get; set; }
        public long? MATERIAL_TYPE_ID { get; set; }
        public decimal? EXPEND_AMOUNT { get; set; }
    }

    partial class HisSereServTeinGet : GetBase
    {
        internal TestMaterialByNormationCollectionSDO GetMaterialAmountByNormation(HisSereServTeinAmountByNormationFilter filter)
        {
            try
            {
                TestMaterialByNormationCollectionSDO result = null;
                string query = "SELECT DISTINCT SS.ID, SST.MACHINE_ID, SS.SERVICE_ID, MSM.MATERIAL_TYPE_ID, MSM.EXPEND_AMOUNT"
                                + " FROM HIS_SERE_SERV_TEIN SST "
                                + " JOIN HIS_SERE_SERV SS ON SS.ID = SST.SERE_SERV_ID "
                                + " JOIN HIS_SERVICE_REQ SRQ ON SRQ.ID = SST.TDL_SERVICE_REQ_ID "
                                + " JOIN HIS_MACHINE_SERV_MATY MSM ON MSM.SERVICE_ID = SS.SERVICE_ID AND MSM.MACHINE_ID = SST.MACHINE_ID "
                                + " WHERE SRQ.IS_DELETE = 0 AND SS.IS_DELETE = 0 AND SS.IS_NO_EXECUTE IS NULL "
                                + " AND SRQ.SERVICE_REQ_STT_ID = " + IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT
                                + " AND SRQ.SERVICE_REQ_TYPE_ID = " + IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN
                                + " AND MSM.MATERIAL_TYPE_ID IS NOT NULL "
                                + " AND MSM.EXPEND_AMOUNT IS NOT NULL "
                                + " AND SST.EXP_MEST_ID IS NULL ";
                if (filter.FINISH_TIME_FROM.HasValue)
                {
                    query += string.Format(" AND SRQ.FINISH_TIME >= {0}", filter.FINISH_TIME_FROM.Value);
                }
                if (filter.FINISH_TIME_TO.HasValue)
                {
                    query += string.Format(" AND SRQ.FINISH_TIME <= {0}", filter.FINISH_TIME_TO.Value);
                }
                if (filter.EXECUTE_ROOM_ID.HasValue)
                {
                    query += string.Format(" AND SRQ.EXECUTE_ROOM_ID = {0}", filter.EXECUTE_ROOM_ID.Value);
                }

                List<MaterialNormationData> tmp = DAOWorker.SqlDAO.GetSql<MaterialNormationData>(query);

                if (tmp != null && tmp.Count > 0)
                {
                    result = new TestMaterialByNormationCollectionSDO();
                    result.SereServIds = tmp.Select(o => o.ID).ToList();
                    result.TestMaterialByNormations = tmp.GroupBy(o => o.MATERIAL_TYPE_ID).ToList()
                        .Select(g => new TestMaterialByNormationSDO {
                            MATERIAL_TYPE_ID = g.Key,
                            NORMATION_AMOUNT = g.Sum(t => t.EXPEND_AMOUNT.Value)
                        }).ToList();
                }
                return result;
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
