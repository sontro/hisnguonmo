using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.DelegateRegister
{
    public interface IRegisterAppConfig
    {       
        MOS.EFMODEL.DataModels.HIS_PATIENT_TYPE GetPatientTypeDefault();
        long GetAlertExpriedTimeHeinCardBhyt();
        long GetCheDoHienThiNoiLamViecManHinhDangKyTiepDon();
        long GetTiepDon_HienThiMotSoThongTinThemBenhNhan();
        string GetDangKyTiepDonThuTienSau();
        long GetDangKyTiepDonThoiGianLoadDanhSachPhongKham();
        long GetDangKyTiepDonHienThiThongBaoTimDuocBenhNhan();
        string GetDangKyTiepDonGoiBenhNhanBangCPA();
        long GetCheDoTuDongFillDuLieuDiaChiGhiTrenTheVaoODiaChiBenhNhanHayKhong();
        string GetInsuranceExpertiseCheckHeinConfig();

        /// <summary>
        /// Đặt là 1 nếu chọn chế độ hiển thị để xem xong rồi in, đặt là 2 nếu chọn chế độ in ngay không cần xem
        /// </summary>
        long GetCheDoInPhieuDangKyDichVuKhamBenh();
        string GetOweTypeDefault();
        long GetCheDoTuDongCheckThongTinTheBHYT();

        /// <summary>
        /// Cấu hình tự động fill yêu cầu - phòng khám gần nhất khi tìm bệnh nhân cũ
        /// Đặt 1 là tự động fill
        /// Mặc định là không tự động
        /// </summary>
        string GetIsAutoFillDataRecentServiceRoom();

        string GetIsDangKyQuaTongDai();
        bool GetIsVisibleSomeControl();
    }
}
