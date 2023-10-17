using Inventec.Common.Adapter;
using Inventec.Common.LocalStorage.SdaConfig;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.Filter;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisRoleUser.Base
{
    class HisImpMestTypeCFG
    {
        // nhập khác
        private static long impMestTypeIdOTHER;
        public static long IMP_MEST_TYPE_ID__OTHER
        {
            get
            {
                if (impMestTypeIdOTHER == 0)
                {
                    impMestTypeIdOTHER = GetId(ConfigKeys.DBCODE__HIS_RS__HIS_IMP_MEST_TYPE__IMP_MEST_TYPE_CODE__OTHER);
                }
                return impMestTypeIdOTHER;
            }
            set
            {
                impMestTypeIdOTHER = value;
            }
        }

        // nhập kiểm kê
        private static long impMestTypeIdINVE;
        public static long IMP_MEST_TYPE_ID__INVE
        {
            get
            {
                if (impMestTypeIdINVE == 0)
                {
                    impMestTypeIdINVE = GetId(ConfigKeys.DBCODE__HIS_RS__HIS_IMP_MEST_TYPE__IMP_MEST_TYPE_CODE__INVE);
                }
                return impMestTypeIdINVE;
            }
            set
            {
                impMestTypeIdINVE = value;
            }
        }

        // nhập đầu kỳ
        private static long impMestTypeIdINIT;
        public static long IMP_MEST_TYPE_ID__INIT
        {
            get
            {
                if (impMestTypeIdINIT == 0)
                {
                    impMestTypeIdINIT = GetId(ConfigKeys.DBCODE__HIS_RS__HIS_IMP_MEST_TYPE__IMP_MEST_TYPE_CODE__INIT);
                }
                return impMestTypeIdINIT;
            }
            set
            {
                impMestTypeIdINIT = value;
            }
        }

        private static long impMestTypeIdAGGR;
        public static long IMP_MEST_TYPE_ID__AGGR
        {
            get
            {
                if (impMestTypeIdAGGR == 0)
                {
                    impMestTypeIdAGGR = GetId(ConfigKeys.DBCODE__HIS_RS__HIS_IMP_MEST_TYPE__IMP_MEST_TYPE_CODE__AGGR);
                }
                return impMestTypeIdAGGR;
            }
            set
            {
                impMestTypeIdAGGR = value;
            }
        }

        private static long impMestTypeIdCHMS;
        public static long IMP_MEST_TYPE_ID__CHMS
        {
            get
            {
                if (impMestTypeIdCHMS == 0)
                {
                    impMestTypeIdCHMS = GetId(ConfigKeys.DBCODE__HIS_RS__HIS_IMP_MEST_TYPE__IMP_MEST_TYPE_CODE__CHMS);
                }
                return impMestTypeIdCHMS;
            }
            set
            {
                impMestTypeIdCHMS = value;
            }
        }

        private static long impMestTypeIdMANU;
        public static long IMP_MEST_TYPE_ID__MANU
        {
            get
            {
                if (impMestTypeIdMANU == 0)
                {
                    impMestTypeIdMANU = GetId(ConfigKeys.DBCODE__HIS_RS__HIS_IMP_MEST_TYPE__IMP_MEST_TYPE_CODE__MANU);
                }
                return impMestTypeIdMANU;
            }
            set
            {
                impMestTypeIdMANU = value;
            }
        }

        private static long impMestTypeIdMOBA;
        public static long IMP_MEST_TYPE_ID__MOBA
        {
            get
            {
                if (impMestTypeIdMOBA == 0)
                {
                    impMestTypeIdMOBA = GetId(ConfigKeys.DBCODE__HIS_RS__HIS_IMP_MEST_TYPE__IMP_MEST_TYPE_CODE__MOBA);
                }
                return impMestTypeIdMOBA;
            }
            set
            {
                impMestTypeIdMOBA = value;
            }
        }

        private static long GetId(string code)
        {
            long result = 0;
            try
            {
                CommonParam param = new CommonParam();
                SDA_CONFIG config = ConfigLoader.dictionaryConfig[code];
                if (config == null) throw new ArgumentNullException(code);
                string value = String.IsNullOrEmpty(config.VALUE) ? (String.IsNullOrEmpty(config.DEFAULT_VALUE) ? "" : config.DEFAULT_VALUE) : config.VALUE;
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(code);
                HisImpMestTypeFilter filter = new HisImpMestTypeFilter();
                filter.IMP_MEST_TYPE_CODE = value;
                var apiresult = new BackendAdapter(param).Get<List<MOS.EFMODEL.DataModels.HIS_IMP_MEST_TYPE>>(ApiConsumer.HisRequestUriStore.HIS_IMP_MEST_TYPE_GET, ApiConsumer.ApiConsumers.MosConsumer, filter, param);
                var data = apiresult.FirstOrDefault();
                if (!(data != null && data.ID > 0)) throw new ArgumentNullException(code + LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => config), config));
                result = data.ID;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = 0;
            }
            return result;
        }
    }
}
