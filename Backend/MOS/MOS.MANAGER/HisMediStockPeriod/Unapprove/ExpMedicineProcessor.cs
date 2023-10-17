using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMedicine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMediStockPeriod.Unapprove
{
    class ExpMedicineProcessor : BusinessBase
    {
        private long expMestId;

        internal ExpMedicineProcessor(CommonParam param)
            : base(param)
        {
            
        }

        internal bool Run(HIS_EXP_MEST expMest, ref List<string> sqls)
        {
            bool result = false;
            try
            {
                if (expMest != null)
                {
                    this.expMestId = expMest.ID;
                    List<HIS_EXP_MEST_MEDICINE> hisExpMestMedicines = new HisExpMestMedicineGet().GetByExpMestId(expMest.ID);
                    if (IsNotNullOrEmpty(hisExpMestMedicines))
                    {
                        foreach (var medicine in hisExpMestMedicines)
                        {
                            if (medicine.IS_ACTIVE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                            {
                                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
                                LogSystem.Error("Ton tai HIS_EXP_MEST_MEDICINE dang bi khoa" + LogUtil.TraceData("expMestMedicine", medicine));
                                return false;
                            }
                            if (medicine.IS_EXPORT == MOS.UTILITY.Constant.IS_TRUE)
                            {
                                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                                LogSystem.Error("Ton tai HIS_EXP_MEST_MEDICINE o trang thai da xuat IS_EXPORT = 1" + LogUtil.TraceData("expMestMedicine", medicine));
                                return false;
                            }
                        }

                        List<long> expMestMedicineIds = hisExpMestMedicines.Select(o => o.ID).ToList();

                        string updateBean = DAOWorker.SqlDAO.AddInClause(expMestMedicineIds, "UPDATE HIS_MEDICINE_BEAN SET IS_ACTIVE = 1, EXP_MEST_MEDICINE_ID = NULL WHERE %IN_CLAUSE% ", "EXP_MEST_MEDICINE_ID");
                        string deleteMedicine = DAOWorker.SqlDAO.AddInClause(expMestMedicineIds, "UPDATE HIS_EXP_MEST_MEDICINE SET IS_DELETE = 1, EXP_MEST_ID = NULL, MEDICINE_ID = NULL, TDL_MEDI_STOCK_ID = NULL, TDL_MEDICINE_TYPE_ID = NULL, TDL_VACCINATION_ID = NULL, VACCINATION_RESULT_ID = NULL WHERE %IN_CLAUSE% ", "ID");
                        sqls.Add(updateBean);
                        sqls.Add(deleteMedicine);//Luu y: can update bean truoc khi xoa medicine de tranh loi FK
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

    }
}
