using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisDataStore.HisDataStore
{
    public class HisDataStoreADO : MOS.EFMODEL.DataModels.V_HIS_DATA_STORE
    {
        public string TREATMENT_END_TYPE_NAMEs { get; set; }

        public HisDataStoreADO(MOS.EFMODEL.DataModels.V_HIS_DATA_STORE data)
        {
            try
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<HisDataStoreADO>(this, data);
                if (!String.IsNullOrWhiteSpace(data.TREATMENT_END_TYPE_IDS))
                {
                    string[] arrTreat = data.TREATMENT_END_TYPE_IDS.Split(',');
                    if (arrTreat != null && arrTreat.Length > 0)
                    {
                        foreach (var item in arrTreat)
                        {
                            long treatmentEndTypeId = 0;
                            if (Int64.TryParse(item, out treatmentEndTypeId))
                            {
                                var treatmentEndType = HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get<MOS.EFMODEL.DataModels.HIS_TREATMENT_END_TYPE>().FirstOrDefault(o => o.ID == treatmentEndTypeId);
                                if (treatmentEndType != null)
                                {
                                    this.TREATMENT_END_TYPE_NAMEs += treatmentEndType.TREATMENT_END_TYPE_NAME + ", ";
                                }
                            }

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Debug(ex);
            }
        }
    }
}
