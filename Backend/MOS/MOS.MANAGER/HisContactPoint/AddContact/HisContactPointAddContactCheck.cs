using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisContact;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisContactPoint.AddContact
{
    partial class HisContactPointAddContactCheck : BusinessBase
    {		
        internal HisContactPointAddContactCheck()
            : base()
        {
        }

        internal HisContactPointAddContactCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool IsValidData(HisContactSDO data)
        {
            bool valid = true;
            try
            {
                if (data.ContactPointId <= 0)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("ContactPointId ko hop le");
                    return false;
                }
                if (data.ContactTime <= 0)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("ContactTime ko hop le");
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

        internal bool IsValidContactLevel(HisContactSDO data, ref HIS_CONTACT_POINT patient)
        {
            bool valid = true;
            try
            {
                patient = new HisContactPointGet().GetById(data.ContactPointId);

                if (patient == null)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("data.ContactPointId ko hop le");
                    return false;
                }

                if (patient.CONTACT_LEVEL.HasValue && data.CONTACT_LEVEL.HasValue && Math.Abs(patient.CONTACT_LEVEL.Value - data.CONTACT_LEVEL.Value) > 1)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisContactPoint_GiaTriPhanLoaiCuaNguoiTiepXucCanNhoHoacLonHon1);
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
    }
}
