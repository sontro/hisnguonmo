using DevExpress.XtraEditors.DXErrorProvider;
using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.LocalData;
using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RadixChangeCabinet.ADO
{
    public class MedicineADO : HisMedicineTypeInStockSDO
    {
        public decimal? AMOUNT { get; set; }
        public decimal? EXP_MEST_ID { get; set; }
        public bool IsBoSung { get; set; }

        public MedicineADO()
        {

        }

        public MedicineADO(HisMedicineTypeInStockSDO data, List<HIS_EXP_MEST_METY_REQ> _MetyReqs)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<MedicineADO>(this, data);
                    if (_MetyReqs != null && _MetyReqs.Count > 0)
                    {
                        var _dataOld = _MetyReqs.FirstOrDefault(p => p.MEDICINE_TYPE_ID == data.Id);
                        if (_dataOld != null)
                        {
                            this.AMOUNT = _dataOld.AMOUNT;
                            this.EXP_MEST_ID = _dataOld.EXP_MEST_ID;
                        }
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
