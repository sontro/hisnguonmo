using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AggrExpMestDetail.ADO
{
    public class ExpMestMatyMetyReqSDODetail : V_HIS_EXP_MEST_MEDICINE
    {
        public bool IS_MEDICINE { get; set; }
        public long MEDI_MATE_TYPE_ID { get; set; }
        public long MEDI_MATE_ID { get; set; }

        public ExpMestMatyMetyReqSDODetail()
        {

        }

        public ExpMestMatyMetyReqSDODetail(ExpMestMatyMetyReqSDODetail medicine)
        {
            if (medicine != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<ExpMestMatyMetyReqSDODetail>(this, medicine);
            }
        }

        public ExpMestMatyMetyReqSDODetail(V_HIS_EXP_MEST_MEDICINE _data)
        {
            try
            {
                if (_data != null)
                {
                    //System.Reflection.PropertyInfo[] pi = Inventec.Common.Repository.Properties.Get<V_HIS_EXP_MEST_MEDICINE>();

                    //foreach (var item in pi)
                    //{
                    //    item.SetValue(this, (item.GetValue(_data)));
                    //}

                    Inventec.Common.Mapper.DataObjectMapper.Map<ExpMestMatyMetyReqSDODetail>(this, _data);
                    this.IS_MEDICINE = true;
                    this.MEDI_MATE_TYPE_ID = _data.MEDICINE_TYPE_ID;
                    this.MEDI_MATE_ID = _data.MEDICINE_ID ?? 0;
                }
            }

            catch (Exception)
            {

            }
        }

        public ExpMestMatyMetyReqSDODetail(V_HIS_EXP_MEST_MATERIAL _data)
        {
            try
            {
                if (_data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<ExpMestMatyMetyReqSDODetail>(this, _data);

                    this.CK_IMP_MEST_MEDICINE_ID = _data.CK_IMP_MEST_MATERIAL_ID;
                    this.EXP_MEST_METY_REQ_ID = _data.EXP_MEST_MATY_REQ_ID;
                    this.MEDICINE_TYPE_CODE = _data.MATERIAL_TYPE_CODE;
                    this.MEDICINE_TYPE_NAME = _data.MATERIAL_TYPE_NAME;
                    this.MEDICINE_TYPE_NUM_ORDER = _data.MATERIAL_TYPE_NUM_ORDER;
                    this.MEDICINE_NUM_ORDER = _data.MATERIAL_NUM_ORDER;
                    this.TDL_MEDICINE_TYPE_ID = _data.TDL_MATERIAL_TYPE_ID;
                    this.IS_MEDICINE = false;
                    this.MEDI_MATE_TYPE_ID = _data.MATERIAL_TYPE_ID;
                    this.MEDI_MATE_ID = _data.MATERIAL_ID ?? 0;
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
