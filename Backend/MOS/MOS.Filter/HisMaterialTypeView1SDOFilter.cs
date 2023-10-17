using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Filter
{
    public class HisMaterialTypeView1SDOFilter
    {
        public long? PARENT_ID { get; set; }
        public bool IS_BUSINESS { get; set; }

        /// <summary>
        /// Danh sach truong du lieu can tra ve
        /// </summary>
        public List<string> ColumnParams { get; set; }
    }
}
