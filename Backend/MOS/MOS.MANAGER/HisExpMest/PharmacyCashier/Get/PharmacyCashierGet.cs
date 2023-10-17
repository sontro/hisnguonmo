using Inventec.Common.Logging;
using Inventec.Core;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.PharmacyCashier.Get
{
    class PharmacyCashierGet : BusinessBase
    {
        internal PharmacyCashierGet()
            : base()
        {
        }

        internal PharmacyCashierGet(CommonParam param)
            : base(param)
        {
        }

        internal List<DHisTransExpSDO> Get(DHisTransExpFilter filter)
        {
            try
            {
                string sql = string.Format("SELECT * FROM D_HIS_TRANS_EXP WHERE (MEDI_STOCK_ID = {0} OR CASHIER_ROOM_ID = {1}) ", filter.MEDI_STOCK_ID, filter.CASHIER_ROOM_ID);
                StringBuilder sb = new StringBuilder(sql);

                if (IsNotNullOrEmpty(filter.TYPE_IDs))
                {
                    string s = string.Join(",", filter.TYPE_IDs);
                    string condition = string.Format(" AND TYPE_ID IN ({0})", s);
                    sb.Append(condition);
                }
                if (!string.IsNullOrWhiteSpace(filter.ACCOUNT_BOOK_CODE__EXACT))
                {
                    string condition = string.Format(" AND ACCOUNT_BOOK_CODE IS NOT NULL AND ACCOUNT_BOOK_CODE = '{0}'", filter.ACCOUNT_BOOK_CODE__EXACT);
                    sb.Append(condition);
                }
                if (!string.IsNullOrWhiteSpace(filter.KEY_WORD))
                {
                    string keyword = filter.KEY_WORD.Trim().ToLower();
                    string condition = string.Format(" AND (LOWER(TDL_PATIENT_CODE) LIKE '%{0}%' OR LOWER(TDL_PATIENT_NAME) LIKE '%{1}%' OR LOWER(TRANSACTION_CODE) LIKE '%{2}%' OR LOWER(TREATMENT_CODE) LIKE '%{3}%' OR LOWER(EXP_MEST_CODE) LIKE '%{4}%' OR LOWER(SYMBOL_CODE) LIKE '%{5}%' OR LOWER(TEMPLATE_CODE) LIKE '%{6}%' )", keyword, keyword, keyword, keyword, keyword, keyword, keyword);
                    sb.Append(condition);
                }
                if (filter.NUM_ORDER__EQUAL.HasValue)
                {
                    string condition = string.Format(" AND NUM_ORDER IS NOT NULL AND NUM_ORDER = {0}", filter.NUM_ORDER__EQUAL.Value);
                    sb.Append(condition);
                }
                if (!string.IsNullOrWhiteSpace(filter.EXP_MEST_CODE__EXACT))
                {
                    //do trong truong hop 1 giao dich gan voi nhieu phieu xuat thi truong EXP_MEST_CODE hien thi duoi dang cac ma phieu xuat ngan cach bang dau phay
                    string condition = string.Format(" AND EXP_MEST_CODE IS NOT NULL AND  ',' || EXP_MEST_CODE || ',' LIKE '%,{0},%'", filter.EXP_MEST_CODE__EXACT);
                    sb.Append(condition);
                }
                if (!string.IsNullOrWhiteSpace(filter.ACCOUNT_BOOK_NAME))
                {
                    string condition = string.Format(" AND ACCOUNT_BOOK_NAME IS NOT NULL AND ACCOUNT_BOOK_NAME LIKE '%{0}%'", filter.ACCOUNT_BOOK_NAME);
                    sb.Append(condition);
                }
                if (filter.CREATE_TIME_FROM.HasValue)
                {
                    string condition = string.Format(" AND CREATE_TIME >= {0}", filter.CREATE_TIME_FROM.Value);
                    sb.Append(condition);
                }
                if (filter.CREATE_TIME_TO.HasValue)
                {
                    string condition = string.Format(" AND CREATE_TIME <= {0}", filter.CREATE_TIME_TO.Value);
                    sb.Append(condition);
                }
                if (!string.IsNullOrWhiteSpace(filter.SYMBOL_CODE))
                {
                    string condition = string.Format(" AND SYMBOL_CODE LIKE '%{0}%'", filter.SYMBOL_CODE);
                    sb.Append(condition);
                }
                if (!string.IsNullOrWhiteSpace(filter.TEMPLATE_CODE))
                {
                    string condition = string.Format(" AND TEMPLATE_CODE LIKE '%{0}%'", filter.TEMPLATE_CODE);
                    sb.Append(condition);
                }
                if (!string.IsNullOrWhiteSpace(filter.TREATMENT_CODE__EXACT))
                {
                    string condition = string.Format(" AND TREATMENT_CODE IS NOT NULL AND TREATMENT_CODE = '{0}'", filter.TREATMENT_CODE__EXACT);
                    sb.Append(condition);
                }
                if (filter.TYPE_ID.HasValue)
                {
                    string condition = string.Format(" AND TYPE_ID = '{0}'", filter.TYPE_ID);
                    sb.Append(condition);
                }
                List<DHisTransExpSDO> data = DAOWorker.SqlDAO.GetSql<DHisTransExpSDO>(sb.ToString());
                //sort va paging lai du lieu
                int start = param.Start.HasValue ? param.Start.Value : 0;
                int limit = param.Limit.HasValue ? param.Limit.Value : Int32.MaxValue;
                param.Count = data.Count;

                return data.AsQueryable().OrderByProperty(filter.ORDER_FIELD, filter.ORDER_DIRECTION).Skip(start).Take(limit).ToList();
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
