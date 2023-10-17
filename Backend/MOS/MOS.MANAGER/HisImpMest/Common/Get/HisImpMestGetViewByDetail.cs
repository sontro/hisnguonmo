using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Common.Get
{
    internal class HisImpMestGetViewByDetail : GetBase
    {
        internal HisImpMestGetViewByDetail()
            : base()
        {

        }

        internal HisImpMestGetViewByDetail(CommonParam param)
            : base(param)
        {

        }

        internal List<V_HIS_IMP_MEST> GetViewByDetail(HisImpMestViewDetailFilter filter)
        {
            List<V_HIS_IMP_MEST> result = null;
            try
            {
                if (!String.IsNullOrWhiteSpace(filter.MEDICINE_TYPE_CODE__EXACT))
                {
                    string sqlQuery = new StringBuilder().Append("SELECT * FROM V_HIS_IMP_MEST EXME ")
                        .Append("JOIN HIS_IMP_MEST_MEDICINE IMME ON EXME.ID = IMME.IMP_MEST_ID ")
                        .Append("JOIN HIS_MEDICINE MEDI ON IMME.MEDICINE_ID = MEDI.ID ")
                        .Append("JOIN HIS_MEDICINE_TYPE METY ON MEDI.MEDICINE_TYPE_ID = METY.ID ")
                        .Append("WHERE METY.MEDICINE_TYPE_CODE = '").Append(filter.MEDICINE_TYPE_CODE__EXACT).Append("'").ToString();

                    if (filter.IMP_MEST_STT_ID.HasValue)
                    {
                        sqlQuery = new StringBuilder().Append(sqlQuery)
                            .Append(" AND EXME.IMP_MEST_STT_ID = ").Append(filter.IMP_MEST_STT_ID.Value).ToString();
                    }
                    if (filter.IMP_MEST_TYPE_ID.HasValue)
                    {
                        sqlQuery = new StringBuilder().Append(sqlQuery)
                            .Append(" AND EXME.IMP_MEST_TYPE_ID = ").Append(filter.IMP_MEST_TYPE_ID.Value).ToString();
                    }
                    if (filter.MEDI_STOCK_ID.HasValue)
                    {
                        sqlQuery = new StringBuilder().Append(sqlQuery)
                            .Append(" AND EXME.MEDI_STOCK_ID = ").Append(filter.MEDI_STOCK_ID.Value).ToString();
                    }
                    if (filter.MEDI_STOCK_PERIOD_ID.HasValue)
                    {
                        sqlQuery = new StringBuilder().Append(sqlQuery)
                            .Append(" AND EXME.MEDI_STOCK_PERIOD_ID = ").Append(filter.MEDI_STOCK_PERIOD_ID.Value).ToString();
                    }
                    if (filter.REQ_DEPARTMENT_ID.HasValue)
                    {
                        sqlQuery = new StringBuilder().Append(sqlQuery)
                            .Append(" AND EXME.REQ_DEPARTMENT_ID = ").Append(filter.REQ_DEPARTMENT_ID.Value).ToString();
                    }
                    if (filter.REQ_ROOM_ID.HasValue)
                    {
                        sqlQuery = new StringBuilder().Append(sqlQuery)
                            .Append(" AND EXME.REQ_ROOM_ID = ").Append(filter.REQ_ROOM_ID.Value).ToString();
                    }
                    if (!String.IsNullOrWhiteSpace(filter.TDL_PATIENT_CODE__EXACT))
                    {
                        sqlQuery = new StringBuilder().Append(sqlQuery)
                            .Append(" AND EXME.TDL_PATIENT_CODE = '").Append(filter.TDL_PATIENT_CODE__EXACT).Append("'").ToString();
                    }
                    if (!String.IsNullOrWhiteSpace(filter.TDL_TREATMENT_CODE__EXACT))
                    {
                        sqlQuery = new StringBuilder().Append(sqlQuery)
                            .Append(" AND EXME.TDL_TREATMENT_CODE = '").Append(filter.TDL_TREATMENT_CODE__EXACT).Append("'").ToString();
                    }
                    if (!String.IsNullOrWhiteSpace(filter.DOCUMENT_NUMBER__EXACT))
                    {
                        sqlQuery = new StringBuilder().Append(sqlQuery)
                            .Append(" AND EXME.DOCUMENT_NUMBER = '").Append(filter.DOCUMENT_NUMBER__EXACT).Append("'").ToString();
                    }
                    if (!String.IsNullOrWhiteSpace(filter.IMP_MEST_CODE__EXACT))
                    {
                        sqlQuery = new StringBuilder().Append(sqlQuery)
                            .Append(" AND EXME.IMP_MEST_CODE = '").Append(filter.IMP_MEST_CODE__EXACT).Append("'").ToString();
                    }
                    if (!String.IsNullOrWhiteSpace(filter.ORDER_FIELD))
                    {
                        sqlQuery = new StringBuilder().Append(sqlQuery)
                            .Append(" ORDER BY EXME.").Append(filter.ORDER_FIELD).ToString();

                        if (!String.IsNullOrWhiteSpace(filter.ORDER_DIRECTION) && (filter.ORDER_DIRECTION.ToLower() == "desc" || filter.ORDER_DIRECTION.ToLower() == "asc"))
                        {
                            sqlQuery = new StringBuilder().Append(sqlQuery)
                            .Append(" ").Append(filter.ORDER_DIRECTION).ToString();
                        }
                    }

                    List<V_HIS_IMP_MEST> listImps = DAOWorker.SqlDAO.GetSql<V_HIS_IMP_MEST>(sqlQuery);

                    if (IsNotNullOrEmpty(listImps) && !string.IsNullOrWhiteSpace(filter.ORDER_FIELD) && !string.IsNullOrWhiteSpace(filter.ORDER_DIRECTION))
                    {
                        if (!param.Start.HasValue || !param.Limit.HasValue)
                        {
                            result = listImps;
                        }
                        else
                        {
                            param.Count = listImps.Count();
                            if (param.Count <= param.Limit.Value && param.Start.Value == 0)
                            {
                                result = listImps;
                            }
                            else
                            {
                                result = listImps.Skip(param.Start.Value).Take(param.Limit.Value).ToList();
                            }
                        }
                    }
                    else
                    {
                        result = listImps;
                    }
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }
    }
}
