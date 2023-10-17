using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Token.ResourceSystem;
using MOS.EFMODEL.DataModels;
using MOS.LibraryMessage;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisMaterialBean.Handle;
using MOS.MANAGER.HisMaterialPaty;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMaterialBean.Handle
{
    /// <summary>
    /// Nghiep vu lay bean theo serial_number
    /// </summary>
    class HisMaterialBeanTakeBySerial : BusinessBase
    {
        private List<HIS_MATERIAL_BEAN> recentMaterialBeans = null;

        internal HisMaterialBeanTakeBySerial()
            : base()
        {
        }

        internal HisMaterialBeanTakeBySerial(CommonParam param)
            : base(param)
        {
        }

        internal bool Run(List<string> serialNumbers, long mediStockId, ref List<HIS_MATERIAL_BEAN> resultData)
        {
            return this.Run(serialNumbers, mediStockId, null, ref resultData);
        }

        internal bool Run(List<string> serialNumbers, long mediStockId, List<long> expMestMaterialIds, ref List<HIS_MATERIAL_BEAN> resultData)
        {
            bool result = false;
            try
            {
                if (!IsNotNullOrEmpty(serialNumbers))
                {
                    return true;
                }

                List<HIS_MATERIAL_BEAN> beans = this.GetBean(serialNumbers, mediStockId, expMestMaterialIds);
                List<string> unavailables = IsNotNullOrEmpty(serialNumbers) ? serialNumbers.Where(o => beans == null || !beans.Exists(t => t.SERIAL_NUMBER == o)).Select(o => o).ToList() : null;
                if (IsNotNullOrEmpty(unavailables))
                {
                    string s = string.Join(",", unavailables);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMaterialBean_VatTuKhongCoSan, s);
                    return false;
                }

                if (IsNotNullOrEmpty(beans) && this.LockBean(beans))
                {
                    resultData = beans;
                    this.recentMaterialBeans = beans;
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }

            return result;
        }

        private bool LockBean(List<HIS_MATERIAL_BEAN> beans)
        {
            if (IsNotNullOrEmpty(beans))
            {
                //cap nhat session_key thi trigger tu dong cap nhat is_active = 0
                string sql = "UPDATE HIS_MATERIAL_BEAN SET IS_USE = 1, IS_ACTIVE = 0 WHERE %IN_CLAUSE%";
                List<long> ids = beans.Select(o => o.ID).ToList();
                sql = DAOWorker.SqlDAO.AddInClause(ids, sql, "ID");
                return DAOWorker.SqlDAO.Execute(sql);
            }
            return true;
        }

        private List<HIS_MATERIAL_BEAN> GetBean(List<string> serialNumbers, long mediStockId, List<long> expMestMaterialIds)
        {
            if (IsNotNullOrEmpty(serialNumbers))
            {
                HisMaterialBeanFilterQuery filter = new HisMaterialBeanFilterQuery();
                filter.SERIAL_NUMBERs = serialNumbers;
                filter.MATERIAL_IS_ACTIVE = Constant.IS_TRUE;
                filter.IS_ACTIVE = Constant.IS_TRUE;
                filter.ACTIVE__OR__EXP_MEST_MATERIAL_IDs = expMestMaterialIds;
                filter.MEDI_STOCK_ID = mediStockId;
                return new HisMaterialBeanGet().Get(filter);
            }
            return null;
        }

        internal void Rollback()
        {
            try
            {
                if (IsNotNullOrEmpty(this.recentMaterialBeans))
                {
                    //cap nhat session_key thi trigger tu dong cap nhat is_active = 0
                    string sql = "UPDATE HIS_MATERIAL_BEAN SET IS_ACTIVE = 1 WHERE %IN_CLAUSE%";
                    List<long> ids = this.recentMaterialBeans.Select(o => o.ID).ToList();
                    sql = DAOWorker.SqlDAO.AddInClause(ids, sql, "ID");
                    if (DAOWorker.SqlDAO.Execute(sql))
                    {
                        this.recentMaterialBeans = null;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}