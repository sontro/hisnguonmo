using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOS.MANAGER.HisMediStock
{
    partial class HisMediStockGet : GetBase
    {
        internal List<D_HIS_MEDI_STOCK_1> GetDHisMediStock1(DHisMediStock1Filter filter)
        {
            try
            {
                StringBuilder sqlBuilder = new StringBuilder();
                sqlBuilder.Append("SELECT A.ID,A.IS_ACTIVE,A.MEDICINE_TYPE_CODE,A.MEDICINE_TYPE_NAME,A.SERVICE_ID,A.MANUFACTURER_ID,A.ALERT_MIN_IN_STOCK,A.MEDICINE_USE_FORM_ID,A.TUTORIAL,A.NATIONAL_NAME,A.CONCENTRA,A.USE_ON_DAY,A.IS_STAR_MARK,A.ACTIVE_INGR_BHYT_CODE,A.ACTIVE_INGR_BHYT_NAME,A.ALERT_MAX_IN_TREATMENT,A.IS_STENT,A.ALERT_MAX_IN_PRESCRIPTION,A.IS_VACCINE,A.RANK,A.IMP_PRICE,A.IMP_VAT_RATIO,A.IS_REUSABLE,A.LAST_EXP_PRICE,A.LAST_EXP_VAT_RATIO,A.CONTRAINDICATION,A.MATERIAL_TYPE_MAP_ID,A.PARENT_ID,A.MANUFACTURER_CODE,A.MANUFACTURER_NAME,A.MEDI_STOCK_CODE,A.MEDI_STOCK_NAME,A.AMOUNT,A.MEDI_STOCK_ID,A.SERVICE_TYPE_ID,A.IS_CHEMICAL_SUBSTANCE,A.IS_AUTO_EXPEND,A.PARENT_CODE,A.PARENT_NAME, ");
                sqlBuilder.Append("SERV.SERVICE_UNIT_ID,SERV.HEIN_SERVICE_TYPE_ID,SERV.IS_OUT_PARENT_FEE,SERV.GENDER_ID,SERV.OTHER_PAY_SOURCE_ID,SERV.OTHER_PAY_SOURCE_ICDS, ");
                sqlBuilder.Append("SEUN.SERVICE_UNIT_CODE,SEUN.SERVICE_UNIT_NAME,SEUN.CONVERT_RATIO,NULL AS MEDICINE_REGISTER_NUMBER, ");
                sqlBuilder.Append("CSEU.SERVICE_UNIT_CODE AS CONVERT_UNIT_CODE,CSEU.SERVICE_UNIT_NAME AS CONVERT_UNIT_NAME ");
                sqlBuilder.Append(" FROM ");
                sqlBuilder.Append(" ( ");
                sqlBuilder.Append("SELECT METY.ID,METY.IS_ACTIVE,METY.MEDICINE_TYPE_CODE,METY.MEDICINE_TYPE_NAME,METY.SERVICE_ID,METY.MANUFACTURER_ID,METY.ALERT_MIN_IN_STOCK,METY.MEDICINE_USE_FORM_ID,METY.TUTORIAL,METY.NATIONAL_NAME,METY.CONCENTRA,METY.USE_ON_DAY,METY.IS_STAR_MARK,METY.ACTIVE_INGR_BHYT_CODE,METY.ACTIVE_INGR_BHYT_NAME,METY.ALERT_MAX_IN_TREATMENT,NULL AS IS_STENT,METY.ALERT_MAX_IN_PRESCRIPTION,METY.IS_VACCINE,METY.RANK,METY.IMP_PRICE,METY.IMP_VAT_RATIO,NULL AS IS_REUSABLE,METY.LAST_EXP_PRICE,METY.LAST_EXP_VAT_RATIO,METY.CONTRAINDICATION, NULL AS MATERIAL_TYPE_MAP_ID,METY.PARENT_ID, ");
                sqlBuilder.Append("MANU.MANUFACTURER_CODE,MANU.MANUFACTURER_NAME, ");
                sqlBuilder.Append("TEMP.MEDI_STOCK_CODE,TEMP.MEDI_STOCK_NAME,TEMP.AMOUNT,TEMP.MEDI_STOCK_ID, ");
                sqlBuilder.Append("6 AS SERVICE_TYPE_ID,NULL AS IS_CHEMICAL_SUBSTANCE,METY.IS_AUTO_EXPEND, ");
                sqlBuilder.Append("PMTY.MEDICINE_TYPE_CODE AS PARENT_CODE,PMTY.MEDICINE_TYPE_NAME AS PARENT_NAME ");
                sqlBuilder.Append(" FROM HIS_MEDICINE_TYPE METY ");
                sqlBuilder.Append(" JOIN ");
                sqlBuilder.Append("(SELECT SUM(MEBE.AMOUNT) AS AMOUNT,MEBE.MEDI_STOCK_ID,MEBE.TDL_MEDICINE_TYPE_ID AS MEDICINE_TYPE_ID,MEST.MEDI_STOCK_CODE,MEST.MEDI_STOCK_NAME ");
                sqlBuilder.Append(" FROM HIS_MEDICINE_BEAN MEBE ");
                sqlBuilder.Append(" JOIN HIS_MEDI_STOCK MEST ON MEBE.MEDI_STOCK_ID = MEST.ID ");
                sqlBuilder.Append(" WHERE MEBE.IS_ACTIVE = 1 AND MEBE.TDL_MEDICINE_IS_ACTIVE =1 {0} ");  //de chen dieu kien lien quan den han su dung
                sqlBuilder.Append(" GROUP BY MEBE.MEDI_STOCK_ID,MEBE.TDL_MEDICINE_TYPE_ID,MEST.MEDI_STOCK_CODE,MEST.MEDI_STOCK_NAME) TEMP ");
                sqlBuilder.Append(" ON METY.ID = TEMP.MEDICINE_TYPE_ID ");
                sqlBuilder.Append(" LEFT JOIN HIS_MANUFACTURER MANU ON METY.MANUFACTURER_ID = MANU.ID ");
                sqlBuilder.Append(" LEFT JOIN HIS_MEDICINE_TYPE PMTY ON METY.PARENT_ID = PMTY.ID ");
                sqlBuilder.Append(" WHERE METY.IS_ACTIVE = 1 ");
                sqlBuilder.Append(" UNION ALL ");
                sqlBuilder.Append(" SELECT MATY.ID,MATY.IS_ACTIVE,MATY.MATERIAL_TYPE_CODE,MATY.MATERIAL_TYPE_NAME,MATY.SERVICE_ID,MATY.MANUFACTURER_ID,MATY.ALERT_MIN_IN_STOCK,NULL AS MEDICINE_USE_FORM_ID,NULL AS TUTORIAL,MATY.NATIONAL_NAME,MATY.CONCENTRA,NULL AS USE_ON_DAY,NULL AS IS_STAR_MARK,NULL AS ACTIVE_INGR_BHYT_CODE,NULL AS ACTIVE_INGR_BHYT_NAME,NULL AS ALERT_MAX_IN_TREATMENT,MATY.IS_STENT,MATY.ALERT_MAX_IN_PRESCRIPTION,NULL AS IS_VACCINE,NULL AS RANK,MATY.IMP_PRICE,MATY.IMP_VAT_RATIO,MATY.IS_REUSABLE,MATY.LAST_EXP_PRICE,MATY.LAST_EXP_VAT_RATIO,NULL AS CONTRAINDICATION,MATY.MATERIAL_TYPE_MAP_ID,MATY.PARENT_ID, ");
                sqlBuilder.Append("MANU.MANUFACTURER_CODE,MANU.MANUFACTURER_NAME, ");
                sqlBuilder.Append("TEMP.MEDI_STOCK_CODE,TEMP.MEDI_STOCK_NAME,TEMP.AMOUNT,TEMP.MEDI_STOCK_ID, ");
                sqlBuilder.Append("7 AS SERVICE_TYPE_ID, MATY.IS_CHEMICAL_SUBSTANCE,MATY.IS_AUTO_EXPEND, ");
                sqlBuilder.Append("PMTY.MATERIAL_TYPE_CODE AS PARENT_CODE,PMTY.MATERIAL_TYPE_NAME AS PARENT_NAME ");
                sqlBuilder.Append(" FROM HIS_MATERIAL_TYPE MATY ");
                sqlBuilder.Append(" JOIN ");
                sqlBuilder.Append(" (SELECT SUM(MEBE.AMOUNT) AS AMOUNT,MEBE.MEDI_STOCK_ID,MEBE.TDL_MATERIAL_TYPE_ID AS MATERIAL_TYPE_ID,MEST.MEDI_STOCK_CODE,MEST.MEDI_STOCK_NAME ");
                sqlBuilder.Append(" FROM HIS_MATERIAL_BEAN MEBE ");
                sqlBuilder.Append(" JOIN HIS_MEDI_STOCK MEST ON MEBE.MEDI_STOCK_ID = MEST.ID ");
                sqlBuilder.Append(" WHERE MEBE.IS_ACTIVE = 1 AND MEBE.TDL_MATERIAL_IS_ACTIVE = 1 {1} ");  //de chen dieu kien lien quan den han su dung
                sqlBuilder.Append(" GROUP BY MEBE.MEDI_STOCK_ID,MEBE.TDL_MATERIAL_TYPE_ID,MEST.MEDI_STOCK_CODE,MEST.MEDI_STOCK_NAME) TEMP ");
                sqlBuilder.Append(" ON MATY.ID = TEMP.MATERIAL_TYPE_ID ");
                sqlBuilder.Append(" LEFT JOIN HIS_MANUFACTURER MANU ON MATY.MANUFACTURER_ID = MANU.ID ");
                sqlBuilder.Append(" LEFT JOIN HIS_MATERIAL_TYPE PMTY ON MATY.PARENT_ID = PMTY.ID ");
                sqlBuilder.Append(" WHERE MATY.IS_ACTIVE = 1 ");
                sqlBuilder.Append(" ) A ");
                sqlBuilder.Append(" JOIN HIS_SERVICE SERV ON A.SERVICE_ID = SERV.ID ");
                sqlBuilder.Append(" JOIN HIS_SERVICE_UNIT SEUN ON SERV.SERVICE_UNIT_ID = SEUN.ID ");
                sqlBuilder.Append(" LEFT JOIN HIS_SERVICE_UNIT CSEU ON CSEU.ID = SEUN.CONVERT_ID ");
                sqlBuilder.Append(" WHERE 1 = 1  "); // bat dau dieu kien loc
                
                if (filter.IS_VACCINE.HasValue && filter.IS_VACCINE.Value)
                {
                    sqlBuilder.Append("AND IS_VACCINE IS NOT NULL AND IS_VACCINE = 1 ");
                }
                if (filter.IS_VACCINE.HasValue && !filter.IS_VACCINE.Value)
                {
                    sqlBuilder.Append("AND (IS_VACCINE IS NULL OR IS_VACCINE <> 1) ");
                }
                if (filter.IS_REUSABLE.HasValue && filter.IS_REUSABLE.Value)
                {
                    sqlBuilder.Append("AND IS_REUSABLE IS NOT NULL AND IS_REUSABLE = 1 ");
                }
                if (filter.IS_REUSABLE.HasValue && !filter.IS_REUSABLE.Value)
                {
                    sqlBuilder.Append("AND (IS_REUSABLE IS NULL OR IS_REUSABLE <> 1) ");
                }
                sqlBuilder.Append(" AND %IN_CLAUSE% ");

                string sExpiredDateMedicine = "";
                string sExpiredDateMaterial = "";
                if (filter.EXPIRED_DATE__NULL_OR_GREATER_THAN_OR_EQUAL.HasValue)
                {
                    sExpiredDateMedicine = string.Format(" AND (MEBE.TDL_MEDICINE_EXPIRED_DATE IS NULL OR MEBE.TDL_MEDICINE_EXPIRED_DATE >= {0})", filter.EXPIRED_DATE__NULL_OR_GREATER_THAN_OR_EQUAL.Value);
                    sExpiredDateMaterial = string.Format(" AND (MEBE.TDL_MATERIAL_EXPIRED_DATE IS NULL OR MEBE.TDL_MATERIAL_EXPIRED_DATE >= {0})", filter.EXPIRED_DATE__NULL_OR_GREATER_THAN_OR_EQUAL.Value);
                }

                string sql = sqlBuilder.ToString();
                sql = string.Format(sql, sExpiredDateMedicine, sExpiredDateMaterial);
                string query = this.AddInClause(filter.MEDI_STOCK_IDs, sql, "MEDI_STOCK_ID");
                return DAOWorker.SqlDAO.GetSql<D_HIS_MEDI_STOCK_1>(query);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<D_HIS_MEDI_STOCK_2> GetDHisMediStock2(DHisMediStock2Filter filter)
        {
            try
            {
                StringBuilder sb = new StringBuilder().Append("SELECT * FROM D_HIS_MEDI_STOCK_2 WHERE ");
                if (filter.IS_VACCINE.HasValue && filter.IS_VACCINE.Value)
                {
                    sb.Append("IS_VACCINE IS NOT NULL AND IS_VACCINE = 1 AND ");
                }
                if (filter.IS_VACCINE.HasValue && !filter.IS_VACCINE.Value)
                {
                    sb.Append("(IS_VACCINE IS NULL OR IS_VACCINE <> 1) AND ");
                }
                if (filter.IS_REUSABLE.HasValue && filter.IS_REUSABLE.Value)
                {
                    sb.Append("IS_REUSABLE IS NOT NULL AND IS_REUSABLE = 1 AND ");
                }
                if (filter.IS_REUSABLE.HasValue && !filter.IS_REUSABLE.Value)
                {
                    sb.Append("(IS_REUSABLE IS NULL OR IS_REUSABLE <> 1) AND ");
                }
                if (filter.EXPIRED_DATE__NULL_OR_GREATER_THAN_OR_EQUAL.HasValue)
                {
                    sb.Append("(EXPIRED_DATE IS NULL OR (EXPIRED_DATE IS NOT NULL AND EXPIRED_DATE >= ").Append(filter.EXPIRED_DATE__NULL_OR_GREATER_THAN_OR_EQUAL.Value).Append(")) AND ");
                }

                sb.Append(" %IN_CLAUSE% ");
                string query = this.AddInClause(filter.MEDI_STOCK_IDs, sb.ToString(), "MEDI_STOCK_ID");
                return DAOWorker.SqlDAO.GetSql<D_HIS_MEDI_STOCK_2>(query);
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
