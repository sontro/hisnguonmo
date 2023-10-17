using Inventec.Common.Logging;
using Inventec.Core;
using SDA.LibraryBug;
using SDA.LibraryMessage;
using SDA.MANAGER.Base;
using SDA.SDO;
using SDA.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDA.MANAGER.Core.SdaLicense.Decode
{
    class SdaLicenseDecodeBehaviorEv : BeanObjectBase, ISdaLicenseDecode
    {
        string entity;

        internal SdaLicenseDecodeBehaviorEv(CommonParam param, string data)
            : base(param)
        {
            entity = data;
        }

        object ISdaLicenseDecode.Run()
        {
            SdaLicenseSDO result = null;
            try
            {
                if (this.Check())
                {
                    result = RsaHash.GetLicense(this.entity);
                }
            }
            catch (Exception ex)
            {
                MessageUtil.SetMessage(param, Message.Enum.SdaLicense_MaKichHoatKhongHopLe);
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        bool Check()
        {
            bool valid = true;
            try
            {
                if (entity == null) throw new ArgumentNullException("data");
            }
            catch (ArgumentNullException ex)
            {
                MessageUtil.SetMessage(param, Message.Enum.Common__DuLieuKhongHopLe);
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
    }
}
