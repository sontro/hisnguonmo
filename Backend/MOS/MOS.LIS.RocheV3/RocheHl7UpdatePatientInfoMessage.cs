using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LIS.RocheV3
{
    /// <summary>
    /// Ban tin mau:
    /// MSH|^~\&|INFINITY^2.2|Roche Diagnostics|LIS|TPH|20211210090619||ADT^A31^ADT_A31|99D3EA9A-4D9E-99D-998C-F5DE2629CD899|P|2.5|||AL|ER
    /// EVN||20211210235100
    /// PID|1||040178||^BÙI THỊ LÂM ANH DŨNG|^BUI THI LAM ANH DŨNG|19690808|F|||Thuy Phuong, Bac Tu Liem, Ha Noi, Viet Nam^^^^||^^^^^^^^^^^0919700275
    /// </summary>
    public class RocheHl7UpdatePatientInfoMessage : RocheHl7BaseMessage
    {
        /// <summary>
        /// 0: 20201226221819: Ngay tao goi tin
        /// 1: 46D0EA8A-1D9E-446D-888C-F4DE062CD391: Id goi tin
        /// 2: 20201226221819: Ngay tao goi tin
        /// 3: Mã bệnh nhân
        /// 4: Ten benh nhan co dau
        /// 5: Ten benh nhan ko dau
        /// 6: Ngay sinh (vd: 19690808)
        /// 7: Gioi tinh (Nam: M, Nu: F, Khac: U)
        /// 8: Dia chi
        /// 9: So dien thoai
        /// </summary>
        private const string  MESSAGE_FORMAT = "MSH|^~\\&|INFINITY^2.2|Roche Diagnostics|LIS|TPH|{0}||ADT^A31^ADT_A31|{1}|P|2.5|||AL|ER"
            + "\r\n"
            + "EVN||{2}"
            + "\r\n"
            + "PID|1||{3}||^{4}|^{5}|{6}|{7}|||{8}^^^^||^^^^^^^^^^^{9}";

        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public RocheHl7Gender Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public RocheHl7UpdatePatientInfoMessage(string patientId, string patientName, DateTime dateOfBirth, RocheHl7Gender gender, string address, string phoneNumber)
        {
            this.PatientId = patientId;
            this.PatientName = patientName;
            this.DateOfBirth = dateOfBirth;
            this.Gender = gender;
            this.Address = address;
            this.PhoneNumber = phoneNumber;
        }

        public override string ToMessage()
        {
            long generatedTime = Inventec.Common.DateTime.Get.Now().Value;
            string id = Guid.NewGuid().ToString().ToUpper();

            return string.Format(MESSAGE_FORMAT,
                generatedTime,
                id,
                generatedTime,
                RocheHl7Util.NVL(this.PatientId),
                RocheHl7Util.NVL(this.PatientName),
                Inventec.Common.String.Convert.UnSignVNese2(RocheHl7Util.NVL(this.PatientName)),
                RocheHl7Util.DateOfBirthToString(this.DateOfBirth),
                GenderUtil.ToString(this.Gender),
                RocheHl7Util.NVL(this.Address),
                RocheHl7Util.NVL(this.PhoneNumber)
                );
        }

        public override void FromMessage(string message)
        {
            throw new NotImplementedException();
        }
    }
}
