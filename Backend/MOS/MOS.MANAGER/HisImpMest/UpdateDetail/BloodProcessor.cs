using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisBlood;
using MOS.MANAGER.HisExpMestBlood;
using MOS.MANAGER.HisImpMestBlood;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.UpdateDetail
{
    class BloodProcessor : BusinessBase
    {
        private HisImpMestBloodUpdate hisImpMestBloodUpdate;
        private HisBloodUpdate hisBloodUpdate;

        internal BloodProcessor(CommonParam param)
            : base(param)
        {
            this.hisImpMestBloodUpdate = new HisImpMestBloodUpdate(param);
            this.hisBloodUpdate = new HisBloodUpdate(param);
        }

        internal bool Run(HisImpMestUpdateDetailSDO data, HIS_IMP_MEST impMest, HisImpMestUpdateDetailLog logProcessor)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(data.ImpMestBloods))
                {
                    List<HIS_IMP_MEST_BLOOD> listRaw = new List<HIS_IMP_MEST_BLOOD>();
                    List<HIS_BLOOD> rawBloods = new List<HIS_BLOOD>();
                    HisImpMestBloodCheck checker = new HisImpMestBloodCheck(param);
                    HisBloodCheck bloodChecker = new HisBloodCheck(param);
                    bool valid = true;
                    valid = valid && checker.VerifyIds(data.ImpMestBloods.Select(s => s.Id).ToList(), listRaw);
                    valid = valid && checker.IsUnLock(listRaw);
                    valid = valid && bloodChecker.VerifyIds(listRaw.Select(s => s.BLOOD_ID).ToList(), rawBloods);
                    valid = valid && bloodChecker.IsUnLock(rawBloods);
                    if (!valid)
                    {
                        return false;
                    }
                    Mapper.CreateMap<HIS_IMP_MEST_BLOOD, HIS_IMP_MEST_BLOOD>();
                    List<HIS_IMP_MEST_BLOOD> beforeImpMestBloods = Mapper.Map<List<HIS_IMP_MEST_BLOOD>>(listRaw);
                    Mapper.CreateMap<HIS_BLOOD, HIS_BLOOD>();
                    List<HIS_BLOOD> beforeBloods = Mapper.Map<List<HIS_BLOOD>>(rawBloods);
                    if (!this.CheckDetail(data, impMest, listRaw, rawBloods, logProcessor))
                    {
                        return false;
                    }

                    if (!this.hisImpMestBloodUpdate.UpdateList(listRaw, beforeImpMestBloods))
                    {
                        throw new Exception("hisImpMestBloodUpdate. Ket thuc nghiep vu");
                    }

                    if (!this.hisBloodUpdate.UpdateList(rawBloods, beforeBloods))
                    {
                        throw new Exception("hisBloodUpdate. Ket thuc nghiep vu");
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

        private bool CheckDetail(HisImpMestUpdateDetailSDO data, HIS_IMP_MEST impMest, List<HIS_IMP_MEST_BLOOD> listRaw, List<HIS_BLOOD> rawBloods, HisImpMestUpdateDetailLog logProcessor)
        {
            bool valid = true;
            try
            {
                if (listRaw.Exists(e => e.IMP_MEST_ID != impMest.ID))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Ton tai HIS_IMP_MEST_BLOOD khong thuoc phieu nhap can sua");
                }

                if (valid)
                {
                    foreach (HisImpMestBloodSDO sdo in data.ImpMestBloods)
                    {
                        HIS_IMP_MEST_BLOOD impMestMedi = listRaw.FirstOrDefault(o => o.ID == sdo.Id);
                        HIS_BLOOD blood = rawBloods.FirstOrDefault(o => o.ID == impMestMedi.BLOOD_ID);
                        HIS_BLOOD_TYPE bloodType = HisBloodTypeCFG.DATA.FirstOrDefault(o => o.ID == blood.BLOOD_TYPE_ID);
                        HIS_BLOOD_TYPE newBloodType = HisBloodTypeCFG.DATA.FirstOrDefault(o => o.ID == sdo.BloodTypeId);
                        if (!this.CheckExists(blood, impMest))
                        {
                            return false;
                        }
                        logProcessor.GenerateLogMessage(blood, bloodType, newBloodType, sdo);
                        impMestMedi.PRICE = sdo.ImpPrice;
                        impMestMedi.VAT_RATIO = sdo.ImpVatRatio;
                        blood.IMP_PRICE = sdo.ImpPrice;
                        blood.IMP_VAT_RATIO = sdo.ImpVatRatio;
                        blood.INTERNAL_PRICE = newBloodType.INTERNAL_PRICE;
                        blood.BLOOD_TYPE_ID = newBloodType.ID;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        private bool CheckExists(HIS_BLOOD blood, HIS_IMP_MEST impMest)
        {
            bool valid = true;
            try
            {
                List<V_HIS_EXP_MEST_BLOOD> existsExps = new HisExpMestBloodGet().GetViewByBloodId(blood.ID);
                if (IsNotNullOrEmpty(existsExps))
                {
                    List<string> expMestCodes = existsExps.Select(s => s.EXP_MEST_CODE).Distinct().ToList();
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_TuiMauDaThuocPhieuXuat, blood.BLOOD_CODE, String.Join(";", expMestCodes));
                    return false;
                }
                List<V_HIS_IMP_MEST_BLOOD> existsImps = new HisImpMestBloodGet().GetViewByBloodId(blood.ID);
                existsImps = existsImps != null ? existsImps.Where(o => o.IMP_MEST_ID != impMest.ID).ToList() : null;
                if (IsNotNullOrEmpty(existsImps))
                {
                    List<string> impMestCodes = existsImps.Select(s => s.IMP_MEST_CODE).Distinct().ToList();
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_TuiMauDaThuocPhieuNhap, blood.BLOOD_CODE, String.Join(";", impMestCodes));
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal void RollbackData()
        {
            try
            {
                this.hisBloodUpdate.RollbackData();
                this.hisImpMestBloodUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
