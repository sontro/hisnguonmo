using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineBean.Handle
{
    class HisMedicineBeanAddToExpMest : BusinessBase
    {
        internal HisMedicineBeanAddToExpMest()
            : base()
        {

        }

        internal HisMedicineBeanAddToExpMest(CommonParam paramUnlock)
            : base(paramUnlock)
        {

        }

        /// <summary>
        /// Cap nhat toan bo danh sach thanh mo khoa
        /// </summary>
        /// <param name="listData"></param>
        /// <returns></returns>
        internal List<string> GenSql(Dictionary<long, List<long>> useBeandIdDic)
        {
            if (IsNotNullOrEmpty(useBeandIdDic))
            {
                List<string> sqls = new List<string>();
                foreach (long expMestMedicineId in useBeandIdDic.Keys)
                {
                    List<long> beanIds = useBeandIdDic[expMestMedicineId];
                    //cap nhat danh sach cac bean da dung
                    string sql = DAOWorker.SqlDAO.AddInClause(beanIds, "UPDATE HIS_MEDICINE_BEAN SET SESSION_KEY = NULL, SESSION_TIME = NULL, IS_ACTIVE = 0, EXP_MEST_MEDICINE_ID = {0} WHERE %IN_CLAUSE% ", "ID");
                    sql = string.Format(sql, expMestMedicineId);
                    sqls.Add(sql);
                }
                return sqls;
            }
            return null;
        }

        /// <summary>
        /// Cap nhat toan bo danh sach thanh mo khoa
        /// </summary>
        /// <param name="listData"></param>
        /// <returns></returns>
        internal List<string> GenSql(long expMestMedicineId, List<long> useBeandIds)
        {
            if (expMestMedicineId > 0 && IsNotNullOrEmpty(useBeandIds))
            {
                Dictionary<long, List<long>> useBeandIdDic = new Dictionary<long, List<long>>();
                useBeandIdDic.Add(expMestMedicineId, useBeandIds);
                return this.GenSql(useBeandIdDic);
            }
            return null;
        }
    }
}
