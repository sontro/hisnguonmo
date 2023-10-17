using Inventec.Core;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisBaby.Sync
{
    class HisBabySyncCheck: BusinessBase
    {
        internal HisBabySyncCheck()
            : base()
        {

        }

        internal HisBabySyncCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool IsHasConfig(string heinMediOrgCode, ref BabySyncInfo syncInfo)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(HisBabyCFG.BABY_SYNC_INFO))
                {
                    syncInfo = HisBabyCFG.BABY_SYNC_INFO.FirstOrDefault(o => o.HeinMediOrgCode == heinMediOrgCode);
                }

                if ((!IsNotNull(syncInfo)
                    || String.IsNullOrWhiteSpace(syncInfo.Url)
                    || String.IsNullOrWhiteSpace(syncInfo.User)
                    || String.IsNullOrWhiteSpace(syncInfo.Password))
                    )
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

        
    }
}
