using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LIS.RocheV3
{
    /// <summary>
    /// Ban tin mau:
    /// MSH|^~\&|INFINITY^2.2|Roche Diagnostics|LIS|TPH|20201226221819||OML^O21^OML_O21|46D0EA8A-1D9E-446D-888C-F4DE062CD391|P|2.5|||AL|ER
    /// PID|1||2011040178||^BÙI THỊ LAN ANH|^BUI THI LAN ANH|19690701|F|||Thụy Phương, Bắc Từ Liêm, Hà Nội, Việt Nam^^^^||^^^^^^^^^^^0919700275
    /// NTE|1||
    /// PV1|1|O|2011230080^^||||||||||||||||^^^^^^^^
    /// ORC|NW|9388491|9388491^||||||20210130000619|||ntht^bs.nguyễn thị hoài thương|K01^Tim mạch 1|CS1^ bệnh viện tim hà nội CS1|TKM^Tiền mãn kinh|UT18|DV^Dịch vụ
    /// NTE|1||
    /// TQ1|1||||||||S^Cấp cứu
    /// OBR|1|||CBC^Tổng phân tích tế bào máu|||||||||||||||||||||
    /// NTE|1||
    /// OBR|2|||UREA^Urea (Máu)|||||||||||||||||||||
    /// NTE|1||
    /// DG1|1||^H20 - Viêm mống thể mi

    /// </summary>
    public class RocheHl7OrderMessage : RocheHl7BaseMessage
    {
        /// <summary>
        /// 0: 20201226221819: Ngay tao goi tin
        /// 1: 46D0EA8A-1D9E-446D-888C-F4DE062CD391: Id goi tin
        /// 2: Thong tin BN
        /// 3: Thong tin chi dinh
        /// </summary>
        private const string  MESSAGE_FORMAT = "MSH|^~\\&|INFINITY^2.2|Roche Diagnostics|LIS|TPH|{0}||OML^O21^OML_O21|{1}|P|2.5|||AL|ER"
            + "\r\n"
            + "{2}"
            + "\r\n"
            + "{3}"
            + "\r\n"
            ;

        private RocheHl7PatientData patientData;
        private RocheHl7OrderData orderData;

        public RocheHl7OrderMessage(RocheHl7PatientData patientData, RocheHl7OrderData order)
        {
            this.patientData = patientData;
            this.orderData = order;
            this.Type = Hl7MessageType.ORDER;
        }

        public override string ToMessage()
        {
            long generatedTime = Inventec.Common.DateTime.Get.Now().Value;
            string id = Guid.NewGuid().ToString().ToUpper();
            string patient = patientData.ToString();
            string order = orderData.ToString();

            return string.Format(MESSAGE_FORMAT, generatedTime, id, patient, order);
        }

        public override void FromMessage(string message)
        {
            throw new NotImplementedException();
        }
    }
}
