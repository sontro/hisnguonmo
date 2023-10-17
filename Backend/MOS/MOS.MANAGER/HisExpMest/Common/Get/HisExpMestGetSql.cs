using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOS.MANAGER.HisExpMest.Common.Get
{
    partial class HisExpMestGet : GetBase
    {
        internal List<D_HIS_EXP_MEST_DETAIL_1> GetExpMestDetail1(DHisExpMestDetail1Filter filter)
        {
            try
            {
                string sql = "SELECT * FROM D_HIS_EXP_MEST_DETAIL_1 WHERE 1 = 1 ";

                StringBuilder sb = new StringBuilder().Append(sql);

                if (filter.EXP_MEST_TYPE_ID.HasValue)
                {
                    sb.Append("AND EXP_MEST_TYPE_ID = ").Append(filter.EXP_MEST_TYPE_ID.Value);
                }
                if (filter.BILL_ID.HasValue)
                {
                    sb.Append("AND BILL_ID IS NOT NULL AND BILL_ID = ").Append(filter.BILL_ID.Value);
                }
                if (!string.IsNullOrWhiteSpace(filter.EXP_MEST_CODE__EXACT))
                {
                    sb.Append("AND EXP_MEST_CODE = ").Append(filter.EXP_MEST_CODE__EXACT);
                }
                if (filter.MEDI_STOCK_ID.HasValue)
                {
                    sb.Append("AND MEDI_STOCK_ID = ").Append(filter.MEDI_STOCK_ID.Value);
                }
                if (!string.IsNullOrWhiteSpace(filter.TDL_TREATMENT_CODE__EXACT))
                {
                    sb.Append("AND TDL_TREATMENT_CODE = ").Append(filter.TDL_TREATMENT_CODE__EXACT);
                }
                if (filter.DEBT_ID.HasValue)
                {
                    sb.Append("AND DEBT_ID IS NOT NULL AND DEBT_ID = ").Append(filter.DEBT_ID.Value);
                }
                if (filter.HAS_BILL.HasValue && filter.HAS_BILL.Value)
                {
                    sb.Append("AND BILL_ID IS NOT NULL ");
                }
                if (filter.HAS_BILL.HasValue && !filter.HAS_BILL.Value)
                {
                    sb.Append("AND BILL_ID IS NULL ");
                }
                if (filter.HAS_DEBT.HasValue && filter.HAS_DEBT.Value)
                {
                    sb.Append("AND DEBT_ID IS NOT NULL ");
                }
                if (filter.HAS_DEBT.HasValue && !filter.HAS_DEBT.Value)
                {
                    sb.Append("AND DEBT_ID IS NULL ");
                }
                if (filter.EXP_MEST_IDs != null)
                {
                    string ids = "-1";
                    if (filter.EXP_MEST_IDs.Count > 0)
                    {
                        //Do thuc te, d/s EXP_MEST_IDs la it, nen co the tao cau query luon nhu the nay
                        ids = string.Join(",", filter.EXP_MEST_IDs);
                    }
                    sb.Append(string.Format("AND EXP_MEST_ID IN ({0}) ", ids));
                }

                return DAOWorker.SqlDAO.GetSql<D_HIS_EXP_MEST_DETAIL_1>(sb.ToString());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<ExpMestTutorialSDO> GetExpMestTutorial(HisExpMestTutorialFilter filter)
        {
            try
            {
                string sql = "SELECT MT.MEDICINE_TYPE_NAME AS ItemName, 1 AS IsMedicine, M.TUTORIAL AS Tutorial "
                            + " FROM HIS_EXP_MEST_MEDICINE M "
                            + " JOIN HIS_EXP_MEST EXP ON EXP.ID = M.EXP_MEST_ID "
                            + " JOIN HIS_MEDICINE_TYPE MT ON MT.ID = M.TDL_MEDICINE_TYPE_ID "
                            + " WHERE M.TDL_TREATMENT_ID = {0} ";
                sql = string.Format(sql, filter.TREATMENT_ID);

                StringBuilder sb = new StringBuilder().Append(sql);

                if (filter.EXP_MEST_TYPE_IDs != null)
                {
                    string ids = "-1";
                    if (filter.EXP_MEST_TYPE_IDs.Count > 0)
                    {
                        //Do thuc te, d/s EXP_MEST_TYPE_IDs la it, nen co the tao cau query luon nhu the nay
                        ids = string.Join(",", filter.EXP_MEST_TYPE_IDs);
                    }
                    sb.Append(string.Format(" AND EXP.EXP_MEST_TYPE_ID IN ({0}) ", ids));
                }
                if (filter.MEDICINE_USE_FORM_IDs != null)
                {
                    string ids = "-1";
                    if (filter.MEDICINE_USE_FORM_IDs.Count > 0)
                    {
                        //Do thuc te, d/s MEDICINE_USE_FORM_IDs la it, nen co the tao cau query luon nhu the nay
                        ids = string.Join(",", filter.MEDICINE_USE_FORM_IDs);
                    }
                    sb.Append(string.Format(" AND MT.MEDICINE_USE_FORM_ID IN ({0}) ", ids));
                }
                if (filter.INCLUDE_MATERIAL)
                {
                    string sqlMaterial = "UNION all "
                            + " SELECT MT.MATERIAL_TYPE_NAME AS ItemName, 0 AS IsMedicine, M.TUTORIAL AS Tutorial "
                            + " FROM HIS_EXP_MEST_MATERIAL M "
                            + " JOIN HIS_EXP_MEST EXP ON EXP.ID = M.EXP_MEST_ID "
                            + " JOIN HIS_MATERIAL_TYPE MT ON MT.ID = M.TDL_MATERIAL_TYPE_ID "
                            + " WHERE M.TDL_TREATMENT_ID = {0} ";
                    sqlMaterial = string.Format(sqlMaterial, filter.TREATMENT_ID);

                    StringBuilder sbMaterial = new StringBuilder().Append(sqlMaterial);

                    if (filter.EXP_MEST_TYPE_IDs != null)
                    {
                        string ids = "-1";
                        if (filter.EXP_MEST_TYPE_IDs.Count > 0)
                        {
                            //Do thuc te, d/s EXP_MEST_TYPE_IDs la it, nen co the tao cau query luon nhu the nay
                            ids = string.Join(",", filter.EXP_MEST_TYPE_IDs);
                        }
                        sbMaterial.Append(string.Format(" AND EXP.EXP_MEST_TYPE_ID IN ({0}) ", ids));
                    }
                    sb.Append(sbMaterial);
                }

                return DAOWorker.SqlDAO.GetSql<ExpMestTutorialSDO>(sb.ToString());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HisExpMestBcsMoreInfoSDO GetBcsMoreInfo(HisExpMestBcsMoreInfoFilter filter)
        {
            HisExpMestBcsMoreInfoSDO result = null;
            try
            {
                result = new HisExpMestBcsMoreInfoSDO();
                result.ExpMestId = filter.BCS_EXP_MEST_ID;
                HIS_EXP_MEST exp = DAOWorker.SqlDAO.GetSqlSingle<HIS_EXP_MEST>("SELECT * FROM HIS_EXP_MEST WHERE ID = :param1", filter.BCS_EXP_MEST_ID);

                if (exp == null
                    || !(exp.BCS_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_MEDI_STOCK.CABINET_MANAGE_OPTION__PRES || exp.BCS_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_MEDI_STOCK.CABINET_MANAGE_OPTION__PRES_DETAIL))
                {
                    LogSystem.Warn("ExpMestId khong chinh xac hoac khong phai bu theo don, theo chi tiet don: " + filter.BCS_EXP_MEST_ID);
                    return result;
                }

                if (exp.BCS_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_MEDI_STOCK.CABINET_MANAGE_OPTION__PRES)
                {
                    List<ExpData> datas = DAOWorker.SqlDAO.GetSql<ExpData>("SELECT ID, EXP_MEST_CODE, EXP_MEST_TYPE_ID, TDL_TREATMENT_CODE FROM HIS_EXP_MEST WHERE XBTT_EXP_MEST_ID = :param1", filter.BCS_EXP_MEST_ID);

                    if (IsNotNullOrEmpty(datas))
                    {
                        result.ExpMestCodes = datas.Select(s => s.EXP_MEST_CODE).Distinct().ToList();
                        result.TreatmentCodes = datas.Where(o => !String.IsNullOrWhiteSpace(o.TDL_TREATMENT_CODE)).Select(s => s.TDL_TREATMENT_CODE).Distinct().ToList();
                        result.PrescriptionCount = datas.Where(o => o.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DTT).Select(s => s.EXP_MEST_CODE).Distinct().Count();
                    }
                }
                else if (exp.BCS_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_MEDI_STOCK.CABINET_MANAGE_OPTION__PRES_DETAIL)
                {
                    StringBuilder sb = new StringBuilder();

                    //HisBcsMatyReqReq
                    sb.Append("SELECT A.ID, B.EXP_MEST_ID, C.EXP_MEST_CODE, C.EXP_MEST_TYPE_ID ")
                        .Append("FROM HIS_BCS_MATY_REQ_REQ A ")
                        .Append("JOIN HIS_EXP_MEST_MATY_REQ B ON A.PRE_EXP_MEST_MATY_REQ_ID = B.ID ")
                        .Append("JOIN HIS_EXP_MEST C ON B.EXP_MEST_ID = C.ID ")
                        .Append("WHERE A.TDL_XBTT_EXP_MEST_ID = :param1 ");

                    //HisBcsMetyReqReq
                    sb.Append("UNION ")
                        .Append("SELECT A.ID, B.EXP_MEST_ID, C.EXP_MEST_CODE, C.EXP_MEST_TYPE_ID ")
                        .Append("FROM HIS_BCS_METY_REQ_REQ A ")
                        .Append("JOIN HIS_EXP_MEST_METY_REQ B ON A.PRE_EXP_MEST_METY_REQ_ID = B.ID ")
                        .Append("JOIN HIS_EXP_MEST C ON B.EXP_MEST_ID = C.ID ")
                        .Append("WHERE A.TDL_XBTT_EXP_MEST_ID = :param1 ");

                    //HisBcsMatyReqDt
                    sb.Append("UNION ")
                        .Append("SELECT A.ID, B.EXP_MEST_ID, C.EXP_MEST_CODE, C.EXP_MEST_TYPE_ID ")
                        .Append("FROM HIS_BCS_MATY_REQ_DT A ")
                        .Append("JOIN HIS_EXP_MEST_MATERIAL B ON A.EXP_MEST_MATERIAL_ID = B.ID ")
                        .Append("JOIN HIS_EXP_MEST C ON B.EXP_MEST_ID = C.ID ")
                        .Append("WHERE B.IS_DELETE = 0 AND A.TDL_XBTT_EXP_MEST_ID = :param1 ");

                    //HisBcsMetyReqDt
                    sb.Append("UNION ")
                        .Append("SELECT A.ID, B.EXP_MEST_ID, C.EXP_MEST_CODE, C.EXP_MEST_TYPE_ID ")
                        .Append("FROM HIS_BCS_METY_REQ_DT A ")
                        .Append("JOIN HIS_EXP_MEST_MEDICINE B ON A.EXP_MEST_MEDICINE_ID = B.ID ")
                        .Append("JOIN HIS_EXP_MEST C ON B.EXP_MEST_ID = C.ID ")
                        .Append("WHERE B.IS_DELETE = 0 AND A.TDL_XBTT_EXP_MEST_ID = :param1");

                    string sql = sb.ToString();

                    List<ExpDetailData> datas = DAOWorker.SqlDAO.GetSql<ExpDetailData>(sql, filter.BCS_EXP_MEST_ID);

                    if (IsNotNullOrEmpty(datas))
                    {
                        result.ExpMestCodes = datas.Select(s => s.EXP_MEST_CODE).Distinct().ToList();
                        result.PrescriptionCount = datas.Where(o => o.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DTT).Select(s => s.EXP_MEST_CODE).Distinct().Count();
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

        internal List<HisExpMestGroupByTreatmentSDO> GetExpMestGroupByTreatment(HisExpMestGroupByTreatmentFilter filter)
        {
            try
            {
                string sql = "SELECT TREA.ID AS TREATMENT_ID, TREA.TREATMENT_CODE, TREA.TDL_PATIENT_CODE, "
                            + "     TREA.TDL_PATIENT_NAME, TREA.TDL_PATIENT_DOB, TREA.TDL_PATIENT_IS_HAS_NOT_DAY_DOB, "
                            + "     TREA.TDL_PATIENT_GENDER_NAME,TREA.OUT_TIME, "
                            + "     LISTAGG(EXP.ID, ',') WITHIN GROUP (ORDER BY EXP.ID) AS EXP_MEST_IDS "
                            + "     FROM HIS_EXP_MEST EXP "
                            + "     JOIN HIS_TREATMENT TREA ON TREA.ID = EXP.TDL_TREATMENT_ID "
                            + " WHERE EXP.AGGR_EXP_MEST_ID IS NULL "; //ko lay ra cac phieu xuat da duoc tong hop
                if (filter.EXP_MEST_TYPE_ID.HasValue)
                {
                    sql += string.Format(" AND EXP.EXP_MEST_TYPE_ID = {0} ", filter.EXP_MEST_TYPE_ID.Value);
                }
                if (filter.EXP_MEST_TYPE_IDs != null)
                {
                    string idStr = string.Join(",", filter.EXP_MEST_TYPE_IDs);
                    sql += string.Format(" AND EXP.EXP_MEST_TYPE_ID IN ({0}) ", idStr);
                }
                if (filter.MEDI_STOCK_ID.HasValue)
                {
                    sql += string.Format(" AND EXP.MEDI_STOCK_ID = {0} ", filter.MEDI_STOCK_ID.Value);
                }
                if (filter.EXP_MEST_STT_ID.HasValue)
                {
                    sql += string.Format(" AND EXP.EXP_MEST_STT_ID = {0} ", filter.EXP_MEST_STT_ID.Value);
                }
                if (!string.IsNullOrWhiteSpace(filter.EXP_MEST_CODE__EXACT))
                {
                    sql += string.Format(" AND EXP.EXP_MEST_CODE = '{0}' ", filter.EXP_MEST_CODE__EXACT);
                }
                if (filter.IS_NOT_TAKEN.HasValue && filter.IS_NOT_TAKEN.Value)
                {
                    sql += string.Format(" AND EXP.IS_NOT_TAKEN = {0} ", Constant.IS_TRUE);
                }
                if (filter.IS_NOT_TAKEN.HasValue && !filter.IS_NOT_TAKEN.Value)
                {
                    sql += string.Format(" AND (EXP.IS_NOT_TAKEN IS NULL OR EXP.IS_NOT_TAKEN <> {0}) ", Constant.IS_TRUE);
                }
                if (filter.OUT_TIME_FROM.HasValue)
                {
                    sql += string.Format(" AND TREA.OUT_TIME >= {0} ", filter.OUT_TIME_FROM.Value);
                }
                if (!string.IsNullOrWhiteSpace(filter.TREATMENT_CODE__EXACT))
                {
                    sql += string.Format(" AND TREA.TREATMENT_CODE = '{0}' ", filter.TREATMENT_CODE__EXACT);
                }
                if (!string.IsNullOrWhiteSpace(filter.PATIENT_CODE__EXACT))
                {
                    sql += string.Format(" AND TREA.TDL_PATIENT_CODE = '{0}' ", filter.PATIENT_CODE__EXACT);
                }
                if (filter.OUT_TIME_TO.HasValue)
                {
                    sql += string.Format(" AND TREA.OUT_TIME <= {0} ", filter.OUT_TIME_TO.Value);
                }
                if (filter.TREATMENT_IS_PAUSE.HasValue && filter.TREATMENT_IS_PAUSE.Value)
                {
                    sql += string.Format(" AND TREA.IS_PAUSE = {0} ", Constant.IS_TRUE);
                }
                if (filter.TREATMENT_IS_PAUSE.HasValue && !filter.TREATMENT_IS_PAUSE.Value)
                {
                    sql += string.Format(" AND (TREA.IS_PAUSE IS NULL OR TREA.IS_PAUSE <> {0}) ", Constant.IS_TRUE);
                }
                if (filter.TREATMENT_IS_ACTIVE.HasValue && filter.TREATMENT_IS_ACTIVE.Value)
                {
                    sql += string.Format(" AND TREA.IS_ACTIVE = {0} ", Constant.IS_TRUE);
                }
                if (filter.TREATMENT_IS_ACTIVE.HasValue && !filter.TREATMENT_IS_ACTIVE.Value)
                {
                    sql += string.Format(" AND (TREA.IS_ACTIVE IS NULL OR TREA.IS_ACTIVE <> {0}) ", Constant.IS_TRUE);
                }
                if (!string.IsNullOrWhiteSpace(filter.KEYWORD))
                {
                    string keyWord = filter.KEYWORD.ToLower();
                    sql += string.Format(" AND (lower(TREA.TREATMENT_CODE) LIKE '%{0}%' OR lower(TREA.TDL_PATIENT_NAME) LIKE '%{1}%' OR lower(TREA.TDL_PATIENT_CODE) LIKE '%{2}%')", keyWord, keyWord, keyWord);
                }
                sql += " GROUP BY TREA.ID, TREA.TREATMENT_CODE, TREA.TDL_PATIENT_CODE, "
                    + "     TREA.TDL_PATIENT_NAME, TREA.TDL_PATIENT_DOB, TREA.TDL_PATIENT_IS_HAS_NOT_DAY_DOB, "
                    + "     TREA.TDL_PATIENT_GENDER_NAME,TREA.OUT_TIME";


                return DAOWorker.SqlDAO.GetSql<HisExpMestGroupByTreatmentSDO>(sql);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }

    public class ExpData
    {
        public long ID { get; set; }
        public string EXP_MEST_CODE { get; set; }
        public long EXP_MEST_TYPE_ID { get; set; }
        public string TDL_TREATMENT_CODE { get; set; }
    }

    public class ExpDetailData
    {
        public long ID { get; set; }
        public long EXP_MEST_ID { get; set; }
        public string EXP_MEST_CODE { get; set; }
        public long EXP_MEST_TYPE_ID { get; set; }
    }
}
