using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using SDA.MANAGER.Core.SdaLanguage.EventLog;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaLanguage.Update
{
    class SdaLanguageUpdateBehaviorEv : BeanObjectBase, ISdaLanguageUpdate
    {
        SDA_LANGUAGE current;
        SDA_LANGUAGE entity;
        List<SDA_LANGUAGE> list;

        internal SdaLanguageUpdateBehaviorEv(CommonParam param, SDA_LANGUAGE data)
            : base(param)
        {
            entity = data;
        }

        public SdaLanguageUpdateBehaviorEv(CommonParam param, List<SDA_LANGUAGE> data)
            : base(param)
        {
            list = data;
        }

        bool ISdaLanguageUpdate.Run()
        {
            bool result = false;
            try
            {
                if (IsNotNull(entity))
                {
                    result = Check() && DAOWorker.SdaLanguageDAO.Update(entity);
                    if (result)
                    {
                        SdaLanguageEventLogUpdate.Log(current, entity);
                    }
                }
                else if (IsNotNullOrEmpty(list))
                {
                    result = CheckList() && DAOWorker.SdaLanguageDAO.UpdateList(list);
                    if (result)
                    {
                        SdaLanguageEventLogUpdate.Log(current, list);
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

        bool CheckList()
        {
            bool result = true;
            try
            {
                result = result && SdaLanguageCheckVerifyValidData.Verify(param, list);
                result = result && SdaLanguageCheckVerifyIsUnlock.Verify(param, list);
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
                result = result && SdaLanguageCheckVerifyValidData.Verify(param, entity);
                result = result && SdaLanguageCheckVerifyIsUnlock.Verify(param, entity.ID, ref current);
                result = result && SdaLanguageCheckVerifyExistsCode.Verify(param, entity.LANGUAGE_CODE, entity.ID);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
