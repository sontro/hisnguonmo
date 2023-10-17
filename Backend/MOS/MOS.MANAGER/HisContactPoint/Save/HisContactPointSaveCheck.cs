using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.UTILITY;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisContactPoint.Save
{
    partial class HisContactPointSaveCheck : BusinessBase
    {		
        internal HisContactPointSaveCheck()
            : base()
        {

        }

        internal HisContactPointSaveCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool IsValidData(HIS_CONTACT_POINT data)
        {
            bool valid = true;
            try
            {
                if (data.CONTACT_TYPE != IMSys.DbConfig.HIS_RS.HIS_CONTACT_POINT.CONTACT_TYPE__BN
                    && data.CONTACT_TYPE != IMSys.DbConfig.HIS_RS.HIS_CONTACT_POINT.CONTACT_TYPE__NV
                    && data.CONTACT_TYPE != IMSys.DbConfig.HIS_RS.HIS_CONTACT_POINT.CONTACT_TYPE__KHAC)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("CONTACT_TYPE ko hop le");
                    return false;
                }

                if (data.CONTACT_TYPE == IMSys.DbConfig.HIS_RS.HIS_CONTACT_POINT.CONTACT_TYPE__BN && !data.PATIENT_ID.HasValue)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisContactPoint_ChuaChonBenhNhan);
                    return false;
                }

                if (data.CONTACT_TYPE == IMSys.DbConfig.HIS_RS.HIS_CONTACT_POINT.CONTACT_TYPE__NV && !data.EMPLOYEE_ID.HasValue)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisContactPoint_ChuaChonNhanVien);
                    return false;
                }
                if (data.CONTACT_TYPE == IMSys.DbConfig.HIS_RS.HIS_CONTACT_POINT.CONTACT_TYPE__KHAC
                    && string.IsNullOrWhiteSpace(data.CONTACT_POINT_OTHER_TYPE_NAME))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisContactPoint_ChuaNhapTenLoaiDoiTuongKhac);
                    return false;
                }
                if (data.CONTACT_TYPE == IMSys.DbConfig.HIS_RS.HIS_CONTACT_POINT.CONTACT_TYPE__KHAC
                    && ((string.IsNullOrWhiteSpace(data.LAST_NAME) && string.IsNullOrWhiteSpace(data.FIRST_NAME)) || !data.DOB.HasValue || !data.GENDER_ID.HasValue))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisContactPoint_ChuaNhapThongTinNguoiBenh);
                    return false;
                }
                if (!data.CONTACT_LEVEL.HasValue)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisContactPoint_ChuaNhapThongTinPhanLoai);
                    return false;
                }
                if (data.CONTACT_LEVEL.Value < 0)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisContactPoint_GiaTriPhanLoaiNhoHon0);
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

        internal bool IsNotExists(HIS_CONTACT_POINT data)
        {
            bool valid = true;
            try
            {
                if (data.EMPLOYEE_ID.HasValue || data.PATIENT_ID.HasValue)
                {
                    HisContactPointFilterQuery filter = new HisContactPointFilterQuery();
                    filter.EMPLOYEE_ID = data.EMPLOYEE_ID;
                    filter.PATIENT_ID = data.PATIENT_ID;
                    filter.ID__NOT_EQUAL = data.ID;
                    List<HIS_CONTACT_POINT> exists = new HisContactPointGet().Get(filter);
                    if (IsNotNullOrEmpty(exists))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.DuLieuDaTonTaiTrenHeThong);
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
