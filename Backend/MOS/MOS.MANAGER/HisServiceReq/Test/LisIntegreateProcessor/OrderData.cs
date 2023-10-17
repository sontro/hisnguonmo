using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Test.LisIntegreateProcessor
{
    class OrderData
    {
        /// <summary>
        /// Y lenh can gui
        /// </summary>
        public HIS_SERVICE_REQ ServiceReq { get; set; }
        /// <summary>
        /// Cac sere-serv can y/c LIS xoa
        /// </summary>
        public List<HIS_SERE_SERV> Deletes { get; set; }
        /// <summary>
        /// Cac sere-serv can y/c LIS bo sung
        /// </summary>
        public List<HIS_SERE_SERV> Inserts { get; set; }
        /// <summary>
        /// Tat ca sere-serv hien tai (ko tinh cac ss da bi xoa) cua y lenh
        /// </summary>
        public List<HIS_SERE_SERV> Availables { get; set; }
        /// <summary>
        /// hop dong kham suc khoe
        /// </summary>
        public HIS_KSK_CONTRACT KskContract { get; set; }
    }
}
