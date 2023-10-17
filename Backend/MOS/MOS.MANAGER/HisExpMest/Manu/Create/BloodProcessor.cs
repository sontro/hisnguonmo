using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisBlood;
using MOS.MANAGER.HisExpMestBlood;
using MOS.MANAGER.HisExpMestBltyReq;
using MOS.SDO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Manu.Create
{
    class BloodProcessor : BusinessBase
    {
        private HisBloodLock hisBloodLock;
        private HisExpMestBloodCreate hisExpMestBloodCreate;
        private List<long> bloodIds;

        internal BloodProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisBloodLock = new HisBloodLock(param);
            this.hisExpMestBloodCreate = new HisExpMestBloodCreate(param);
        }

        internal bool Run(List<ExpBloodSDO> bloods, HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_BLOOD> resultData)
        {
            try
            {
                if (IsNotNullOrEmpty(bloods) && expMest != null)
                {
                    this.bloodIds = bloods.Select(o => o.BloodId).ToList();
                    List<HIS_BLOOD> bls = new HisBloodGet().GetByIds(bloodIds);
                    if (!IsNotNullOrEmpty(bls))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("blood_id ko hop le");
                        return false;
                    }

                    List<string> notInMediStocks = bls.Where(o => o.MEDI_STOCK_ID != expMest.MEDI_STOCK_ID).Select(o => o.BLOOD_CODE).ToList();
                    if (IsNotNullOrEmpty(notInMediStocks))
                    {
                        string bloodCodes = string.Join(",", notInMediStocks);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisBlood_KhongThuocKho, bloodCodes);
                        return false;
                    }

                    List<string> lockBloods = bls.Where(o => o.IS_ACTIVE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE).Select(s => s.BLOOD_CODE).ToList();
                    if (IsNotNullOrEmpty(lockBloods))
                    {
                        string bloodCodes = string.Join(",", lockBloods);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisBlood_CacTuiMauSauDangTamKhoa, bloodCodes);
                        return false;
                    }

                    if (!this.hisBloodLock.LockList(bloodIds))
                    {
                        return false;
                    }

                    List<HIS_EXP_MEST_BLOOD> data = new List<HIS_EXP_MEST_BLOOD>();
                    //Duyet theo y/c cua client de tao ra exp_mest_blood tuong ung
                    foreach (ExpBloodSDO sdo in bloods)
                    {
                        HIS_BLOOD blood = bls.Where(o => o.ID == sdo.BloodId).FirstOrDefault();
                        if (blood == null)
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_TuiMauDuocChonKhongKhaDung);
                            return false;
                        }
                        HIS_EXP_MEST_BLOOD exp = new HIS_EXP_MEST_BLOOD();
                        exp.EXP_MEST_ID = expMest.ID;
                        exp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                        exp.TDL_BLOOD_TYPE_ID = blood.BLOOD_TYPE_ID;
                        exp.TDL_SERVICE_REQ_ID = expMest.SERVICE_REQ_ID;
                        exp.TDL_TREATMENT_ID = expMest.TDL_TREATMENT_ID;
                        exp.BLOOD_ID = sdo.BloodId;
                        exp.NUM_ORDER = sdo.NumOrder;
                        exp.DESCRIPTION = sdo.Description;
                        exp.AC_SELF_ENVIDENCE = sdo.AcSelfEnvidence;
                        exp.AC_SELF_ENVIDENCE_SECOND = sdo.AcSelfEnvidenceSecond;
                        data.Add(exp);
                    }

                    if (!this.hisExpMestBloodCreate.CreateList(data))
                    {
                        throw new Exception("Tao exp_mest_blood that bai. Rollback du lieu");
                    }
                    resultData = data;
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                return false;
            }
            return true;
        }

        internal void Rollback()
        {
            this.hisBloodLock.UnlockList(bloodIds);
            this.hisExpMestBloodCreate.RollbackData();
        }
    }
}
