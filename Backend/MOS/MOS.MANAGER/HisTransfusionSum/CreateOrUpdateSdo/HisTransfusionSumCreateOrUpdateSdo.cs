using AutoMapper;
using Inventec.Core;
using Inventec.Common.Logging;
using MOS.MANAGER.Base;
using MOS.EFMODEL.DataModels;
using MOS.SDO;
using MOS.MANAGER.HisExpMestBlood;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransfusionSum.CreateOrUpdateSdo
{
    class HisTransfusionSumCreateOrUpdateSdo : BusinessBase
    {
        HIS_EXP_MEST_BLOOD oldExpMestBlood = null;

        internal HisTransfusionSumCreateOrUpdateSdo()
            : base()
        {
        }

        internal HisTransfusionSumCreateOrUpdateSdo(CommonParam param)
            : base(param)
        {
        }

        internal bool Run(HisTransfusionSumSDO data, ref HIS_TRANSFUSION_SUM resultData)
        {
            bool result = false;
            try
            {
                HIS_EXP_MEST_BLOOD expMestBlood = null;
                HisTransfusionSumSdoCheck checker = new HisTransfusionSumSdoCheck(param);
                HisExpMestBloodCheck bloodchecker = new HisExpMestBloodCheck(param);
                bool valid = true;
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && bloodchecker.VerifyId(data.ExpMestBloodId, ref expMestBlood);
                valid = valid && bloodchecker.IsUnLock(expMestBlood);
                if (valid)
                {
                    Mapper.CreateMap<HIS_EXP_MEST_BLOOD, HIS_EXP_MEST_BLOOD>();
                    this.oldExpMestBlood = Mapper.Map<HIS_EXP_MEST_BLOOD>(expMestBlood);
                    if (expMestBlood.PUC != data.Puc)
                    {
                        expMestBlood.PUC = data.Puc;
                        if (!DAOWorker.HisExpMestBloodDAO.Update(expMestBlood))
                        {
                            MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestBlood_CapNhatThatBai);
                            throw new Exception("Cap nhat thong tin HisExpMestBlood that bai." + LogUtil.TraceData("data", expMestBlood));
                        }
                    }

                    var transfusions = new HisTransfusionSumGet().GetByExpMestBloodId(expMestBlood.ID);
                    if (IsNotNullOrEmpty(transfusions))
                    {
                        var update = transfusions.First();
                        update.TREATMENT_ID = data.TreatmentId;
                        update.ROOM_ID = data.RequestRoomId;
                        update.EXECUTE_LOGINNAME = data.ExecuteLoginname;
                        update.EXECUTE_USERNAME = data.ExecuteUsername;
                        update.START_TIME = data.StartTime;
                        update.FINISH_TIME = data.FinishTime;
                        update.ICD_CODE = data.IcdCode;
                        update.ICD_NAME = data.IcdName;
                        update.ICD_SUB_CODE = data.IcdSubCode;
                        update.ICD_TEXT = data.IcdText;
                        update.NOTE = data.Note;
                        update.NUM_ORDER = data.NumOrder;
                        update.TRANSFUSION_VOLUME = data.TransfusionVolume;

                        if (!DAOWorker.HisTransfusionSumDAO.Update(transfusions.First()))
                        {
                            MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransfusionSum_CapNhatThatBai);
                            throw new Exception("Cap nhat thong tin HisTransfusionSum that bai." + LogUtil.TraceData("data", update));
                        }
                        resultData = update;
                    }
                    else
                    {
                        HIS_TRANSFUSION_SUM create = new HIS_TRANSFUSION_SUM();
                        create.EXP_MEST_BLOOD_ID = data.ExpMestBloodId;
                        create.TREATMENT_ID = data.TreatmentId;
                        create.ROOM_ID = data.RequestRoomId;
                        create.EXECUTE_LOGINNAME = data.ExecuteLoginname;
                        create.EXECUTE_USERNAME = data.ExecuteUsername;
                        create.START_TIME = data.StartTime;
                        create.FINISH_TIME = data.FinishTime;
                        create.ICD_CODE = data.IcdCode;
                        create.ICD_NAME = data.IcdName;
                        create.ICD_SUB_CODE = data.IcdSubCode;
                        create.ICD_TEXT = data.IcdText;
                        create.NOTE = data.Note;
                        create.NUM_ORDER = data.NumOrder;
                        create.TRANSFUSION_VOLUME = data.TransfusionVolume;

                        if (!DAOWorker.HisTransfusionSumDAO.Create(create))
                        {
                            MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransfusionSum_ThemMoiThatBai);
                            throw new Exception("Them moi thong tin HisTransfusionSum that bai." + LogUtil.TraceData("data", create));
                        }
                        resultData = create;
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void Rollback()
        {
            if (IsNotNull(this.oldExpMestBlood))
            {
                if (!DAOWorker.HisExpMestBloodDAO.Update(this.oldExpMestBlood))
                {
                    LogSystem.Warn("Rollback du lieu HisExpMestBlood that bai, can kiem tra lai." + LogUtil.TraceData("data", this.oldExpMestBlood));
                }
                this.oldExpMestBlood = null;
            }
        }
    }
}
