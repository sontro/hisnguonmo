using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.CustomerRequest
{
    class DataYCKH
    {
        public List<YCKH> items { get; set; }
        public object first { get; set; }
        public bool hasMore { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public int count { get; set; }
        
    }

    class DataBinhLuan
    {
        public List<BINH_LUAN> items { get; set; }
        public object first { get; set; }
    }

    class Item
    {
        public Dictionary<string, object> data { get; set; }
    }
}
