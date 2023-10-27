using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using ACS.SDO;
using AutoMapper;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsApplication.Create
{
    class AcsApplicationCreateSdoBehavior : BeanObjectBase, IAcsApplicationCreate
    {
        AcsApplicationWithDataSDO entity;
        ACS_APPLICATION acsApplicationDTO;
        List<ACS_APP_OTP_TYPE> appOtpTypes;

        internal AcsApplicationCreateSdoBehavior(CommonParam param, AcsApplicationWithDataSDO data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsApplicationCreate.Run()
        {
            bool result = false;
            try
            {
                Inventec.Common.Logging.LogSystem.Debug("AcsApplicationCreateSdoBehavior.1");
                if (Check())
                {
                    Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => acsApplicationDTO), acsApplicationDTO));
                    if (DAOWorker.AcsApplicationDAO.Create(acsApplicationDTO))
                    {
                        Inventec.Common.Logging.LogSystem.Debug("AcsApplicationCreateSdoBehavior.2");
                        result = true;
                        entity.ID = acsApplicationDTO.ID;
                        try
                        {
                            if (this.appOtpTypes != null && appOtpTypes.Count > 0)
                            {
                                appOtpTypes.ForEach(o => o.APPLICATION_ID = entity.ID);
                                if (!DAOWorker.AcsAppOtpTypeDAO.CreateList(appOtpTypes))
                                {
                                    param.Messages.Add("Tạo danh sách dữ liệu AcsAppOtpType theo ứng dụng thất bại");
                                }
                                else
                                    entity.AppOtpTypes = appOtpTypes;
                            }
                        }
                        catch (Exception exx)
                        {
                            Inventec.Common.Logging.LogSystem.Warn(exx);
                        }
                        Inventec.Common.Logging.LogSystem.Debug("AcsApplicationCreateSdoBehavior.3");
                    }
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

        bool Check()
        {
            bool result = true;
            try
            {
                acsApplicationDTO = new ACS_APPLICATION();
                result = result && AcsApplicationCheckVerifyValidData.Verify(param, entity, ref acsApplicationDTO);
                result = result && AcsApplicationCheckVerifyExistsCode.Verify(param, entity.APPLICATION_CODE, null);
                this.appOtpTypes = entity != null ? entity.AppOtpTypes : null;
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
