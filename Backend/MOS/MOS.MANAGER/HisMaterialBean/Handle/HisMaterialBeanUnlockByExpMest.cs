using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMaterialBean.Handle
{
    class HisMaterialBeanUnlockByExpMest : BusinessBase
    {
        internal HisMaterialBeanUnlockByExpMest()
            : base()
        {

        }

        internal HisMaterialBeanUnlockByExpMest(CommonParam paramUnlock)
            : base(paramUnlock)
        {

        }

        /// <summary>
        /// Cap nhat toan bo danh sach thanh mo khoa
        /// </summary>
        /// <param name="listData"></param>
        /// <returns></returns>
        internal string GenSql(List<long> expMestMaterialIds)
        {
            if (IsNotNullOrEmpty(expMestMaterialIds))
            {
                return DAOWorker.SqlDAO.AddInClause(expMestMaterialIds, "UPDATE HIS_MATERIAL_BEAN SET IS_ACTIVE = 1, EXP_MEST_MATERIAL_ID = NULL WHERE %IN_CLAUSE% ", "EXP_MEST_MATERIAL_ID");
            }
            return null;
        }
    }
}
