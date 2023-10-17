using Inventec.Core;
using Inventec.Common.Logging;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMaterialType
{
    partial class HisMaterialTypeGet : GetBase
    {
        internal List<GetMaterialTypeForEmrResultSDO> GetMaterialTypeForEmr(long treatmentId)
        {
            try
            {
                List<GetMaterialTypeForEmrResultSDO> result = new List<GetMaterialTypeForEmrResultSDO>();

                HisExpMestMaterialViewFilterQuery filter = new HisExpMestMaterialViewFilterQuery();
                filter.TDL_TREATMENT_ID = treatmentId;
                List<V_HIS_EXP_MEST_MATERIAL> epMaterials = new HisExpMestMaterialGet().GetView(filter);

                if (IsNotNullOrEmpty(epMaterials))
                {
                    // Lay ra tat ca cac thong tin chi dinh cha
                            List<long> ssParentIds = epMaterials.Where(o => o.SERE_SERV_PARENT_ID.HasValue).Select(s => s.SERE_SERV_PARENT_ID.Value).ToList();
                            List<V_HIS_SERE_SERV> ssParents = new List<V_HIS_SERE_SERV>();
                            if (IsNotNullOrEmpty(ssParentIds))
                            {
                                StringBuilder sqlBuilder = new StringBuilder();
                                sqlBuilder.Append("SELECT * FROM V_HIS_SERE_SERV WHERE %IN_CLAUSE%");
                                string query = this.AddInClause(ssParentIds, sqlBuilder.ToString(), "ID");
                                List<V_HIS_SERE_SERV> ss = DAOWorker.SqlDAO.GetSql<V_HIS_SERE_SERV>(query);
                                ssParents.AddRange(ss);
                            }

                    foreach (var epMaterial in epMaterials)
                    {
                        GetMaterialTypeForEmrResultSDO sdo = new GetMaterialTypeForEmrResultSDO();
                        sdo.MATERIAL_TYPE_CODE = epMaterial.MATERIAL_TYPE_CODE;
                        sdo.MATERIAL_TYPE_NAME = epMaterial.MATERIAL_TYPE_NAME;
                        sdo.SERVICE_UNIT_NAME = epMaterial.SERVICE_UNIT_NAME;
                        sdo.EXP_MEST_STT_ID = epMaterial.EXP_MEST_STT_ID;
                        sdo.AMOUNT = epMaterial.AMOUNT;
                        sdo.SereServParentId = epMaterial.SERE_SERV_PARENT_ID;

                        // Lay thong tin dich vu cha
                        V_HIS_SERE_SERV ssParent = IsNotNullOrEmpty(ssParents) ? ssParents.FirstOrDefault(o => o.ID == epMaterial.SERE_SERV_PARENT_ID) : null;
                        //V_HIS_SERVICE service = IsNotNull(ssParent) ? HisServiceCFG.DATA_VIEW.FirstOrDefault(o => o.ID == ssParent.SERVICE_ID) : null;
                        sdo.ServiceId = ssParent != null ? (long?)ssParent.SERVICE_ID : null;
                        sdo.ServiceName = ssParent != null ? ssParent.TDL_SERVICE_NAME : null;
                        result.Add(sdo);
                    }
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
