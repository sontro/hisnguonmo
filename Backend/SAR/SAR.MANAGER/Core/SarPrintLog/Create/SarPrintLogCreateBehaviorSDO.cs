using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAR.MANAGER.Core.SarPrintLog.Create
{
    class SarPrintLogCreateBehaviorSDO : BeanObjectBase, ISarPrintLogCreate
    {
        SDO.SarPrintLogSDO entity;
        SAR_PRINT_LOG raw;

        public SarPrintLogCreateBehaviorSDO(Inventec.Core.CommonParam param, SDO.SarPrintLogSDO data)
            : base(param)
        {
            this.entity = data;
        }

        bool ISarPrintLogCreate.Run()
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(entity.PrintTypeCode);
                valid = valid && IsNotNull(entity.LogginName);
                valid = valid && IsNotNull(entity.UniqueCode);

                if (valid)
                {
                    raw = new SAR_PRINT_LOG();
                    raw.DATA_CONTENT = Inventec.Common.String.CountVi.SubStringVi(entity.DataContent, 2000);
                    if (String.IsNullOrEmpty(entity.LogginName))
                    {
                        raw.LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        raw.CREATOR = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    }
                    else
                    {
                        raw.CREATOR = entity.LogginName;
                        raw.LOGINNAME = entity.LogginName;
                    }
                    raw.PRINT_TIME = entity.PrintTime;
                    raw.PRINT_TYPE_CODE = entity.PrintTypeCode;
                    raw.PRINT_TYPE_NAME = entity.PrintTypeName;
                    raw.MODIFIER = raw.CREATOR;
                    raw.UNIQUE_CODE = entity.UniqueCode;
                    raw.IP = entity.Ip;
                    raw.NUM_ORDER = entity.NumOrder;
                    raw.PRINT_REASON = entity.PrintReason;

                    result = Check() && DAOWorker.SarPrintLogDAO.Create(raw);
                }
                else
                {
                    SAR.MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Common__ThieuThongTinBatBuoc);
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
                result = result && SarPrintLogCheckVerifyValidData.Verify(param, raw);
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
