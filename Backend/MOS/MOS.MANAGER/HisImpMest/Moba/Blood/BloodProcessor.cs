using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisBlood;
using MOS.MANAGER.HisExpMestBlood;
using MOS.MANAGER.HisImpMestBlood;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Moba.Blood
{
    class BloodProcessor : BusinessBase
    {
        private HisImpMestBloodCreate hisImpMestBloodCreate;
        private HisExpMestBloodUpdate hisExpMestBloodUpdate;
        private HisBloodUpdate hisBloodUpdate;

        internal BloodProcessor(CommonParam param)
            : base(param)
        {
            this.hisBloodUpdate = new HisBloodUpdate(param);
            this.hisExpMestBloodUpdate = new HisExpMestBloodUpdate(param);
            this.hisImpMestBloodCreate = new HisImpMestBloodCreate(param);
        }

        internal bool Run(HisImpMestMobaBloodSDO data, HIS_IMP_MEST impMest, ref List<HIS_IMP_MEST_BLOOD> hisImpMestBloods)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(data.BloodIds))
                {
                    List<HIS_IMP_MEST_BLOOD> mobaImpMestBlood = new List<HIS_IMP_MEST_BLOOD>();
                    List<HIS_BLOOD> mobaBloods = new List<HIS_BLOOD>();
                    List<HIS_EXP_MEST_BLOOD> updateList = new List<HIS_EXP_MEST_BLOOD>();
                    List<HIS_EXP_MEST_BLOOD> beforeUpdateList = new List<HIS_EXP_MEST_BLOOD>();
                    List<HIS_EXP_MEST_BLOOD> existedExpMestBloods = new HisExpMestBloodGet().GetExportedByExpMestId(impMest.MOBA_EXP_MEST_ID.Value);

                    List<long> bloodIds = data.BloodIds.Distinct().ToList();
                    List<HIS_BLOOD> hisBloods = new HisBloodGet().GetByIds(bloodIds);

                    if (!IsNotNullOrEmpty(hisBloods))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                        throw new Exception("Khong lay duoc danh sach HIS_BLOOD theo " + LogUtil.TraceData("BloodIds", data.BloodIds));
                    }
                    if (!IsNotNullOrEmpty(existedExpMestBloods))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Khong lay duoc danh sach HIS_EXP_MEST_BLOOD");
                    }

                    Mapper.CreateMap<HIS_EXP_MEST_BLOOD, HIS_EXP_MEST_BLOOD>();
                    foreach (var bloodId in bloodIds)
                    {
                        HIS_EXP_MEST_BLOOD expMestBlood = existedExpMestBloods.FirstOrDefault(o => o.BLOOD_ID == bloodId);
                        if (expMestBlood == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("khong co HIS_EXP_MEST_BLOOD theo BloodId: " + bloodId);
                        }

                        HIS_BLOOD blood = hisBloods.FirstOrDefault(o => o.ID == bloodId);
                        if (blood == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("BloodId invalid: " + bloodId);
                        }

                        if (expMestBlood.IS_TH == MOS.UTILITY.Constant.IS_TRUE)
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_TuiMauDaDuocThuHoi, blood.BLOOD_CODE);
                            throw new Exception("Tui mau da duoc thu hoi" + LogUtil.TraceData("ExpMestBlood", expMestBlood));
                        }

                        HIS_IMP_MEST_BLOOD impMestBlood = new HIS_IMP_MEST_BLOOD();
                        impMestBlood.BLOOD_ID = blood.ID;
                        impMestBlood.IMP_MEST_ID = impMest.ID;
                        mobaImpMestBlood.Add(impMestBlood);

                        if (blood.IS_ACTIVE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                        {
                            blood.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
                            mobaBloods.Add(blood);
                        }

                        HIS_EXP_MEST_BLOOD before = Mapper.Map<HIS_EXP_MEST_BLOOD>(expMestBlood);
                        expMestBlood.IS_TH = MOS.UTILITY.Constant.IS_TRUE;
                        updateList.Add(expMestBlood);
                        beforeUpdateList.Add(before);
                    }

                    if (!this.hisImpMestBloodCreate.CreateList(mobaImpMestBlood))
                    {
                        throw new Exception("Tao HIS_IMP_MEST_BLOOD that bai");
                    }
                    if (!this.hisExpMestBloodUpdate.UpdateList(updateList, beforeUpdateList))
                    {
                        throw new Exception("Update HIS_EXP_MEST_BLOOD that bai.");
                    }
                    if (IsNotNullOrEmpty(mobaBloods))
                    {
                        if (!this.hisBloodUpdate.UpdateList(mobaBloods))
                        {
                            throw new Exception("Update HIS_BLOOD that bai.");
                        }
                    }
                    hisImpMestBloods = mobaImpMestBlood;
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

        internal void Rollback()
        {
            this.hisBloodUpdate.RollbackData();
            this.hisExpMestBloodUpdate.RollbackData();
            this.hisImpMestBloodCreate.RollbackData();
        }
    }
}
