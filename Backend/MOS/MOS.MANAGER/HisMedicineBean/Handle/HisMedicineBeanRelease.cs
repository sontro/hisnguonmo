using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Token.ResourceSystem;
using MOS.EFMODEL.DataModels;
using MOS.LibraryMessage;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMedicineBean.Handle
{
    /// <summary>
    /// Nghiep vu release bean theo session
    /// </summary>
    class HisMedicineBeanRelease : BusinessBase
    {
        internal HisMedicineBeanRelease()
            : base()
        {
        }

        internal HisMedicineBeanRelease(CommonParam paramCreate)
            : base(paramCreate)
        {
        }

        /// <summary>
        /// Tra bean. Lay toan bo cac bean tuong ung voi medi-stock-id, medicine-type-id, session-key truyen vao,
        /// va cap nhat is_active = 1, session_key = null
        /// </summary>
        /// <param name="mediStockId"></param>
        /// <param name="medicineTypeId"></param>
        /// <param name="clientSessionKey"></param>
        /// <returns></returns>
        internal bool Release(ReleaseBeanSDO sdo)
        {
            try
            {
                string sql = null;
                long id = 0;
                if (sdo.MameId.HasValue)
                {
                    sql = "UPDATE HIS_MEDICINE_BEAN "
                        + " SET SESSION_KEY = NULL, IS_ACTIVE = 1 "
                        + " WHERE MEDICINE_ID = :param1 "
                        + " AND MEDI_STOCK_ID = :param2 "
                        + " AND SESSION_KEY = :param3 "
                        + " AND %IN_CLAUSE% ";
                    id = sdo.MameId.Value;
                }
                else
                {
                    sql = "UPDATE HIS_MEDICINE_BEAN "
                        + " SET SESSION_KEY = NULL, IS_ACTIVE = 1 "
                        + " WHERE TDL_MEDICINE_TYPE_ID = :param1 "
                        + " AND MEDI_STOCK_ID = :param2 "
                        + " AND SESSION_KEY = :param3 "
                        + " AND %IN_CLAUSE% ";
                    id = sdo.TypeId;
                }
                
                sql = DAOWorker.SqlDAO.AddInClause(sdo.BeanIds, sql, "ID");
                if (!DAOWorker.SqlDAO.Execute(sql, id, sdo.MediStockId, SessionUtil.SessionKey(sdo.ClientSessionKey)))
                {
                    LogSystem.Warn("Release bean that bai. sql: " + sql + LogUtil.TraceData("sdo", sdo));
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
        }

        /// <summary>
        /// Tra bean. Lay toan bo cac bean tuong ung voi session-key truyen vao,
        /// va cap nhat is_active = 1, session_key = null
        /// </summary>
        /// <param name="mediStockId"></param>
        /// <param name="medicineTypeId"></param>
        /// <param name="clientSessionKey"></param>
        /// <returns></returns>
        internal bool Release(string clientSessionKey)
        {
            try
            {
                string sql = "UPDATE HIS_MEDICINE_BEAN "
                            + " SET SESSION_KEY = NULL, IS_ACTIVE = 1 "
                            + " WHERE SESSION_KEY = :param1 AND IS_ACTIVE = 0 ";
                if (!DAOWorker.SqlDAO.Execute(sql, SessionUtil.SessionKey(clientSessionKey)))
                {
                    LogSystem.Warn("Release bean that bai. sql: " + sql + LogUtil.TraceData("clientSessionKey", SessionUtil.SessionKey(clientSessionKey)));
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
        }
    }
}
