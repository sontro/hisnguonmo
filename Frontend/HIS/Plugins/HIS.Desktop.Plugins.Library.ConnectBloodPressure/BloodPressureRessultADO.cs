using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.ConnectBloodPressure
{
    public class BloodPressureRessultADO
    {
        public string Message { get; set; }
        /// <summary>
        /// yyyyMMddHHmmss
        /// </summary>
        public string Time { get; set; }
        public string Systolic { get; set; }
        public string Average { get; set; }
        public string Diastolic { get; set; }
        public string Pulse { get; set; }
    }
}
