using HIS.Desktop.Plugins.CallPatientVer5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.CallPatient
{
    class CallPatientRunSpeech
    {
        internal static void CallPatientByNumOder(string patientName, long numOder, string examRoomName)
        {
            try
            {
                string moiBenhNhanStr = WaitingScreenCFG.CALL_PATIENT_MOI_BENH_NHAN_STR;
                string coSoSttStr = WaitingScreenCFG.CALL_PATIENT_CO_STT_STR;
                string denStr = WaitingScreenCFG.CALL_PATIENT_DEN_STR;

                Inventec.Speech.SpeechPlayer.SpeakSingle(moiBenhNhanStr);
                Inventec.Speech.SpeechPlayer.Speak(patientName);
                Inventec.Speech.SpeechPlayer.SpeakSingle(coSoSttStr);
                Inventec.Speech.SpeechPlayer.Speak(numOder);
                Inventec.Speech.SpeechPlayer.SpeakSingle(denStr);
                Inventec.Speech.SpeechPlayer.SpeakSingle(examRoomName);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
