using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicalStore.ADO
{
    public class DataStoreADO : V_HIS_DATA_STORE
    {
        public bool CheckStore { get; set; }
        public string DataStoreNameWithCountTreatment { get; set; }
        public bool IsChuaLuu { get; set; }
        public string DATA_STORE_NAME_HIDEN { get; set; }


        public DataStoreADO()
        {
        }
        public DataStoreADO(V_HIS_DATA_STORE data)
        {
            if (data != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<DataStoreADO>(this, data);
                this.DataStoreNameWithCountTreatment = this.DATA_STORE_NAME + " (" + this.TREATMENT_COUNT + ")";
                this.DATA_STORE_NAME_HIDEN = convertToUnSign3(data.DATA_STORE_NAME) + data.DATA_STORE_NAME;
            }
        }

        public string convertToUnSign3(string s)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(s))
                    return "";

                Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
                string temp = s.Normalize(NormalizationForm.FormD);
                return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
            }
            catch
            {

            }
            return "";
        }
    }
}
