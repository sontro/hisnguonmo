using Inventec.Common.Logging;
using Inventec.Core;
using SDA.EFMODEL.DataModels;
using SDA.LibraryMessage;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.SdaLicense.Get;
using SDA.SDO;
using SDA.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDA.MANAGER.Core.SdaLicense.Update
{
    class SdaLicenseUpdateBehaviorEvSdo : BeanObjectBase, ISdaLicenseUpdate
    {
        SdaLicenseSDO entity;

        internal SdaLicenseUpdateBehaviorEvSdo(CommonParam param, SdaLicenseSDO data)
            : base(param)
        {
            entity = data;
        }

        object ISdaLicenseUpdate.Run()
        {
            SDA_LICENSE result = null;
            try
            {
                if (this.Check())
                {
                    SdaLicenseFilterQuery filter = new SdaLicenseFilterQuery();
                    filter.APP_CODE__EXACT = this.entity.AppCode;
                    filter.CLIENT_CODE__EXACT = this.entity.ClientCode;
                    List<SDA_LICENSE> listData = new SdaLicenseBO().Get<List<SDA_LICENSE>>(filter);
                    if (!IsNotNullOrEmpty(listData))
                    {
                        MessageUtil.SetMessage(param, Message.Enum.Common__DuLieuKhongTonTai);
                        return result;
                    }

                    SdaLicenseSDO createData = null;
                    try
                    {
                        createData = RsaHash.GetLicense(this.entity.License);
                    }
                    catch (Exception)
                    {
                        MessageUtil.SetMessage(param, Message.Enum.SdaLicense_MaKichHoatKhongHopLe);
                        throw new Exception("Ma Kich Hoat Khong Hop Le");
                    }

                    result = listData.First();

                    result.LICENSE = createData.License;
                    result.APP_CODE = createData.AppCode;
                    result.CLIENT_CODE = createData.ClientCode;
                    result.EXPIRED_DATE = createData.ExpiredDate;

                    if (!DAOWorker.SdaLicenseDAO.Update(result))
                    {
                        throw new Exception("sua SDA_LICENSE that bai.");
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        bool Check()
        {
            bool result = true;
            try
            {
                if (entity == null) throw new ArgumentNullException("data is null");
                if (String.IsNullOrWhiteSpace(entity.License)) throw new ArgumentNullException("License is null");
                if (String.IsNullOrWhiteSpace(entity.AppCode)) throw new ArgumentNullException("AppCode is null");
                if (String.IsNullOrWhiteSpace(entity.ClientCode)) throw new ArgumentNullException("ClientCode is null");
            }
            catch (ArgumentNullException ex)
            {
                MessageUtil.SetMessage(param, Message.Enum.Common__DuLieuKhongHopLe);
                Inventec.Common.Logging.LogSystem.Error(ex);
                LogSystem.Warn(ex);
                result = false;
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
