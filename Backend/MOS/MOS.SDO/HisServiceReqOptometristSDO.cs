using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisServiceReqOptometristSDO
    {
        public string ExecuteLoginname { get; set; }
        public string ExecuteUsername { get; set; }
        public long ServiceReqId { get; set; }
        public bool IsFirstOptometrist { get; set; }
        public string OptometristTime { get; set; }
        public string ForesightRightEye { get; set; }
        public string ForesightLeftEye { get; set; }
        public string ForesightRightGlassHole { get; set; }
        public string ForesightLeftGlassHole { get; set; }
        public string ForesightRightUsingGlass { get; set; }
        public string ForesightLeftUsingGlass { get; set; }
        public string ForesightUsingGlassDegreeR { get; set; }
        public string ForesightUsingGlassDegreeL { get; set; }
        public string RefactometryRightEye { get; set; }
        public string RefactometryLeftEye { get; set; }
        public string BeforeLightReflectionRight { get; set; }
        public string BeforeLightReflectionLeft { get; set; }
        public string AfterLightReflectionRight { get; set; }
        public string AfterLightReflectionLeft { get; set; }
        public string AjustableGlassForesight { get; set; }
        public string AjustableGlassForesightR { get; set; }
        public string AjustableGlassForesightL { get; set; }
        public string NearsightGlassRightEye { get; set; }
        public string NearsightGlassLeftEye { get; set; }
        public string NearsightGlassReadingDist { get; set; }
        public string NearsightGlassPupilDist { get; set; }
        public long? ReoptometristAppointment { get; set; }
        public bool IsFinish { get; set; }
    }
}
