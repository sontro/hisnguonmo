using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisPrepareApprove.ADO
{
    public class MetyMatyADO : MOS.EFMODEL.DataModels.V_HIS_PREPARE_METY
    {
        public bool IsMedicine { get; set; }
        public decimal ReqAmount { get; set; }
        public decimal? ApprovalAmount { get; set; }

        public MetyMatyADO()
        { }

        public MetyMatyADO(MOS.EFMODEL.DataModels.V_HIS_PREPARE_METY _data)
        {
            try
            {
                if (_data != null)
                {
                    this.IsMedicine = true;
                    this.ApprovalAmount = _data.APPROVAL_AMOUNT;
                    this.ReqAmount = _data.REQ_AMOUNT;

                    System.Reflection.PropertyInfo[] pi = Inventec.Common.Repository.Properties.Get<MOS.EFMODEL.DataModels.V_HIS_PREPARE_METY>();

                    foreach (var item in pi)
                    {
                        item.SetValue(this, (item.GetValue(_data)));
                    }
                }

            }

            catch (Exception)
            {

            }
        }

        public MetyMatyADO(MOS.EFMODEL.DataModels.V_HIS_PREPARE_MATY _data)
        {
            try
            {
                if (_data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<MetyMatyADO>(this, _data);
                    this.IsMedicine = false;
                    this.MEDICINE_TYPE_NAME = _data.MATERIAL_TYPE_NAME;
                    this.MEDICINE_TYPE_CODE = _data.MATERIAL_TYPE_CODE;
                    this.ApprovalAmount = _data.APPROVAL_AMOUNT;
                    this.ReqAmount = _data.REQ_AMOUNT;
                    this.SERVICE_UNIT_NAME = _data.SERVICE_UNIT_NAME;
                    this.SERVICE_UNIT_CODE = _data.SERVICE_UNIT_CODE;
                    this.NATIONAL_NAME = _data.NATIONAL_NAME;
                    this.MANUFACTURER_CODE = _data.MANUFACTURER_CODE;
                    this.MANUFACTURER_NAME = _data.MANUFACTURER_NAME;
                    this.CONCENTRA = _data.CONCENTRA;
                }

            }

            catch (Exception)
            {

            }
        }
    }
}
