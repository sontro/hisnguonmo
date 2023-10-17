using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisKskDriver.Sync
{
    class HisKskDriverSyncCheck : BusinessBase
    {
        internal HisKskDriverSyncCheck()
            : base()
        {

        }

        internal HisKskDriverSyncCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool IsHasConfig(string branchCode, ref KskDriverSyncInfo syncInfo)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(HisKskDriverCFG.KSK_DRIVER_SYNC_INFO))
                {
                    syncInfo = HisKskDriverCFG.KSK_DRIVER_SYNC_INFO.FirstOrDefault(o => o.BranchCode == branchCode);
                }

                if ((String.IsNullOrWhiteSpace(SyncConfig.GATEWAY_KSK_USERNAME)
                    || String.IsNullOrWhiteSpace(SyncConfig.GATEWAY_KSK_PASSWORD)
                    || String.IsNullOrWhiteSpace(SyncConfig.GATEWAY_KSK_BASE_ADDRESS)
                    || String.IsNullOrWhiteSpace(SyncConfig.GATEWAY_KSK_API_URI))
                    && (!IsNotNull(syncInfo)
                    || String.IsNullOrWhiteSpace(syncInfo.User)
                    || String.IsNullOrWhiteSpace(syncInfo.Password)
                    || String.IsNullOrWhiteSpace(syncInfo.Url))
                    )
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisKskDriver_KhongCoCauHinhKetNoiCongDuLieuYTe);
                    valid = false;
                }

                if (!IsNotNull(syncInfo))
                {
                    syncInfo = new KskDriverSyncInfo();
                    syncInfo.BranchCode = branchCode;
                    syncInfo.User = SyncConfig.GATEWAY_KSK_USERNAME;
                    syncInfo.Password = SyncConfig.GATEWAY_KSK_PASSWORD;
                    syncInfo.Url = SyncConfig.GATEWAY_KSK_BASE_ADDRESS.Trim('/') + "/" + SyncConfig.GATEWAY_KSK_API_URI.Trim('/');
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsHasWebConfig()
        {
            bool valid = true;
            try
            {
                if (String.IsNullOrWhiteSpace(SyncConfig.GATEWAY_KSK_USERNAME)
                    || String.IsNullOrWhiteSpace(SyncConfig.GATEWAY_KSK_PASSWORD)
                    || String.IsNullOrWhiteSpace(SyncConfig.GATEWAY_KSK_BASE_ADDRESS)
                    || String.IsNullOrWhiteSpace(SyncConfig.GATEWAY_KSK_API_URI))
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisKskDriver_KhongCoCauHinhKetNoiCongDuLieuYTe);
                    valid = false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsHasCmnd(V_HIS_KSK_DRIVER data)
        {
            bool valid = true;
            try
            {
                if ((String.IsNullOrWhiteSpace(data.CMND_NUMBER) || String.IsNullOrWhiteSpace(data.CMND_PLACE) || !data.CMND_DATE.HasValue)
                    && (String.IsNullOrWhiteSpace(data.CCCD_NUMBER) || String.IsNullOrWhiteSpace(data.CCCD_PLACE) || !data.CCCD_DATE.HasValue)
                    && (String.IsNullOrWhiteSpace(data.PASSPORT_NUMBER) || String.IsNullOrWhiteSpace(data.PASSPORT_PLACE) || !data.PASSPORT_DATE.HasValue))
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisKskDriver_BenhNhanKhongCoThongTinCMND);
                    valid = false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsHasTHX(V_HIS_KSK_DRIVER data)
        {
            bool valid = true;
            try
            {
                if (String.IsNullOrWhiteSpace(data.PROVINCE_CODE) || String.IsNullOrWhiteSpace(data.DISTRICT_CODE) || String.IsNullOrWhiteSpace(data.COMMUNE_CODE))
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisKskDriver_BenhNhanKhongCoDiaChi3CapTHX);
                    valid = false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
