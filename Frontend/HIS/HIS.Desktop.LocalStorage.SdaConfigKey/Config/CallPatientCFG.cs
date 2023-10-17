using Inventec.Common.Logging;
using Inventec.Core;
using MOS.Filter;
using SDA.EFMODEL.DataModels;
using System;
using System.Linq;
using SDA.Filter;

namespace HIS.Desktop.LocalStorage.SdaConfigKey.Config
{
    public class CallPatientCFG
    {
        public const string CALL_PATIENT_DEN = "EXE.CALL_PATIENT.DEN";
        public const string CALL_PATIENT_CO_STT = "EXE.CALL_PATIENT.CO_STT";
        public const string CALL_PATIENT_MOI_BENH_NHAN = "EXE.CALL_PATIENT.MOI_BENH_NHAN";
        public const string CALL_PATIENT_CONG = "EXE.CALL_PATIENT.CONG";

        //private static string callPatientDen;
        //public static string CALL_PATIENT_DEN_STR
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(callPatientDen))
        //        {
        //            callPatientDen = GetName(CALL_PATIENT_DEN);
        //        }
        //        return callPatientDen;
        //    }
        //    set
        //    {
        //        callPatientDen = value;
        //    }
        //}

        //private static string callPatientCoStt;
        //public static string CALL_PATIENT_CO_STT_STR
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(callPatientCoStt))
        //        {
        //            callPatientCoStt = GetName(CALL_PATIENT_CO_STT);
        //        }
        //        return callPatientCoStt;
        //    }
        //    set
        //    {
        //        callPatientCoStt = value;
        //    }
        //}

        //private static string callPatientMoiBenhNhan;
        //public static string CALL_PATIENT_MOI_BENH_NHAN_STR
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(callPatientMoiBenhNhan))
        //        {
        //            callPatientMoiBenhNhan = GetName(CALL_PATIENT_MOI_BENH_NHAN);
        //        }
        //        return callPatientMoiBenhNhan;
        //    }
        //    set
        //    {
        //        callPatientMoiBenhNhan = value;
        //    }
        //}

        //private static string GetName(string code)
        //{
        //    string result = "";
        //    try
        //    {
        //        LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => Loader.dictionaryConfig), Loader.dictionaryConfig));
        //        LogSystem.Debug("Key=" + code);
        //        SDA.EFMODEL.DataModels.SDA_CONFIG config = Loader.dictionaryConfig[code];
        //        if (config == null) throw new ArgumentNullException(code);
        //        string value = String.IsNullOrEmpty(config.VALUE) ? (String.IsNullOrEmpty(config.DEFAULT_VALUE) ? "" : config.DEFAULT_VALUE) : config.VALUE;
        //        if (String.IsNullOrEmpty(value)) return "";
        //        result = value;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogSystem.Error(ex);
        //        result = "";
        //    }
        //    return result;
        //}
    }
}
