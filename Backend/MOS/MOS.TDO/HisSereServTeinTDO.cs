using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.TDO
{
    public class HisSereServTeinTDO
    {
        /// <summary>
        /// Id dich vu chi dinh
        /// </summary>
        public long SereServId { get; set; }

        /// <summary>
        /// Ma chi so
        /// </summary>
        public string TestIndexCode { get; set; }

        /// <summary>
        /// Ten chi so
        /// </summary>
        public string TestIndexName { get; set; }

        /// <summary>
        /// Chi so thap
        /// </summary>
        public bool IsLower { get; set; }

        /// <summary>
        /// Chi so trung binh
        /// </summary>
        public bool IsNormal { get; set; }

        /// <summary>
        /// Chi so cao
        /// </summary>
        public bool IsHigher { get; set; }

        /// <summary>
        /// Chi so quan trong
        /// </summary>
        public bool IsImportant { get; set; }

        /// <summary>
        /// Ket qua
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// Ghi chu
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Don vi xet nghiem
        /// </summary>
        public string TestIndexUnitName { get; set; }
    }
}
