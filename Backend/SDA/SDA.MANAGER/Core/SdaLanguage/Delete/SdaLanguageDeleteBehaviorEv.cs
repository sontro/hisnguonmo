using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using SDA.MANAGER.Core.SdaLanguage.EventLog;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaLanguage.Delete
{
    class SdaLanguageDeleteBehaviorEv : BeanObjectBase, ISdaLanguageDelete
    {
        SDA_LANGUAGE entity;

        internal SdaLanguageDeleteBehaviorEv(CommonParam param, SDA_LANGUAGE data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaLanguageDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaLanguageDAO.Truncate(entity);
                if (result) { SdaLanguageEventLogDelete.Log(entity); }
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
                result = result && SdaLanguageCheckVerifyIsUnlock.Verify(param, entity.ID, ref entity);
                result = result && CheckIsUse();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private bool CheckIsUse()
        {
            bool result = false;
            try
            {
                SdaTranslate.Get.SdaTranslateFilterQuery filter = new SdaTranslate.Get.SdaTranslateFilterQuery();
                filter.LANGUAGE_ID = entity.ID;
                var datas = new Manager.SdaTranslateManager(param).Get<List<SDA_TRANSLATE>>(filter);
                if (datas.Count <= 0)
                {
                    result = true;
                }
                else
                {
                    SDA.MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.SdaLanguage__DuLieuDangDuocSuDung);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }
    }
}
