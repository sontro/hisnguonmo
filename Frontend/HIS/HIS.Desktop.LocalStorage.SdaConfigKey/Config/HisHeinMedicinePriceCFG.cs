using Inventec.Common.Logging;
using Inventec.Core;
using MOS.Filter;
using SDA.EFMODEL.DataModels;
using System;
using System.Linq;
using SDA.Filter;
using System.Collections.Generic;
using Inventec.Common.LocalStorage.SdaConfig;

namespace HIS.Desktop.LocalStorage.SdaConfigKey.Config
{
    public class HisHeinMedicinePriceCFG
    {

        private const string LIMIT_HEIN_MEDICINE_PRICE__RIGHT_MEDI_ORG = "MOS.BHYT.LIMIT_HEIN_MEDICINE_PRICE.RIGHT_MEDI_ORG";
        private const string LIMIT_HEIN_MEDICINE_PRICE__TRAN_PATI = "MOS.BHYT.LIMIT_HEIN_MEDICINE_PRICE.TRAN_PATI";

        private static string rightMediOrgValue;
        public static string LIMIT_HEIN_MEDICINE_RIGHT_MEDI_ORG_VALUE
        {
            get
            {
                try
                {
                    if (rightMediOrgValue == null)
                    {
                        rightMediOrgValue = SdaConfigs.Get<string>(LIMIT_HEIN_MEDICINE_PRICE__RIGHT_MEDI_ORG);
                    }
                }
                catch (Exception ex)
                {
                    LogSystem.Warn(ex);
                }

                return rightMediOrgValue;
            }
            set
            {
                rightMediOrgValue = value;
            }
        }

        private static string tranPatiValue;
        public static string LIMIT_HEIN_MEDICINE_TRAN_PATI_VALUE
        {
            get
            {
                try
                {
                    if (tranPatiValue == null)
                    {
                        tranPatiValue = SdaConfigs.Get<string>(LIMIT_HEIN_MEDICINE_PRICE__TRAN_PATI);
                    }
                }
                catch (Exception ex)
                {
                    LogSystem.Warn(ex);
                }
                
                return tranPatiValue;
            }
            set
            {
                tranPatiValue = value;
            }
        }

        private static decimal limitHeinMedicinePrice__RightMediOrg;
        public static decimal LimitHeinMedicinePrice__RightMediOrg
        {
            get
            {
                try
                {
                    if (limitHeinMedicinePrice__RightMediOrg == 0)
                    {
                        limitHeinMedicinePrice__RightMediOrg = SdaConfigs.Get<decimal>(LIMIT_HEIN_MEDICINE_PRICE__RIGHT_MEDI_ORG);
                    }
                }
                catch (Exception ex)
                {
                    LogSystem.Debug(ex);
                }
                
                return limitHeinMedicinePrice__RightMediOrg;
            }
            set
            {
                limitHeinMedicinePrice__RightMediOrg = value;
            }
        }

        private static decimal limitHeinMedicinePrice__TranPati;
        public static decimal LimitHeinMedicinePrice__TranPati
        {
            get
            {
                try
                {
                    if (limitHeinMedicinePrice__TranPati == 0)
                    {
                        limitHeinMedicinePrice__TranPati = SdaConfigs.Get<decimal>(LIMIT_HEIN_MEDICINE_PRICE__TRAN_PATI);
                    }
                }
                catch (Exception ex)
                {
                    LogSystem.Debug(ex);
                }
                
                return limitHeinMedicinePrice__TranPati;
            }
            set
            {
                limitHeinMedicinePrice__TranPati = value;
            }
        }
    }
}
