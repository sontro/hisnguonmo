using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.EFMODEL.DataModels;
using AutoMapper;


namespace HIS.Desktop.Plugins.DebateDiagnostic.ADO
{
    class HisDebateADO : HIS_DEBATE
    {
        public string HisDebateTimeString { get; set; }
        public string ContentTypeName { get; set; }
        public HisDebateADO(MOS.EFMODEL.DataModels.HIS_DEBATE item)
        {
            Mapper.CreateMap<MOS.EFMODEL.DataModels.HIS_DEBATE, HisDebateADO>();
            Mapper.Map<MOS.EFMODEL.DataModels.HIS_DEBATE, HisDebateADO>(item, this);
            if (this.CONTENT_TYPE == 1)
            {
                ContentTypeName = "Hội chẩn khác";
            }
            else if (this.CONTENT_TYPE == 2)
            {
                ContentTypeName = "Hội chẩn thuốc";
            }
            else
            {
                ContentTypeName = "Hội chẩn trước phẫu thuật";
            }
            this.HisDebateTimeString = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(this.DEBATE_TIME ?? 0);
        }
    }
}
