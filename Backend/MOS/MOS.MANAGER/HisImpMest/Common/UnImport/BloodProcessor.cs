using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisBlood;
using MOS.MANAGER.HisExpMestBlood;
using MOS.MANAGER.HisImpMestBlood;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.UnImport
{
    class ImpExpMestBloodData
    {
        public decimal AMOUNT { get; set; }
        public long IMP_EXP_TIME { get; set; }
    }

    class BloodProcessor : BusinessBase
    {
        private HisBloodUpdate hisBloodUpdate;

        internal BloodProcessor(CommonParam param)
            : base(param)
        {
            this.hisBloodUpdate = new HisBloodUpdate(param);
        }

        internal bool Run(HIS_IMP_MEST impMest)
        {
            bool result = false;
            try
            {
                List<HIS_IMP_MEST_BLOOD> hisImpMestBloods = new HisImpMestBloodGet().GetByImpMestId(impMest.ID);
                if (IsNotNullOrEmpty(hisImpMestBloods))
                {
                    List<HIS_BLOOD> beforeUpdates = new List<HIS_BLOOD>();
                    List<HIS_BLOOD> listBlood = new List<HIS_BLOOD>();
                    Mapper.CreateMap<HIS_BLOOD, HIS_BLOOD>();
                    foreach (var impMestBlood in hisImpMestBloods)
                    {
                        if (!this.CheckCorrectImpExp(impMestBlood.BLOOD_ID, impMest.MEDI_STOCK_ID, impMest.ID))
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_TonTaiDuLieu);
                            throw new Exception("BLOOD da co phieu xuat khong cho phep huy nhap BLOOD_ID: " + impMestBlood.BLOOD_ID);
                        }

                        HIS_BLOOD blood = new HisBloodGet().GetById(impMestBlood.BLOOD_ID);
                        if (blood == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                            throw new Exception("Khong lay duoc BLOOD theo BLOOD_ID :" + impMestBlood.BLOOD_ID);
                        }
                        if (!blood.MEDI_STOCK_ID.HasValue || blood.MEDI_STOCK_ID.Value != impMest.MEDI_STOCK_ID)
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_TuiMauDaDuocXuat, blood.BLOOD_CODE);
                            throw new Exception("Tui mau khong on o trong kho: " + LogUtil.TraceData("Blood", blood));
                        }
                        beforeUpdates.Add(Mapper.Map<HIS_BLOOD>(blood));
                        blood.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
                        blood.MEDI_STOCK_ID = null;
                        if (HisImpMestContanst.TYPE_MUST_CREATE_PACKAGE_IDS.Contains(impMest.IMP_MEST_TYPE_ID))
                        {
                            blood.IMP_TIME = null;
                            blood.IS_PREGNANT = MOS.UTILITY.Constant.IS_TRUE;
                        }
                        listBlood.Add(blood);
                    }
                    if (!this.hisBloodUpdate.UpdateList(listBlood, beforeUpdates))
                    {
                        throw new Exception("Update HIS_BLOOD that bai");
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private bool CheckCorrectImpExp(long bloodId, long mediStockId, long impMestId)
        {
            List<ImpExpMestBloodData> checks = new List<ImpExpMestBloodData>();

            string queryExp = new StringBuilder().Append("SELECT -1 as AMOUNT, EMBL.EXP_TIME as IMP_EXP_TIME FROM HIS_EXP_MEST_BLOOD EMBL ").Append("JOIN HIS_EXP_MEST EXP ON EMBL.EXP_MEST_ID = EXP.ID ").Append(" WHERE EXP.MEDI_STOCK_ID = ").Append(mediStockId).Append(" AND EMBL.BLOOD_ID = ").Append(bloodId).Append(" AND EMBL.IS_EXPORT = 1").ToString();
            List<ImpExpMestBloodData> expMaterials = DAOWorker.SqlDAO.GetSql<ImpExpMestBloodData>(queryExp);
            if (!IsNotNullOrEmpty(expMaterials))
            {
                return true;
            }
            checks.AddRange(expMaterials);
            string queryImp = new StringBuilder().Append("SELECT 1 as AMOUNT, IMP.IMP_TIME as IMP_EXP_TIME FROM HIS_IMP_MEST_BLOOD IMBL ").Append("JOIN HIS_IMP_MEST IMP ON IMBL.IMP_MEST_ID = IMP.ID ").Append("WHERE IMP.MEDI_STOCK_ID = ").Append(mediStockId).Append(" AND IMP.ID <> ").Append(impMestId).Append(" AND IMP.IMP_MEST_STT_ID = ").Append(IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__IMPORT).Append(" AND IMBL.BLOOD_ID = ").Append(bloodId).ToString();
            List<ImpExpMestBloodData> impMaterials = DAOWorker.SqlDAO.GetSql<ImpExpMestBloodData>(queryImp);

            if (!IsNotNullOrEmpty(impMaterials))
            {
                return false;
            }
            checks.AddRange(impMaterials);
            checks = checks.OrderBy(o => o.IMP_EXP_TIME).ToList();
            decimal availAmount = 0;
            foreach (ImpExpMestBloodData item in checks)
            {
                availAmount += item.AMOUNT;
                if (availAmount < 0)
                {
                    return false;
                }
            }
            return true;
        }

        internal void Rollback()
        {
            try
            {
                this.hisBloodUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
