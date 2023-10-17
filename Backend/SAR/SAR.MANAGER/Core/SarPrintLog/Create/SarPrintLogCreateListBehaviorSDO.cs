using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using SAR.MANAGER.Core.Check;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAR.MANAGER.Core.SarPrintLog.Create
{
    class SarPrintLogCreateListBehaviorSDO : BeanObjectBase, ISarPrintLogCreate
    {
        private List<SDO.SarPrintLogSDO> entitys;

        public SarPrintLogCreateListBehaviorSDO(Inventec.Core.CommonParam param, List<SDO.SarPrintLogSDO> datas)
            : base(param)
        {
            this.entitys = datas;
        }

        bool ISarPrintLogCreate.Run()
        {
            bool result = false;
            try
            {
                if (entitys == null || entitys.Count == 0)
                    throw new ArgumentNullException("List SdaEventLogSDO is null");
                foreach (var item in entitys)
                {
                    SAR_PRINT_LOG raw = new SAR_PRINT_LOG();
                    raw.DATA_CONTENT = Inventec.Common.String.CountVi.SubStringVi(item.DataContent, 2000);
                    if (String.IsNullOrEmpty(item.LogginName))
                    {
                        raw.LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                        raw.CREATOR = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    }
                    else
                    {
                        raw.CREATOR = item.LogginName;
                        raw.LOGINNAME = item.LogginName;
                    }
                    raw.PRINT_TIME = item.PrintTime;
                    raw.PRINT_TYPE_CODE = item.PrintTypeCode;
                    raw.PRINT_TYPE_NAME = item.PrintTypeName;
                    raw.MODIFIER = raw.CREATOR;
                    raw.IP = item.Ip;

                    if (Check(raw))
                    {
                        bool success = DAOWorker.SarPrintLogDAO.Create(raw);
                        if (!success)
                        {
                            Inventec.Common.Logging.LogSystem.Error("Tao printlog that bai. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => raw), raw));
                        }
                    }
                }

                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        bool Check(SAR_PRINT_LOG raw)
        {
            bool result = true;
            try
            {
                result = result && SarPrintLogCheckVerifyValidData.Verify(param, raw);
                result = result && IsNotNull(raw.PRINT_TYPE_CODE);
                result = result && IsNotNull(raw.LOGINNAME);
                result = result && IsNotNull(raw.UNIQUE_CODE);
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
