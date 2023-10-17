using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestBlood;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.Delete
{
    class BloodProcessor : BusinessBase
    {
        List<long> BloodIds;

        internal BloodProcessor(CommonParam param)
            : base(param)
        {
            
        }

        internal bool Run(HIS_EXP_MEST expMest, ref List<string> listSql)
        {
            bool result = false;
            try
            {
                if (expMest != null)
                {
                    List<HIS_EXP_MEST_BLOOD> hisExpMestBloods = new HisExpMestBloodGet().GetByExpMestId(expMest.ID);
                    if (IsNotNullOrEmpty(hisExpMestBloods))
                    {
                        
                        this.BloodIds = new List<long>();
                        foreach (var expBlood in hisExpMestBloods)
                        {
                            if (expBlood.IS_ACTIVE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                            {
                                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
                                LogSystem.Error("Ton tai HIS_EXP_MEST_BLOOD dang bi khoa" + LogUtil.TraceData("expMestBlood", expBlood));
                                return false;
                            }
                            if (expBlood.IS_EXPORT == MOS.UTILITY.Constant.IS_TRUE)
                            {
                                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                                LogSystem.Error("Ton tai HIS_EXP_MEST_BLOOD o trang thai da xuat IS_EXPORT = 1" + LogUtil.TraceData("expMestBlood", expBlood));
                                return false;
                            }
                            this.BloodIds.Add(expBlood.BLOOD_ID);

                        }

                        string deleteBlood = new StringBuilder().Append("DELETE HIS_EXP_MEST_BLOOD WHERE EXP_MEST_ID = ").Append(expMest.ID).ToString();
                        string updateBlood = new StringBuilder().Append(DAOWorker.SqlDAO.AddInClause(this.BloodIds, "UPDATE HIS_BLOOD SET IS_ACTIVE = 1 WHERE %IN_CLAUSE% ", "ID")).ToString();
                        listSql.Add(deleteBlood);
                        listSql.Add(updateBlood);
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

    }
}
