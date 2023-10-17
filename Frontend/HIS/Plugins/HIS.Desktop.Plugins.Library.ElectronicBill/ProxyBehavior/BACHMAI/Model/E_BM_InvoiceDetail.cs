using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.ElectronicBill.ProxyBehavior.BACHMAI.Model
{
    class E_BM_InvoiceDetail
    {
        [JsonProperty(Order = 1)]
        public string id_master { get; set; }

        [JsonProperty(Order = 2)]
        public int idstt { get; set; }

        [JsonProperty(Order = 3)]
        public int sothutu { get; set; }

        [JsonProperty(Order = 4)]
        public string mahang { get; set; }

        [JsonProperty(Order = 5)]
        public string tenhang { get; set; }

        [JsonProperty(Order = 6)]
        public decimal dongia { get; set; }

        [JsonProperty(Order = 7)]
        public string thuesuat { get; set; }

        [JsonProperty(Order = 8)]
        public string donvitinh { get; set; }

        [JsonProperty(Order = 9)]
        public decimal soluong { get; set; }

        [JsonProperty(Order = 10)]
        public decimal thanhtien { get; set; }

        [JsonProperty(Order = 11)]
        public decimal tileckgg { get; set; }

        [JsonProperty(Order = 12)]
        public decimal thanhtienckgg { get; set; }

        [JsonProperty(Order = 13)]
        public decimal tienthue { get; set; }

        [JsonProperty(Order = 14)]
        public decimal tongtien { get; set; }

        [JsonProperty(Order = 15)]
        public decimal khuyenmai { get; set; }

        [JsonProperty(Order = 16)]
        public decimal thuettdb { get; set; }

        [JsonProperty(Order = 17)]
        public int khonghienthi { get; set; }

        [JsonProperty(Order = 18)]
        public string extend1 { get; set; }

        [JsonProperty(Order = 19)]
        public string extend2 { get; set; }

        [JsonProperty(Order = 20)]
        public string chitieu1 { get; set; }

        [JsonProperty(Order = 21)]
        public string chitieu2 { get; set; }

        [JsonProperty(Order = 22)]
        public string ghichuct { get; set; }

        [JsonProperty(Order = 23)]
        public decimal mucbhtra { get; set; }

        [JsonProperty(Order = 24)]
        public decimal bhxhtra { get; set; }
    }
}
