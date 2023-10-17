
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisMedicineTypeView1Filter : FilterBase
    {
        public bool? IS_LEAF { get; set; }
        public bool? IS_STOP_IMP { get; set; }

        //Neu IsTree = true thi moi thuc hien tim kiem theo "Node" (parent_id == node)
        public bool? IsTree { get; set; }
        public long? Node { get; set; }
        public long? PARENT_ID { get; set; }
        public long? MANUFACTURER_ID { get; set; }
        public long? MEDICINE_USE_FORM_ID { get; set; }
        public long? MEDICINE_LINE_ID { get; set; }
        public long? MEDICINE_GROUP_ID { get; set; }

        public short? RANK { get; set; }

        public List<long> MEDICINE_GROUP_IDs { get; set; }

        public long? MEMA_GROUP_ID { get; set; }
        public bool? IS_RAW_MEDICINE { get; set; }
        public bool? IS_BUSINESS { get; set; }
        public bool? IS_VITAMIN_A { get; set; }
        public bool? IS_VACCINE { get; set; }

        public HisMedicineTypeView1Filter()
            : base()
        {
        }
    }
}
