using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class NumOrderBlockChooserADO
    {
        public List<MOS.EFMODEL.DataModels.V_HIS_ROOM> ListRoom { get; set; }

        public long? DefaultRoomId { get; set; }
        public bool DisableRoom { get; set; }
        public long? DefaultDate { get; set; }
        public bool DisableDate { get; set; }
        public long? SelectedTime { get; set; }

        /// <summary>
        /// Thứ tự param ROOM_ID, DATE, NUM_ORDER_BLOCK_ID
        /// </summary>
        public Action<ResultChooseNumOrderBlockADO> DelegateChooseData { get; set; }
    }
    public class ResultChooseNumOrderBlockADO
    {
        public long RoomId { get; set; }
        public long Date { get; set; }
        public MOS.SDO.HisNumOrderBlockSDO NumOrderBlock { get; set; }
    }
}
