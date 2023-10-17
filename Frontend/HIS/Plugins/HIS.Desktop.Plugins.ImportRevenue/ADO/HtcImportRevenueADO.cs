using HTC.EFMODEL.DataModels;
using HTC.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImportRevenue.ADO
{
    public class HtcImportRevenueADO : HTC_REVENUE
    {
        public string Description { get; set; }
        public bool IsError { get; set; }

        public string YEAR_STR { get; set; }
        public DateTime? IN_DATE_TIME { get; set; }
        public DateTime? OUT_DATE_TIME { get; set; }

        public HtcImportRevenueADO() { }

        public HtcImportRevenueADO(ImportRevenueSDO data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<HtcImportRevenueADO>(this, data);
                    if (this.DOB.HasValue)
                    {
                        this.YEAR_STR = this.DOB.Value.ToString().Substring(0, 4);
                    }
                    if (this.IN_TIME.HasValue)
                    {
                        this.IN_DATE_TIME = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(this.IN_TIME.Value);
                    }
                    if (this.OUT_TIME.HasValue)
                    {
                        this.OUT_DATE_TIME = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(this.OUT_TIME.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
