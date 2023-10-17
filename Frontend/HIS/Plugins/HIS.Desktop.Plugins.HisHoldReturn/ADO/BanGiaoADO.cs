using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisHoldReturn.ADO
{
    internal class BanGiaoADO
    {
        internal BanGiaoADO()
        {
        }
        public long Value { get; set; }
        public string Text { get; set; }

        internal List<BanGiaoADO> BanGiaoADOs
        {
            get
            {
                List<BanGiaoADO> rs = new List<BanGiaoADO>();
                rs.Add(new BanGiaoADO() { Value = 1, Text = "Tất cả" });
                rs.Add(new BanGiaoADO() { Value = 2, Text = "Đang bàn giao" });
                rs.Add(new BanGiaoADO() { Value = 3, Text = "Đã bàn giao" });
                rs.Add(new BanGiaoADO() { Value = 4, Text = "Chưa trả" });
                rs.Add(new BanGiaoADO() { Value = 5, Text = "Đã trả" });
                return rs;
            }
        }

    }
}
