using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIS.Desktop.Plugins.TestHistory.ADO
{
    public class SampleLisResultADO : V_HIS_TEST_INDEX
    {
        public SampleLisResultADO()
        {

        }

        public SampleLisResultADO(V_HIS_TEST_INDEX data)
        {
            Inventec.Common.Mapper.DataObjectMapper.Map<SampleLisResultADO>(this, data);
        }
        public long IS_PARENT { get; set; }
        public long HAS_ONE_CHILD { get; set; }
        public string VALUE_RANGE { get; set; }
        public long? LEVEL { get; set; }
        public string SERVICE_UNIT_NAME { get; set; }
        public string SAMPLE_SERVICE_STT_CODE { get; set; }
        public long? SAMPLE_SERVICE_STT_ID { get; set; }
        public string SAMPLE_SERVICE_STT_NAME { get; set; }
        public long? SAMPLE_ID { get; set; }
        public long? LIS_RESULT_ID { get; set; }
        public long? SAMPLE_SERVICE_ID { get; set; }
        public short? Item_Edit_Value { get; set; }
        public long? MACHINE_ID { get; set; }
        public long? MACHINE_ID_OLD { get; set; }
        public string PARENT_ID { get; set; }
        public string DESCRIPTION { get; set; }
        public string CHILD_ID { get; set; }
        public decimal? MIN_VALUE { get; set; }
        public decimal? MAX_VALUE { get; set; }
        public string NOTE { get; set; }
        public short? IS_RETURN_RESULT { get; set; }
        public decimal? VALUE { get; set; }
        public bool? IS_NORMAL { get; set; }
        public bool? IS_LOWER { get; set; }
        public bool? IS_HIGHER { get; set; }
        public short? IS_NO_EXECUTE { get; set; }
        public long? SERVICE_NUM_ORDER { get; set; }
        public decimal? APPOINTMENT_TIME { get; set; }
        public string OLD_VALUE { get; set; }
    }
}
