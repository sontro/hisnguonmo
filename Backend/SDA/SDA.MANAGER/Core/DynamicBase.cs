using SDA.Filter;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.MANAGER.Core
{
    abstract class DynamicBase : Inventec.Backend.MANAGER.BusinessBase
    {
        internal DynamicBase(CommonParam param)
            : base(param)
        {

        }

        protected List<object> RunBase(string tableviewname, FilterBase filterBase)
        {
            List<object> result = new List<object>();
            try
            {
                string strRowNumber1 = "";
                string strRowNumber2 = "";
                string strWhereRownum1 = "";
                string strFilterCondition = "";
                string strOrderBy1 = "";
                string strColumn1 = "";
                string strColumn2 = "";

                if (String.IsNullOrEmpty(filterBase.ORDER_FIELD))
                {
                    filterBase.ORDER_FIELD = "MODIFY_TIME";
                }
                if (String.IsNullOrEmpty(filterBase.ORDER_DIRECTION))
                {
                    filterBase.ORDER_DIRECTION = "DESC";
                }

                if (filterBase.ColumnParams != null && filterBase.ColumnParams.Count > 0)
                {
                    List<string> columnParamsForcq = new List<string>();
                    if (!filterBase.ColumnParams.Contains(filterBase.ORDER_FIELD))
                    {
                        columnParamsForcq.AddRange(filterBase.ColumnParams);
                        columnParamsForcq.Add(filterBase.ORDER_FIELD);
                    }

                    List<string> cq1 = (from m in columnParamsForcq select (InsertQuotation("q1") + "." + m)).ToList();
                    List<string> cf1 = (from m in filterBase.ColumnParams select (InsertQuotation("f1") + "." + m)).ToList();
                    strColumn1 = string.Join(", ", cf1);
                    strColumn2 = string.Join(", ", cq1);
                }
                else
                {
                    strColumn1 = "*";
                    strColumn2 = "*";
                }

                string qrTemplate = @"SELECT * FROM (SELECT {0}               
                FROM ( 
                SELECT {1}  
                {2}                
                FROM {3} " + InsertQuotation("q1") + @"                 
                WHERE ((" + InsertQuotation("q1") + @".IS_DELETE IS NULL) OR (1 <> ( CAST( " + InsertQuotation("q1") + @".IS_DELETE AS number(10,0)))))
                {4}
                ) " + "\"f1\"" + @"                
                {5}                
                {6}                
                )
                {7}                
                ";

                if (param.Start > 0 && param.Limit > 0)
                {
                    strRowNumber1 = String.Format("row_number() OVER (ORDER BY " + InsertQuotation("q1") + ".{0} {1}) AS row_number", filterBase.ORDER_FIELD, filterBase.ORDER_DIRECTION);
                    strRowNumber2 = " WHERE (" + InsertQuotation("f1") + ".row_number > 0)";
                    strWhereRownum1 = String.Format("WHERE (ROWNUM > ({0}) AND ROWNUM <= ({1}))", param.Start, param.Limit);
                }

                strOrderBy1 = String.Format("ORDER BY " + InsertQuotation("f1") + ".{0} {1}", filterBase.ORDER_FIELD, filterBase.ORDER_DIRECTION);

                strFilterCondition += ProcessFilterQuery();

                string query = String.Format(qrTemplate, strColumn1, strColumn2, strRowNumber1, tableviewname, strFilterCondition, strRowNumber2, strOrderBy1, strWhereRownum1);

                result = SDA.MANAGER.Base.DAOWorker.SqlDAO.GetDynamicSql(query);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
            return result;
        }

        protected virtual string ProcessFilterQuery()
        {
            return "";
        }

        string InsertQuotation(string input)
        {
            return "\"" + input + "\"";
        }
    }
}
