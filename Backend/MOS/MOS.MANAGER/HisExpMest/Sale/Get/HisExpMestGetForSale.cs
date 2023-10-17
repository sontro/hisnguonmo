using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisMaterialBean;
using MOS.MANAGER.HisMedicineBean;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisServiceReqMaty;
using MOS.MANAGER.HisServiceReqMety;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Sale.Get
{
    class HisExpMestGetForSale : GetBase
    {
        internal HisExpMestGetForSale()
            : base()
        {

        }

        internal HisExpMestGetForSale(CommonParam param)
            : base(param)
        {

        }

        internal HisExpMestForSaleSDO GetForSale(HisExpMestForSaleFilter filter)
        {
            HisExpMestForSaleSDO result = null;
            try
            {
                if (IsNotNull(filter)
                    && (!String.IsNullOrWhiteSpace(filter.SERVICE_REQ_CODE__EXACT) || !String.IsNullOrWhiteSpace(filter.TREATMENT_CODE__EXACT) || IsNotNullOrEmpty(filter.SERVICE_REQ_IDs)))
                {
                    string sqlServiceReq = "SELECT * FROM HIS_SERVICE_REQ WHERE (SERVICE_REQ_TYPE_ID = :param1 OR SERVICE_REQ_TYPE_ID = :param2 OR SERVICE_REQ_TYPE_ID = :param3) AND ";

                    string code = "";
                    if (filter.SERVICE_REQ_IDs != null && filter.SERVICE_REQ_IDs.Count > 0)
                    {
                        code = "1";
                        sqlServiceReq += DAOWorker.SqlDAO.AddInClause(filter.SERVICE_REQ_IDs, " %IN_CLAUSE% ", "ID") + " AND 1 = :param4";
                    }
                    else if (!String.IsNullOrWhiteSpace(filter.SERVICE_REQ_CODE__EXACT))
                    {
                        code = filter.SERVICE_REQ_CODE__EXACT;
                        sqlServiceReq = sqlServiceReq + "SERVICE_REQ_CODE = :param4";
                    }
                    else
                    {
                        code = filter.TREATMENT_CODE__EXACT;
                        sqlServiceReq = sqlServiceReq + "TDL_TREATMENT_CODE = :param4";
                    }

                    List<HIS_SERVICE_REQ> serviceReqs = DAOWorker.SqlDAO.GetSql<HIS_SERVICE_REQ>(sqlServiceReq, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONDT, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONK, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONTT, code);
                    if (IsNotNullOrEmpty(serviceReqs))
                    {
                        result = new HisExpMestForSaleSDO();
                        result.ServiceReqs = serviceReqs;
                        List<long> serviceReqIds = serviceReqs.Select(s => s.ID).ToList();

                        string sqlReqMety = DAOWorker.SqlDAO.AddInClause(serviceReqIds, "SELECT * FROM HIS_SERVICE_REQ_METY WHERE %IN_CLAUSE%", "SERVICE_REQ_ID");
                        string sqlReqMaty = DAOWorker.SqlDAO.AddInClause(serviceReqIds, "SELECT * FROM HIS_SERVICE_REQ_MATY WHERE %IN_CLAUSE%", "SERVICE_REQ_ID");

                        result.ServiceReqMetys = DAOWorker.SqlDAO.GetSql<HIS_SERVICE_REQ_METY>(sqlReqMety);
                        result.ServiceReqMatys = DAOWorker.SqlDAO.GetSql<HIS_SERVICE_REQ_MATY>(sqlReqMaty);

                        if (IsNotNullOrEmpty(result.ServiceReqMetys) || IsNotNullOrEmpty(result.ServiceReqMatys))
                        {
                            string sqlExpMest = DAOWorker.SqlDAO.AddInClause(serviceReqIds, "SELECT * FROM V_HIS_EXP_MEST WHERE PRESCRIPTION_ID IS NOT NULL AND %IN_CLAUSE%", "PRESCRIPTION_ID");
                            result.ExpMests = DAOWorker.SqlDAO.GetSql<V_HIS_EXP_MEST>(sqlExpMest);

                            if (IsNotNullOrEmpty(result.ExpMests))
                            {
                                List<V_HIS_EXP_MEST> expMestReqs = result.ExpMests.Where(o => o.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST && !o.BILL_ID.HasValue).ToList();
                                if (IsNotNullOrEmpty(expMestReqs))
                                {
                                    List<long> expMestReqIds = expMestReqs.Select(s => s.ID).ToList();
                                    string sqlMedicine = DAOWorker.SqlDAO.AddInClause(expMestReqIds, "SELECT * FROM V_HIS_EXP_MEST_MEDICINE WHERE IS_DELETE = 0 AND %IN_CLAUSE%", "EXP_MEST_ID");
                                    string sqlMaterial = DAOWorker.SqlDAO.AddInClause(expMestReqIds, "SELECT * FROM V_HIS_EXP_MEST_MATERIAL WHERE IS_DELETE = 0 AND %IN_CLAUSE%", "EXP_MEST_ID");
                                    result.ViewMedicines = DAOWorker.SqlDAO.GetSql<V_HIS_EXP_MEST_MEDICINE>(sqlMedicine);
                                    result.ViewMaterials = DAOWorker.SqlDAO.GetSql<V_HIS_EXP_MEST_MATERIAL>(sqlMaterial);
                                    if (IsNotNullOrEmpty(result.ViewMedicines))
                                    {
                                        string sqlMediBean = DAOWorker.SqlDAO.AddInClause(result.ViewMedicines.Select(s => s.ID).ToList(), "SELECT * FROM HIS_MEDICINE_BEAN WHERE EXP_MEST_MEDICINE_ID IS NOT NULL AND %IN_CLAUSE%", "EXP_MEST_MEDICINE_ID");
                                        result.MedicineBeans = DAOWorker.SqlDAO.GetSql<HIS_MEDICINE_BEAN>(sqlMediBean);
                                    }
                                    if (IsNotNullOrEmpty(result.ViewMaterials))
                                    {
                                        string sqlMateBean = DAOWorker.SqlDAO.AddInClause(result.ViewMaterials.Select(s => s.ID).ToList(), "SELECT * FROM HIS_MATERIAL_BEAN WHERE EXP_MEST_MATERIAL_ID IS NOT NULL AND %IN_CLAUSE%", "EXP_MEST_MATERIAL_ID");
                                        result.MaterialBeans = DAOWorker.SqlDAO.GetSql<HIS_MATERIAL_BEAN>(sqlMateBean);
                                    }
                                }
                                else
                                {
                                    List<V_HIS_EXP_MEST> expMestDones = result.ExpMests.Where(p => p.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE
                                    || p.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE
                                    || p.BILL_ID.HasValue).ToList();
                                    if (IsNotNullOrEmpty(expMestDones) && expMestDones.Count == result.ExpMests.Count)
                                    {
                                        List<long> expMestDoneIds = expMestDones.Select(s => s.ID).ToList();
                                        string sqlMedicine = DAOWorker.SqlDAO.AddInClause(expMestDoneIds, "SELECT * FROM V_HIS_EXP_MEST_MEDICINE WHERE IS_DELETE = 0 AND %IN_CLAUSE%", "EXP_MEST_ID");
                                        string sqlMaterial = DAOWorker.SqlDAO.AddInClause(expMestDoneIds, "SELECT * FROM V_HIS_EXP_MEST_MATERIAL WHERE IS_DELETE = 0 AND %IN_CLAUSE%", "EXP_MEST_ID");
                                        result.ViewMedicines = DAOWorker.SqlDAO.GetSql<V_HIS_EXP_MEST_MEDICINE>(sqlMedicine);
                                        result.ViewMaterials = DAOWorker.SqlDAO.GetSql<V_HIS_EXP_MEST_MATERIAL>(sqlMaterial);
                                        if (IsNotNullOrEmpty(result.ViewMedicines))
                                        {
                                            string sqlMediBean = DAOWorker.SqlDAO.AddInClause(result.ViewMedicines.Select(s => s.ID).ToList(), "SELECT * FROM HIS_MEDICINE_BEAN WHERE EXP_MEST_MEDICINE_ID IS NOT NULL AND %IN_CLAUSE%", "EXP_MEST_MEDICINE_ID");
                                            result.MedicineBeans = DAOWorker.SqlDAO.GetSql<HIS_MEDICINE_BEAN>(sqlMediBean);
                                        }
                                        if (IsNotNullOrEmpty(result.ViewMaterials))
                                        {
                                            string sqlMateBean = DAOWorker.SqlDAO.AddInClause(result.ViewMaterials.Select(s => s.ID).ToList(), "SELECT * FROM HIS_MATERIAL_BEAN WHERE EXP_MEST_MATERIAL_ID IS NOT NULL AND %IN_CLAUSE%", "EXP_MEST_MATERIAL_ID");
                                            result.MaterialBeans = DAOWorker.SqlDAO.GetSql<HIS_MATERIAL_BEAN>(sqlMateBean);
                                        }
                                    }
                                    else if (IsNotNullOrEmpty(expMestDones))
                                    {
                                        List<long> expMestDoneIds = expMestDones.Select(s => s.ID).ToList();
                                        result.Medicines = new HisExpMestMedicineGet().GetByExpMestIds(expMestDoneIds);
                                        result.Materials = new HisExpMestMaterialGet().GetByExpMestIds(expMestDoneIds);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
                param.HasException = true;
            }
            return result;
        }
    }
}
