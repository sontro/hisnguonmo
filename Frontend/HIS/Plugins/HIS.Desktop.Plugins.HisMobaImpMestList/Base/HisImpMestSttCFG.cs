using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Common.LocalStorage.SdaConfig;
using SDA.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.HisMobaImpMestList.Base
{
    class HisImpMestSttCFG
    {
        private static long impMestSttIdDaImported;
        public static long IMP_MEST_STT_ID__IMPORTED
        {
            get
            {
                if (impMestSttIdDaImported == 0)
                {
                    impMestSttIdDaImported = GetId(ConfigKeys.DBCODE__HIS_RS__HIS_IMP_MEST_STT__IMP_MEST_STT_CODE__IMPORTED);
                }
                return impMestSttIdDaImported;
            }
            set
            {
                impMestSttIdDaImported = value;
            }
        }

        private static long impMestSttIdApproved;
        public static long IMP_MEST_STT_ID__APPROVED
        {
            get
            {
                if (impMestSttIdApproved == 0)
                {
                    impMestSttIdApproved = GetId(ConfigKeys.DBCODE__HIS_RS__HIS_IMP_MEST_STT__IMP_MEST_STT_CODE__APPROVED);
                }
                return impMestSttIdApproved;
            }
            set
            {
                impMestSttIdApproved = value;
            }
        }

        private static long impMestSttIdRejected;
        public static long IMP_MEST_STT_ID__REJECTED
        {
            get
            {
                if (impMestSttIdRejected == 0)
                {
                    impMestSttIdRejected = GetId(ConfigKeys.DBCODE__HIS_RS__HIS_IMP_MEST_STT__IMP_MEST_STT_CODE__REJECTED);
                }
                return impMestSttIdRejected;
            }
            set
            {
                impMestSttIdRejected = value;
            }
        }

        private static long impMestSttIdUnapproved;
        public static long IMP_MEST_STT_ID__UNAPPROVED
        {
            get
            {
                if (impMestSttIdUnapproved == 0)
                {
                    impMestSttIdUnapproved = GetId(ConfigKeys.DBCODE__HIS_RS__HIS_IMP_MEST_STT__IMP_MEST_STT_CODE__REQUEST);
                }
                return impMestSttIdUnapproved;
            }
            set
            {
                impMestSttIdUnapproved = value;
            }
        }

        private static long impMestSttIdDraft;
        public static long IMP_MEST_STT_ID__DRAFT
        {
            get
            {
                if (impMestSttIdDraft == 0)
                {
                    impMestSttIdDraft = GetId(ConfigKeys.DBCODE__HIS_RS__HIS_IMP_MEST_STT__IMP_MEST_STT_CODE__DRAFT);
                }
                return impMestSttIdDraft;
            }
            set
            {
                impMestSttIdDraft = value;
            }
        }

        private static long GetId(string code)
        {
            long result = 0;
            try
            {
                SDA_CONFIG config = ConfigLoader.dictionaryConfig[code];
                if (config == null) throw new ArgumentNullException(code);
                string value = String.IsNullOrEmpty(config.VALUE) ? (String.IsNullOrEmpty(config.DEFAULT_VALUE) ? "" : config.DEFAULT_VALUE) : config.VALUE;
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(code);

                var data = HIS.Desktop.Plugins.HisMobaImpMestList.Base.GlobalStore.HisImpMestStts.FirstOrDefault(o => o.IMP_MEST_STT_CODE == value);
                if (!(data != null && data.ID > 0)) throw new ArgumentNullException(code + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => config), config));
                result = data.ID;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = 0;
            }
            return result;
        }
    }
}
