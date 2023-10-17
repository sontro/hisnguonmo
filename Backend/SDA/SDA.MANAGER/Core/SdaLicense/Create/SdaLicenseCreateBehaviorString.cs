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

namespace SDA.MANAGER.Core.SdaLicense.Create
{
    class SdaLicenseCreateBehaviorString : BeanObjectBase, ISdaLicenseCreate
    {
        string entity;

        internal SdaLicenseCreateBehaviorString(CommonParam param, string data)
            : base(param)
        {
            entity = data;
        }

        object ISdaLicenseCreate.Run()
        {
            SDA_LICENSE result = null;
            try
            {
                if (this.Check())
                {
                    SdaLicenseSDO createData = null;
                    try
                    {
                        createData = RsaHash.GetLicense(this.entity);
                    }
                    catch (Exception)
                    {
                        MessageUtil.SetMessage(param, Message.Enum.SdaLicense_MaKichHoatKhongHopLe);
                        throw new Exception("Ma Kich Hoat Khong Hop Le");
                    }

                    if (IsNotNull(createData) && this.Check(createData))
                    {
                        result = new SDA_LICENSE();
                        result.LICENSE = this.entity;
                        result.APP_CODE = createData.AppCode;
                        result.CLIENT_CODE = createData.ClientCode;
                        result.EXPIRED_DATE = createData.ExpiredDate;

                        if (!DAOWorker.SdaLicenseDAO.Create(result))
                        {
                            throw new Exception("tao SDA_LICENSE that bai.");
                        }
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

        private bool Check(SdaLicenseSDO createData)
        {
            bool result = true;
            try
            {
                if (createData == null) throw new ArgumentNullException("data is null");
                if (String.IsNullOrWhiteSpace(createData.ClientCode)) throw new ArgumentNullException("ClientCode is null");
                if (String.IsNullOrWhiteSpace(createData.AppCode)) throw new ArgumentNullException("AppCode is null");

                SdaLicenseFilterQuery filter = new SdaLicenseFilterQuery();
                filter.APP_CODE__EXACT = createData.AppCode;
                filter.CLIENT_CODE__EXACT = createData.ClientCode;
                List<SDA_LICENSE> listData = new SdaLicenseBO().Get<List<SDA_LICENSE>>(filter);
                if (IsNotNullOrEmpty(listData))
                {
                    MessageUtil.SetMessage(param, Message.Enum.SdaLicense_TomTaiMaKichHoat, createData.AppCode, createData.ClientCode);
                    result = false;
                }
            }
            catch (ArgumentNullException ex)
            {
                MessageUtil.SetMessage(param, Message.Enum.SdaLicense_MaKichHoatKhongHopLe);
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

        bool Check()
        {
            bool result = true;
            try
            {
                if (entity == null) throw new ArgumentNullException("data");
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
