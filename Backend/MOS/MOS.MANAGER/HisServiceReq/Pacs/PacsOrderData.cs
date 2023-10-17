using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Pacs
{
    public class PacsOrderData
    {
        /// <summary>
        /// Tat ca cac SereServ gui thanh cong.
        /// Update ServiceReq (neu khong thi khong update)
        /// Phuc vu cho Pacs Sancy
        /// null - Khong gui duoc dich vu nao (khong update)
        /// false - Co dich vu gui duoc dich vu khong
        /// true - tat ca cac dich vu deu gui duoc
        /// </summary>
        public bool? IsSuccess { get; set; }
        /// <summary>
        /// Ho so dieu tri
        /// </summary>
        public HIS_TREATMENT Treatment { get; set; }
        /// <summary>
        /// Y lenh can gui
        /// </summary>
        public HIS_SERVICE_REQ ServiceReq { get; set; }
        /// <summary>
        /// Cac sere-serv can y/c Pacs xoa
        /// </summary>
        public List<HIS_SERE_SERV> Deletes { get; set; }
        /// <summary>
        /// Cac sere-serv can y/c Pacs bo sung
        /// </summary>
        public List<HIS_SERE_SERV> Inserts { get; set; }
        /// <summary>
        /// Tat ca sere-serv hien tai (ko tinh cac ss da bi xoa) cua y lenh
        /// </summary>
        public List<HIS_SERE_SERV> Availables { get; set; }
        /// <summary>
        /// hop dong ksk
        /// </summary>
        public V_HIS_KSK_CONTRACT KskContract { get; set; }
    }
}
