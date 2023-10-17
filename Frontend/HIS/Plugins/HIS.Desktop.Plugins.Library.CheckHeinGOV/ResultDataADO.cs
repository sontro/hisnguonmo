using His.Bhyt.InsuranceExpertise.LDO;
using Inventec.Common.QrCodeBHYT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.CheckHeinGOV
{
    public class ResultDataADO
    {
        public ResultDataADO() { }

        public ResultHistoryLDO ResultHistoryLDO { get; set; }
        public HeinCardData HeinCardData { get; set; }
        public bool IsShowQuestionWhileChangeHeinTime__Choose { get; set; }
        public bool IsToDate { get; set; }
        public bool IsAddress { get; set; }
        public bool SuccessWithoutMessage { get; set; }
        public bool IsThongTinNguoiDungThayDoiSoVoiCong__Choose { get; set; }
        public ChiTietKCBLDO ChiTietKCBLDO { get; set; }
    }
}
