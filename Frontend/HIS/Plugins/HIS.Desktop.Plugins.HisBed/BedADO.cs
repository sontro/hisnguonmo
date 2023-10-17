using HIS.Desktop.ApiConsumer;
using Inventec.Common.Adapter;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisBed
{
    class BedADO : V_HIS_BED
    {
        public string IS_ACTIVE_STR { get; set; }
        //public long STT { get; set; }
        public string CREATE_TIME_STR { get; set; }
        public string MODIFY_TIME_STR { get; set; }
        //public string TREATMENT_ROOM_NAME_STR { get; set; }
        public BedADO(V_HIS_BED data)
        {
            if (data != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<BedADO>(this, data);
                this.IS_ACTIVE_STR = data.IS_ACTIVE == 1 ? "Hoạt động" : "Tạm khóa";
                this.CREATE_TIME_STR = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(data.CREATE_TIME ?? 0);
                this.MODIFY_TIME_STR = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(data.MODIFY_TIME ?? 0);

                //CommonParam param = new CommonParam();
                //HisTreatmentRoomFilter filter = new HisTreatmentRoomFilter();
                //filter.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
                //var datas = new BackendAdapter(param).Get<List<HIS_TREATMENT_ROOM>>("api/HisTreatmentRoom/Get", ApiConsumers.MosConsumer, filter, param).ToList();
                //if (!string.IsNullOrEmpty(data.TREATMENT_ROOM_ID.ToString()))
                //{
                //     datas.Where(o => o.ID == data.TREATMENT_ROOM_ID).Select(o => o.TREATMENT_ROOM_NAME);
                //    if (true)
                //    {

                //    }
                //}
               
            }
        }
    }
}
