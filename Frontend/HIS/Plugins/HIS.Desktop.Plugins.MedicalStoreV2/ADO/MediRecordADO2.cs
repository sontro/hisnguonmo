using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicalStoreV2.ADO
{
    public class MediRecordADO2 : V_HIS_MEDI_RECORD_2
    {
        public string INTIME_SPLCONCAT { get; set; }
        public string OUTTIME_SPLCONCAT { get; set; }
        public MediRecordADO2() { }

        public MediRecordADO2(V_HIS_MEDI_RECORD_2 data)
        {
            if (data != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<MediRecordADO2>(this, data);
                if(this.IN_TIME!=null)
				{
                    var inT = this.IN_TIME.Split(',');
                    List<string> lst = new List<string>();
					for (int i = 0; i < inT.Length; i++)
					{
                        lst.Add(Inventec.Common.DateTime.Convert.TimeNumberToTimeString(Int64.Parse(inT[i])));
                    }
                    INTIME_SPLCONCAT = String.Join(", ",lst);
				}
                if (this.OUT_TIME != null)
                {
                    var inT = this.OUT_TIME.Split(',');
                    List<string> lst = new List<string>();
                    for (int i = 0; i < inT.Length; i++)
                    {
                        lst.Add(Inventec.Common.DateTime.Convert.TimeNumberToTimeString(Int64.Parse(inT[i])));
                    }
                    OUTTIME_SPLCONCAT = String.Join(", ", lst);
                }
            }
        }
    }
}
