namespace ACS.LibraryMessage
{
    public partial class Message
    {
        private string GetMessage(Enum enumBC)
        {
            string message = "";
            if (Language == LanguageEnum.Vietnamese)
            {
                switch (enumBC)
                {
                    case Enum.Common__DuLieuTruyenLenKhongHopLe: message = MessageViResource.Common__DuLieuTruyenLenKhongHopLe; break;
                    case Enum.Common__ThieuThongTinBatBuoc: message = MessageViResource.Common__ThieuThongTinBatBuoc; break;
                    case Enum.Common__MaDaTonTaiTrenHeThong: message = MessageViResource.Common__MaDaTonTaiTrenHeThong; break;
                    case Enum.Common__LoiCauHinhHeThong: message = MessageViResource.Common__LoiCauHinhHeThong; break;
                    case Enum.Common__DuLieuDangBiKhoa: message = MessageViResource.Common__DuLieuDangBiKhoa; break;
                    case Enum.Core_AcsRole_VaiTroKeThuaDaDuocKeThuaTuVaiTroDangChon: message = MessageViResource.Core_AcsRole_VaiTroKeThuaDaDuocKeThuaTuVaiTroDangChon; break;
                    case Enum.Core_AcsUser_DangNhapThanhCong: message = MessageViResource.Core_AcsUser_DangNhapThanhCong; break;
                    case Enum.Core_AcsUser_TaiKhoanChuaDuocThietLapQuyenTruyCapUngDung: message = MessageViResource.Core_AcsUser_TaiKhoanChuaDuocThietLapQuyenTruyCapUngDung; break;
                    case Enum.Core_AcsUser_TaiKhoanDangBiTamKhoa: message = MessageViResource.Core_AcsUser_TaiKhoanDangBiTamKhoa; break;
                    case Enum.Core_AcsUser_TenDangNhapHoacMatKhauKhongChinhXac: message = MessageViResource.Core_AcsUser_TenDangNhapHoacMatKhauKhongChinhXac; break;
                    case Enum.Core_AcsUser_TaoTaiKhoanThatBai: message = MessageViResource.Core_AcsUser_TaoTaiKhoanThatBai; break;
                    case Enum.Core_AcsUser_TaoTaiKhoanHeThongTichHopThanhCongTuyNhienKhongGanDuocUserRole: message = MessageViResource.Core_AcsUser_TaoTaiKhoanHeThongTichHopThanhCongTuyNhienKhongGanDuocUserRole; break;
                    case Enum.Core_AcsUser_TaoTaiKhoanHeThongTichHopThanhCongTuyNhienKhongGanDuocUserRoleDoRoleCodeGuiLenKhongHopLe: message = MessageViResource.Core_AcsUser_TaoTaiKhoanHeThongTichHopThanhCongTuyNhienKhongGanDuocUserRoleDoRoleCodeGuiLenKhongHopLe; break;
                    case Enum.Core_AcsUser_TenDangNhapKhongChinhXac: message = MessageViResource.Core_AcsUser_TenDangNhapKhongChinhXac; break;
                    case Enum.Core_AcsUser_EmailKhongHopLe: message = MessageViResource.Core_AcsUser_EmailKhongHopLe; break;
                    case Enum.Core_AcsUser_YeuCauCapNhatThongTinTaiKhoanDayDuTruocKhiResetMatKhauHoacGoiTongDaiCHKHDeDuocHoTro: message = MessageViResource.Core_AcsUser_YeuCauCapNhatThongTinTaiKhoanDayDuTruocKhiResetMatKhauHoacGoiTongDaiCHKHDeDuocHoTro; break;
                    case Enum.Core_AcsUser_GuiEmailXacNhanResetMatKhauDenNguoiDungThatBai: message = MessageViResource.Core_AcsUser_GuiEmailXacNhanResetMatKhauDenNguoiDungThatBai; break;
                    case Enum.Core_AcsUser_GuiSmsMaKichHoatTaiKhoanDenSDTThatBai: message = MessageViResource.Core_AcsUser_GuiSmsMaKichHoatTaiKhoanDenSDTThatBai; break;

                    case Enum.Core_AcsApplicationRole_XoaDanhSachQuyenTruyCapThanhCongChiTiet: message = MessageViResource.Core_AcsApplicationRole_XoaDanhSachQuyenTruyCapThanhCongChiTiet; break;
                    case Enum.Core_AcsApplicationRole_ThemDanhSachQuyenTruyCapThanhCongChiTiet: message = MessageViResource.Core_AcsApplicationRole_ThemDanhSachQuyenTruyCapThanhCongChiTiet; break;
                    case Enum.Core_AcsModuleRole_XoaDanhSachVaiTroChucNangThanhCongChiTiet: message = MessageViResource.Core_AcsModuleRole_XoaDanhSachVaiTroChucNangThanhCongChiTiet; break;
                    case Enum.Core_AcsModuleRole_ThemDanhSachVaiTroChucNangThanhCongChiTiet: message = MessageViResource.Core_AcsModuleRole_ThemDanhSachVaiTroChucNangThanhCongChiTiet; break;
                    case Enum.Core_AcsRole_ThemVaiTroThanhCongChiTiet: message = MessageViResource.Core_AcsRole_ThemVaiTroThanhCongChiTiet; break;
                    case Enum.Core_AcsRole_SuaVaiTroThanhCongChiTiet: message = MessageViResource.Core_AcsRole_SuaVaiTroThanhCongChiTiet; break;
                    case Enum.Core_AcsRole_XoaVaiTroThanhCongChiTiet: message = MessageViResource.Core_AcsRole_XoaVaiTroThanhCongChiTiet; break;
                    case Enum.Core_AcsRole_ThayDoiVaiTroKeThuaCuaVaiTroThanhCongChiTiet: message = MessageViResource.Core_AcsRole_ThayDoiVaiTroKeThuaCuaVaiTroThanhCongChiTiet; break;
                    case Enum.Core_AcsToken_PhienBanHienTaiDaCuPhaiNenPhienBanMoiHonDeHeThongTuongThichChayOnDinh: message = MessageViResource.Core_AcsToken_PhienBanHienTaiDaCuPhaiNenPhienBanMoiHonDeHeThongTuongThichChayOnDinh; break;
                    case Enum.Core_AcsUser_YeuCaukichHoatTaiKhoanThatBai: message = MessageViResource.Core_AcsUser_YeuCaukichHoatTaiKhoanThatBai; break;
                    case Enum.Core_AcsOtp_GuiSmsMaDoiMatKhauDenSDTThatBai: message = MessageViResource.Core_AcsOtp_GuiSmsMaDoiMatKhauDenSDTThatBai; break;
                    case Enum.Core_AcsUser_DaQuaThoiHanResetMatKhau: message = MessageViResource.Core_AcsUser_DaQuaThoiHanResetMatKhau; break;
                    case Enum.Core_AcsUser_ChuaThucHienGuiYeuCauGuiOtpDoiMatKhau: message = MessageViResource.Core_AcsUser_ChuaThucHienGuiYeuCauGuiOtpDoiMatKhau; break;
                    case Enum.Core_AcsUser_GuiYeuCauOtpDoiMatKhauTaiKhoanTruyCapGuiLenKhongHopLe: message = MessageViResource.Core_AcsUser_GuiYeuCauOtpDoiMatKhauTaiKhoanTruyCapGuiLenKhongHopLe; break;
                    case Enum.Core_AcsUser_GuiYeuCauOtpDoiMatKhauSoDienThoaiGuiLenKhongKhopVoiSoDTDaDangKy: message = MessageViResource.Core_AcsUser_GuiYeuCauOtpDoiMatKhauSoDienThoaiGuiLenKhongKhopVoiSoDTDaDangKy; break;
                    case Enum.Core_AcsUser_CapNhatThongTinTaiKhoanThatBai: message = MessageViResource.Core_AcsUser_CapNhatThongTinTaiKhoanThatBai; break;
                    case Enum.Core_AcsUser_MatKhauCuKhongChinhXac: message = MessageViResource.Core_AcsUser_MatKhauCuKhongChinhXac; break;
                    case Enum.Core_AcsOtp_GuiYeuCauCapOtpThatBai: message = MessageViResource.Core_AcsOtp_GuiYeuCauCapOtpThatBai; break;
                    case Enum.Core_AcsOtp_VerifyYeuCauCapOtpThatBai: message = MessageViResource.Core_AcsOtp_VerifyYeuCauCapOtpThatBai; break;

                    case Enum.Core_AcsAuthenRequest_AuthenRequest__ThieuMaHeThongUyQuyen: message = MessageViResource.Core_AcsAuthenRequest_AuthenRequest__ThieuMaHeThongUyQuyen; break;
                    case Enum.Core_AcsAuthenRequest_AuthenRequest__MaHeThongUyQuyenKhongHopLe: message = MessageViResource.Core_AcsAuthenRequest_AuthenRequest__MaHeThongUyQuyenKhongHopLe; break;
                    case Enum.Core_AcsAuthenRequest_AuthenRequest__ThieuKhoaBaoMat: message = MessageViResource.Core_AcsAuthenRequest_AuthenRequest__ThieuKhoaBaoMat; break;
                    case Enum.Core_AcsAuthenRequest_AuthenRequest__KhoaBaoMatKhongHopLe: message = MessageViResource.Core_AcsAuthenRequest_AuthenRequest__KhoaBaoMatKhongHopLe; break;
                    case Enum.Core_AcsAuthenRequest_AuthenRequest__ThieuThongTinNguoiYeuCau: message = MessageViResource.Core_AcsAuthenRequest_AuthenRequest__ThieuThongTinNguoiYeuCau; break;
                    case Enum.Core_AcsAuthenRequest_AuthenRequest__ThieuThongTinBoSung: message = MessageViResource.Core_AcsAuthenRequest_AuthenRequest__ThieuThongTinBoSung; break;
                    case Enum.Core_AcsAuthenRequest_AuthenRequest__ThieuThongTinMaXacThuc: message = MessageViResource.Core_AcsAuthenRequest_AuthenRequest__ThieuThongTinMaXacThuc; break;
                    case Enum.Core_AcsAuthenRequest_AuthenRequest__YeuCauXacThucKhongTonTaiHoacDaHetHan: message = MessageViResource.Core_AcsAuthenRequest_AuthenRequest__YeuCauXacThucKhongTonTaiHoacDaHetHan; break;
                    case Enum.Core_AcsAuthenRequest_AuthenRequest__KhoaCacYeuCauXacThucThatBai: message = MessageViResource.Core_AcsAuthenRequest_AuthenRequest__KhoaCacYeuCauXacThucThatBai; break;
                    case Enum.Core_AcsAuthenRequest_AuthenRequest__HeThongUyQuyenChuaDuocGanQuyen: message = MessageViResource.Core_AcsAuthenRequest_AuthenRequest__HeThongUyQuyenChuaDuocGanQuyen; break;
                    case Enum.Core_AcsOtp_OtpReqiure__MaUngDungKhongHopLe: message = MessageViResource.Core_AcsOtp_OtpReqiure__MaUngDungKhongHopLe; break;
                    case Enum.Core_AcsOtp_OtpDaHetHan: message = MessageViResource.Core_AcsOtp_OtpDaHetHan; break;

                    case Enum.Core_AcsOtp_OtpReqiure__ungDungChuaKhaiBaoMauTinNhanKichHoatTaiKhoan: message = MessageViResource.Core_AcsOtp_OtpReqiure__ungDungChuaKhaiBaoMauTinNhanKichHoatTaiKhoan; break;
                    case Enum.Core_AcsOtp_OtpReqiure__ungDungChuaKhaiBaoMauTinNhanDoiMatKhau: message = MessageViResource.Core_AcsOtp_OtpReqiure__ungDungChuaKhaiBaoMauTinNhanDoiMatKhau; break;
                    case Enum.Core_AcsOtp_OtpReqiure__ungDungChuaKhaiBaoMauTinNhanKhac: message = MessageViResource.Core_AcsOtp_OtpReqiure__ungDungChuaKhaiBaoMauTinNhanKhac; break;
                    case Enum.Core_AcsApplication__TaoUngDungThanhCongTuyNhienKhongTaoDuocDanhSachTemplateCuaUngDung: message = MessageViResource.Core_AcsApplication__TaoUngDungThanhCongTuyNhienKhongTaoDuocDanhSachTemplateCuaUngDung; break;
                    case Enum.Core_AcsApplication__SuaUngDungThanhCongTuyNhienKhongTaoDuocDanhSachTemplateCuaUngDung: message = MessageViResource.Core_AcsApplication__SuaUngDungThanhCongTuyNhienKhongTaoDuocDanhSachTemplateCuaUngDung; break;

                    case Enum.Core_AcsOtp_GuiSmsMaVerifyDangNhapDenSDTThatBai: message = MessageViResource.Core_AcsOtp_GuiSmsMaVerifyDangNhapDenSDTThatBai; break;
                    case Enum.Core_AcsOtp_TaiKhoanDangNhapChuaDuocKhaiBaoSoDienThoai: message = MessageViResource.Core_AcsOtp_TaiKhoanDangNhapChuaDuocKhaiBaoSoDienThoai; break;
                    case Enum.Core_AcsOtp_GuiSmsMaOtpVerifyDangNhapAppGuiDenSDTThatBai: message = MessageViResource.Core_AcsOtp_GuiSmsMaOtpVerifyDangNhapAppGuiDenSDTThatBai; break;
                    case Enum.Core_AcsToken_KhongChoPhepGiaHanThoiGianHieuLucCuaToken: message = MessageViResource.Core_AcsToken_KhongChoPhepGiaHanThoiGianHieuLucCuaToken; break;
                    case Enum.Common__ThemMoiThatBai: message = MessageViResource.Common__ThemMoiThatBai; break;
                    case Enum.Common__CapNhatThatBai: message = MessageViResource.Common__CapNhatThatBai; break;

                    default: message = defaultViMessage; break;
                }
            }
            else if (Language == LanguageEnum.English)
            {
                switch (enumBC)
                {
                    case Enum.Common__DuLieuTruyenLenKhongHopLe: message = MessageViResource.Common__DuLieuTruyenLenKhongHopLe; break;
                    case Enum.Common__ThieuThongTinBatBuoc: message = MessageEnResource.Common__ThieuThongTinBatBuoc; break;
                    case Enum.Common__MaDaTonTaiTrenHeThong: message = MessageEnResource.Common__MaDaTonTaiTrenHeThong; break;
                    case Enum.Common__LoiCauHinhHeThong: message = MessageEnResource.Common__LoiCauHinhHeThong; break;
                    case Enum.Common__DuLieuDangBiKhoa: message = MessageEnResource.Common__DuLieuDangBiKhoa; break;
                    case Enum.Core_AcsRole_VaiTroKeThuaDaDuocKeThuaTuVaiTroDangChon: message = MessageEnResource.Core_AcsRole_VaiTroKeThuaDaDuocKeThuaTuVaiTroDangChon; break;
                    case Enum.Core_AcsUser_TaiKhoanChuaDuocThietLapQuyenTruyCapUngDung: message = MessageEnResource.Core_AcsUser_TaiKhoanChuaDuocThietLapQuyenTruyCapUngDung; break;
                    case Enum.Core_AcsUser_TaiKhoanDangBiTamKhoa: message = MessageEnResource.Core_AcsUser_TaiKhoanDangBiTamKhoa; break;
                    case Enum.Core_AcsUser_TenDangNhapHoacMatKhauKhongChinhXac: message = MessageEnResource.Core_AcsUser_TenDangNhapHoacMatKhauKhongChinhXac; break;
                    case Enum.Core_AcsUser_TaoTaiKhoanThatBai: message = MessageEnResource.Core_AcsUser_TaoTaiKhoanThatBai; break;
                    case Enum.Core_AcsUser_TaoTaiKhoanHeThongTichHopThanhCongTuyNhienKhongGanDuocUserRole: message = MessageEnResource.Core_AcsUser_TaoTaiKhoanHeThongTichHopThanhCongTuyNhienKhongGanDuocUserRole; break;
                    case Enum.Core_AcsUser_TaoTaiKhoanHeThongTichHopThanhCongTuyNhienKhongGanDuocUserRoleDoRoleCodeGuiLenKhongHopLe: message = MessageEnResource.Core_AcsUser_TaoTaiKhoanHeThongTichHopThanhCongTuyNhienKhongGanDuocUserRoleDoRoleCodeGuiLenKhongHopLe; break;
                    case Enum.Core_AcsUser_TenDangNhapKhongChinhXac: message = MessageEnResource.Core_AcsUser_TenDangNhapKhongChinhXac; break;
                    case Enum.Core_AcsUser_EmailKhongHopLe: message = MessageEnResource.Core_AcsUser_EmailKhongHopLe; break;
                    case Enum.Core_AcsUser_YeuCauCapNhatThongTinTaiKhoanDayDuTruocKhiResetMatKhauHoacGoiTongDaiCHKHDeDuocHoTro: message = MessageEnResource.Core_AcsUser_YeuCauCapNhatThongTinTaiKhoanDayDuTruocKhiResetMatKhauHoacGoiTongDaiCHKHDeDuocHoTro; break;
                    case Enum.Core_AcsUser_GuiEmailXacNhanResetMatKhauDenNguoiDungThatBai: message = MessageEnResource.Core_AcsUser_GuiEmailXacNhanResetMatKhauDenNguoiDungThatBai; break;
                    case Enum.Core_AcsUser_YeuCaukichHoatTaiKhoanThatBai: message = MessageViResource.Core_AcsUser_YeuCaukichHoatTaiKhoanThatBai; break;
                    case Enum.Core_AcsUser_DaQuaThoiHanResetMatKhau: message = MessageViResource.Core_AcsUser_DaQuaThoiHanResetMatKhau; break;
                    case Enum.Core_AcsUser_ChuaThucHienGuiYeuCauGuiOtpDoiMatKhau: message = MessageViResource.Core_AcsUser_ChuaThucHienGuiYeuCauGuiOtpDoiMatKhau; break;
                    case Enum.Core_AcsUser_GuiYeuCauOtpDoiMatKhauTaiKhoanTruyCapGuiLenKhongHopLe: message = MessageViResource.Core_AcsUser_GuiYeuCauOtpDoiMatKhauTaiKhoanTruyCapGuiLenKhongHopLe; break;
                    case Enum.Core_AcsUser_GuiYeuCauOtpDoiMatKhauSoDienThoaiGuiLenKhongKhopVoiSoDTDaDangKy: message = MessageViResource.Core_AcsUser_GuiYeuCauOtpDoiMatKhauSoDienThoaiGuiLenKhongKhopVoiSoDTDaDangKy; break;
                    case Enum.Core_AcsUser_CapNhatThongTinTaiKhoanThatBai: message = MessageViResource.Core_AcsUser_CapNhatThongTinTaiKhoanThatBai; break;
                    case Enum.Core_AcsToken_KhongChoPhepGiaHanThoiGianHieuLucCuaToken: message = MessageViResource.Core_AcsToken_KhongChoPhepGiaHanThoiGianHieuLucCuaToken; break;
                    case Enum.Common__ThemMoiThatBai: message = MessageViResource.Common__ThemMoiThatBai; break;
                    case Enum.Common__CapNhatThatBai: message = MessageViResource.Common__CapNhatThatBai; break;

                    default: message = defaultEnMessage; break;
                }
            }
            return message;
        }
    }
}
