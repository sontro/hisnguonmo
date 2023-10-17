using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMaterialBean.Handle
{
    class HisMaterialBeanUnlock : BusinessBase
    {
        private List<long> recentMaterialBeanIds;

        internal HisMaterialBeanUnlock()
            : base()
        {

        }

        internal HisMaterialBeanUnlock(CommonParam paramUnlock)
            : base(paramUnlock)
        {

        }

        /// <summary>
        /// Cap nhat toan bo danh sach thanh mo khoa
        /// </summary>
        /// <param name="listData"></param>
        /// <returns></returns>
        internal bool Run(List<long> ids)
        {
            if (IsNotNullOrEmpty(ids))
            {
                string query = DAOWorker.SqlDAO.AddInClause(ids, "UPDATE HIS_MATERIAL_BEAN SET IS_ACTIVE = 1, EXP_MEST_MATERIAL_ID = NULL WHERE %IN_CLAUSE% ", "ID");
                if (!DAOWorker.SqlDAO.Execute(query))
                {
                    LogSystem.Error("unlock material_bean that bai");
                    return false;
                }
                this.recentMaterialBeanIds = ids; //phuc vu rollback
            }
            return true;
        }

        internal void Rollback()
        {
            try
            {
                if (IsNotNullOrEmpty(this.recentMaterialBeanIds))
                {
                    string query = DAOWorker.SqlDAO.AddInClause(this.recentMaterialBeanIds, "UPDATE HIS_MATERIAL_BEAN SET IS_ACTIVE = 0 WHERE %IN_CLAUSE% ", "ID");
                    if (!DAOWorker.SqlDAO.Execute(query))
                    {
                        LogSystem.Warn("Rollback unlock material_bean that bai");
                    }
                    this.recentMaterialBeanIds = null;// tranh truong hop rollback 2 lan
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
