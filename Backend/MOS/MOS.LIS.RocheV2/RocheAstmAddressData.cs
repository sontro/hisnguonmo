using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LIS.RocheV2
{
    public class RocheAstmAddressData
    {
        private string address;
        private string administrativeDivision;

        public RocheAstmAddressData(string address, string administrativeDivision)
        {
            this.address = address;
            this.administrativeDivision = administrativeDivision;
        }

        public string ToString()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(this.address) && !string.IsNullOrWhiteSpace(this.administrativeDivision))
                {
                    return string.Format("{0}^{1}", this.address, this.administrativeDivision);
                }
                else if (!string.IsNullOrWhiteSpace(this.address))
                {
                    return this.address;
                }
                else if (!string.IsNullOrWhiteSpace(this.administrativeDivision))
                {
                    return this.administrativeDivision;
                }
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
