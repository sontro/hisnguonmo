using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisServiceSDO
    {
        public HIS_SERVICE HisService { get; set; }

        /// <summary>
        /// true: Update tat ca cac HisSereServ cua Service
        /// false: Chi Update cac HisSereServ ma Treatment con mo
        /// null: Khong update HisSereServ
        /// </summary>
        public bool? UpdateSereServ { get; set; }
    }
}
