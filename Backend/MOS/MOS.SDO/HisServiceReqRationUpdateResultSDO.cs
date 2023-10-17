using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisServiceReqRationUpdateResultSDO
    {
        public HIS_SERVICE_REQ ServiceReq { get; set; } //  Thông tin y lệnh suất ăn vừa sửa.
        public List<HIS_SERE_SERV_RATION> SereServRations { get; set; } //  Danh sách các suất ăn trong y lệnh
    }
}
