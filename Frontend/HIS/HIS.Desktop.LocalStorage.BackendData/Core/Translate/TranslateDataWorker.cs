using HIS.Desktop.ApiConsumer;
using Inventec.Common.Adapter;
using Inventec.Core;
using Inventec.Desktop.Common.LanguageManager;
using Microsoft.Win32;
using SDA.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.BackendData
{
    public class TranslateDataWorker
    {
        private static SDA.EFMODEL.DataModels.SDA_LANGUAGE language;
        public static SDA.EFMODEL.DataModels.SDA_LANGUAGE Language
        {
            get
            {
                try
                {
                    if (language == null)
                    {
                        CommonParam param = new CommonParam();
                        SdaLanguageFilter filter = new SdaLanguageFilter();
                        filter.IS_ACTIVE = 1;
                        var languages = new BackendAdapter(param).Get<List<SDA.EFMODEL.DataModels.SDA_LANGUAGE>>(SdaRequestUriStore.SDA_LANGUAGE_GET, ApiConsumers.SdaConsumer, filter, param);
                        language = languages.FirstOrDefault(o => o.LANGUAGE_CODE.ToUpper() == LanguageManager.GetLanguage().ToUpper());
                    }
                    if (language == null) language = new SDA.EFMODEL.DataModels.SDA_LANGUAGE();
                }
                catch (Exception ex)
                {
                    Inventec.Common.Logging.LogSystem.Warn(ex);
                }

                return language;
            }
            set
            {
                language = value;
            }
        }
    }
}
