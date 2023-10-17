using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TrackingCreate.ADO
{
    public class TreeSereServADO : HIS_SERE_SERV
    {
        public string CONCRETE_ID__IN_SETY { get; set; }
        public string PARENT_ID__IN_SETY { get; set; }
        public string SERVICE_REQ_CODE { get; set; }
        public string SERVICE_UNIT_NAME { get; set; }
        public bool IS_THUHOI { get; set; }
        public bool IS_OUT_MEDI_MATE { get; set; }
        public bool IsNotShowMediAndMate {get; set; }
        public bool IsNotShowOutMediAndMate { get; set; }
        public long? NUM_ORDER { get; set; }
        public short? IS_EXECUTE_KIDNEY_PRES { get; set; }
        public short? PRESCRIPTION_TYPE_ID { get; set; }
        public string REQUEST_LOGINNAME { get; set; }
        public long SERVICE_REQ_STT_ID { get; set; }
        public short? IS_NO_EXECUTE { get; set; }
        public long TDL_PATIENT_DOB { get; set; }
        public string TDL_PATIENT_NAME { get; set; }
        public string TDL_PATIENT_GENDER_NAME { get; set; }
        public long LEVER { get; set; }
        public bool IS_RATION { get; set; }
        public long? TDL_USE_TIME { get; set; }
        public long? TDL_INTRUCTION_DATE { get; set; }
        public bool IsMedicinePreventive { get; set; }
        public long? TDL_TRACKING_ID { get; set; }
        public long? USED_FOR_TRACKING_ID { get; set; }
        public bool IsServiceUseForTracking { get; set; }
        public short? IS_TEMPORARY_PRES { get; set; }
        public short? IS_DISABLE { get; set; }
        public TAB_TYPE? tabType { get; set; }
        public enum TAB_TYPE
		{
            TAB_1,
            TAB_2
		}
        public TreeSereServADO() { }

        public TreeSereServADO(HIS_SERE_SERV data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<TreeSereServADO>(this, data);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
