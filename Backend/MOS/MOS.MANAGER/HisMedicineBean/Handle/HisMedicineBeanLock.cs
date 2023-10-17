using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineBean.Handle
{
    class HisMedicineBeanLock : BusinessBase
    {
        private List<long> recentMedicineBeanIds;

        internal HisMedicineBeanLock()
            : base()
        {

        }

        internal HisMedicineBeanLock(CommonParam paramLock)
            : base(paramLock)
        {

        }

        /// <summary>
        /// Cap nhat toan bo danh sach thanh khoa
        /// </summary>
        /// <param name="listData"></param>
        /// <returns></returns>
        internal bool Run(List<long> ids)
        {
            if (IsNotNullOrEmpty(ids))
            {
                string query = DAOWorker.SqlDAO.AddInClause(ids, "UPDATE HIS_MEDICINE_BEAN SET IS_ACTIVE = 0, IS_USE = 1 WHERE %IN_CLAUSE% ", "ID");
                if (!DAOWorker.SqlDAO.Execute(query))
                {
                    LogSystem.Error("lock medicine_bean that bai");
                    return false;
                }
                this.recentMedicineBeanIds = ids; //phuc vu rollback
            }
            return true;
        }

        internal void Rollback()
        {
            try
            {
                if (IsNotNullOrEmpty(this.recentMedicineBeanIds))
                {
                    string query = DAOWorker.SqlDAO.AddInClause(this.recentMedicineBeanIds, "UPDATE HIS_MEDICINE_BEAN SET IS_ACTIVE = 1 WHERE %IN_CLAUSE% ", "ID");
                    if (!DAOWorker.SqlDAO.Execute(query))
                    {
                        LogSystem.Warn("Rollback lock medicine_bean that bai");
                    }
                    this.recentMedicineBeanIds = null; //tranh truong hop goi rollback 2 lan
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
