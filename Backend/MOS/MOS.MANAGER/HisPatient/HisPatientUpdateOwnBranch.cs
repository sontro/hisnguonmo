using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisPatient
{
    class HisPatientUpdateOwnBranch : BusinessBase
    {

        HisPatientUpdate hisPatientUpdate;

        internal HisPatientUpdateOwnBranch()
            : base()
        {
            this.hisPatientUpdate = new HisPatientUpdate(param);
        }

        internal HisPatientUpdateOwnBranch(CommonParam param)
            : base(param)
        {
            this.hisPatientUpdate = new HisPatientUpdate(param);
        }


        internal bool Follow(HIS_PATIENT data, ref HIS_PATIENT resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_PATIENT raw = null;
                HIS_BRANCH branch = null;
                HisPatientCheck checker = new HisPatientCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && this.CheckBranch(ref branch);
                if (valid)
                {
                    if (HisPatientUtil.IsExistsOwnBranchIds(raw.OWN_BRANCH_IDS, branch.ID))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisPatient_CoSoDangTheoDoiBenhNhan);
                        return false;
                    }
                    Mapper.CreateMap<HIS_PATIENT, HIS_PATIENT>();
                    HIS_PATIENT before = Mapper.Map<HIS_PATIENT>(raw);
                    HisPatientUtil.SetOwnBranhIds(raw, branch.ID);
                    if (!this.hisPatientUpdate.Update(raw, before))
                    {
                        throw new Exception("Khong update duoc  OWN_BRANCH_IDS");
                    }
                    result = true;
                    resultData = raw;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool Unfollow(HIS_PATIENT data, ref HIS_PATIENT resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_PATIENT raw = null;
                HIS_BRANCH branch = null;
                HisPatientCheck checker = new HisPatientCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && this.CheckBranch(ref branch);
                if (valid)
                {
                    if (!HisPatientUtil.IsExistsOwnBranchIds(raw.OWN_BRANCH_IDS, branch.ID))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisPatient_CoSoDangBoTheoDoiBenhNhan);
                        return false;
                    }
                    Mapper.CreateMap<HIS_PATIENT, HIS_PATIENT>();
                    HIS_PATIENT before = Mapper.Map<HIS_PATIENT>(raw);
                    HisPatientUtil.RemoveOwnBranhIds(raw, branch.ID);
                    if (!this.hisPatientUpdate.Update(raw, before))
                    {
                        throw new Exception("Khong update duoc  OWN_BRANCH_IDS");
                    }
                    result = true;
                    resultData = raw;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private bool CheckBranch(ref HIS_BRANCH branch)
        {
            bool valid = true;
            try
            {
                branch = new TokenManager(param).GetBranch();
                valid = branch != null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }
    }
}
