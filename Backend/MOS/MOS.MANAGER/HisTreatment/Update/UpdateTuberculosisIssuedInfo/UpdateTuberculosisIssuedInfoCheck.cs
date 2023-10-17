using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisMediRecord;
using MOS.MANAGER.HisProgram;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTransaction;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Update.UpdateTuberculosisIssuedInfo
{
    class UpdateTuberculosisIssuedInfoCheck: BusinessBase
    {
        internal UpdateTuberculosisIssuedInfoCheck()
            : base()
        {
        }

        internal UpdateTuberculosisIssuedInfoCheck(CommonParam param)
            : base(param)
        {
        }

        internal bool IsNotDate(HisTreatmentTuberculosisIssuedInfoSDO data)
        {
            bool valid = true;
            try
            {
                if (!string.IsNullOrEmpty(data.TuberculosisIssuedOrgCode) && !data.TuberculosisIssuedDate.HasValue)
                {
                     MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_KhongCoThongTinNgayCapGiayXacNhanDieuTriBenhLao);
                     return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsNotId(HisTreatmentTuberculosisIssuedInfoSDO data, ref HIS_MEDI_ORG mediOrg)
        {
            bool valid = true;
            try
            {

                mediOrg = HisMediOrgCFG.DATA != null ? HisMediOrgCFG.DATA.Where(o => o.MEDI_ORG_CODE == data.TuberculosisIssuedOrgCode && o.IS_ACTIVE == Constant.IS_TRUE).FirstOrDefault() : null;
                if (mediOrg == null)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_MaCoSoKhamChuaBenhKhongTonTaiTrenHeThong);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsNotTreatmentId(HisTreatmentTuberculosisIssuedInfoSDO data, ref HIS_TREATMENT treatment)
        {
            bool valid = true;
            try
            {
                if (IsNotNull(data) && data.TreatmentId > 0)
                {   
                    treatment = new HisTreatmentGet().GetById(data.TreatmentId);
                    if (treatment == null)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_DuLieuKhongHopLe);
                        return false;
                    }
                }
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
