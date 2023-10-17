using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBlood.Update
{
    class HisBloodUnlock : BusinessBase
    {
        private List<long> recentBloodIds;

        internal HisBloodUnlock()
            : base()
        {

        }

        internal HisBloodUnlock(CommonParam paramUnlock)
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
                string query = DAOWorker.SqlDAO.AddInClause(ids, "UPDATE HIS_BLOOD SET IS_ACTIVE = 1 WHERE %IN_CLAUSE% ", "ID");
                this.recentBloodIds = ids; //phuc vu rollback
                if (!DAOWorker.SqlDAO.Execute(query))
                {
                    LogSystem.Error("unlock material_bean that bai");
                    return false;
                }
            }
            return true;
        }

        internal void Rollback()
        {
            try
            {
                if (IsNotNullOrEmpty(this.recentBloodIds))
                {
                    string query = DAOWorker.SqlDAO.AddInClause(this.recentBloodIds, "UPDATE HIS_BLOOD SET IS_ACTIVE = 0 WHERE %IN_CLAUSE% ", "ID");
                    if (!DAOWorker.SqlDAO.Execute(query))
                    {
                        LogSystem.Warn("Rollback unlock material_bean that bai");
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
