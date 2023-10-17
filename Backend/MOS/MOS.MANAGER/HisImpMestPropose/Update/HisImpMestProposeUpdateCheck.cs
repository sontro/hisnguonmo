using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMedicalContract;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMestPropose.Update
{
    class HisImpMestProposeUpdateCheck : BusinessBase
    {
        internal HisImpMestProposeUpdateCheck()
            : base()
        {

        }

        internal HisImpMestProposeUpdateCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool VerifyRequireField(HisImpMestProposeSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.WorkingRoomId <= 0) throw new ArgumentNullException("data.WorkingRoomId");
                if (data.SupplierId <= 0) throw new ArgumentNullException("data.SupplierId");
                if (!IsNotNullOrEmpty(data.ImpMestIds)) throw new ArgumentNullException("data.ImpMestIds");
                if (!data.Id.HasValue || data.Id.Value <= 0) throw new ArgumentNullException("data.Id");
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsValidMedicalContract(List<HIS_IMP_MEST> impMests, HIS_IMP_MEST_PROPOSE impMestPropose)
        {
            bool valid = true;
            try
            {
                List<HIS_IMP_MEST> invalidMedicalContracts = impMests.Where(o => o.MEDICAL_CONTRACT_ID != impMestPropose.MEDICAL_CONTRACT_ID).ToList();
                if (IsNotNullOrEmpty(invalidMedicalContracts))
                {
                    if (impMestPropose.MEDICAL_CONTRACT_ID.HasValue)
                    {
                        HIS_MEDICAL_CONTRACT medicalContract = new HisMedicalContractGet().GetById(impMestPropose.MEDICAL_CONTRACT_ID.Value);

                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisImpMestPropose_ChiChoPhepBoSungCacPhieuNhapThuocHopDong, medicalContract.MEDICAL_CONTRACT_CODE, medicalContract.MEDICAL_CONTRACT_NAME);
                        return false;
                    }
                    else if (!impMestPropose.MEDICAL_CONTRACT_ID.HasValue)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisImpMestPropose_ChiChoPhepBoSungCacPhieuNhapKhongThuocHopDongNao);
                        return false;
                    }
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
