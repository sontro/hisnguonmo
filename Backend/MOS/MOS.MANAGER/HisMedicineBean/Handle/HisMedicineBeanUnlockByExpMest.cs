using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineBean.Handle
{
    class HisMedicineBeanUnlockByExpMest : BusinessBase
    {
        internal HisMedicineBeanUnlockByExpMest()
            : base()
        {

        }

        internal HisMedicineBeanUnlockByExpMest(CommonParam paramUnlock)
            : base(paramUnlock)
        {

        }

        /// <summary>
        /// Cap nhat toan bo danh sach thanh mo khoa
        /// </summary>
        /// <param name="listData"></param>
        /// <returns></returns>
        internal string GenSql(List<long> expMestMedicineIds)
        {
            if (IsNotNullOrEmpty(expMestMedicineIds))
            {
                return DAOWorker.SqlDAO.AddInClause(expMestMedicineIds, "UPDATE HIS_MEDICINE_BEAN SET IS_ACTIVE = 1, EXP_MEST_MEDICINE_ID = NULL WHERE %IN_CLAUSE% ", "EXP_MEST_MEDICINE_ID");
            }
            return null;
        }
    }
}
