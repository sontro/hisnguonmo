using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.TDO
{
    public class HisSereServTDO
    {
        /// <summary>
        /// Id chi dinh dich vu
        /// </summary>
        public long SereServId { get; set; }

        /// <summary>
        /// Ma y lenh
        /// </summary>
        public string ServiceReqCode { get; set; }

        /// <summary>
        /// Ma dich vu
        /// </summary>
        public string ServiceCode { get; set; }

        /// <summary>
        /// Ten dich vu
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// Id loai dich vu
        /// </summary>
        public long ServiceTypeId { get; set; }

        /// <summary>
        /// Ten loai dich vu
        /// </summary>
        public string ServiceTypeName { get; set; }

        /// <summary>
        /// Ket qua
        /// </summary>
        public string Result { get; set; }


        /// <summary>
        /// Ghi chu
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Huong dan su dung thuoc/vat tu
        /// </summary>
        public string Tutorial { get; set; }

        /// <summary>
        /// Don vi cua dich vu
        /// </summary>
        public string ServiceUnitName { get; set; }

        /// <summary>
        /// Danh sach chi so xet nghiem
        /// </summary>
        public List<HisSereServTeinTDO> TestIndexs { get; set; }
    }
}
