namespace ACS.LibraryBug
{
    public partial class Bug
    {
        private string GetCode(Enum enumBC)
        {
            string code = "";
            switch (enumBC)
            {
                case Enum.Common__KXDDDuLieuCanXuLy: code = CodeResource.Common__KXDDDuLieuCanXuLy; break;
                case Enum.Common__ThieuThongTinBatBuoc: code = CodeResource.Common__ThieuThongTinBatBuoc; break;
                case Enum.Common__LoiCauHinhHeThong: code = CodeResource.Common__LoiCauHinhHeThong; break;
                case Enum.Common__FactoryKhoiTaoDoiTuongThatBai: code = CodeResource.Common__FactoryKhoiTaoDoiTuongThatBai; break;
                case Enum.Common__MaDaTonTai: code = CodeResource.Common__MaDaTonTai; break;
                case Enum.Common__DuLieuDangBiKhoa: code = CodeResource.Common__DuLieuDangBiKhoa; break;
                case Enum.Common__CapNhatThatBai: code = CodeResource.Common__CapNhatThatBai; break;
                case Enum.Common__ThemMoiThatBai: code = CodeResource.Common__ThemMoiThatBai; break;
                case Enum.AcsUser_GuiEmailXacNhanThongTinTaiKhoanThatBai: code = CodeResource.AcsUser_GuiEmailXacNhanThongTinTaiKhoanThatBai; break;
                case Enum.AcsUser_GuiSmsXacNhanThongTinTaiKhoanThatBai: code = CodeResource.AcsUser_GuiSmsXacNhanThongTinTaiKhoanThatBai; break;
                case Enum.AcsRoleBase_ThemMoiThatBai: code = CodeResource.AcsRoleBase_ThemMoiThatBai; break;
                case Enum.AcsRoleBase_RollBackThatBai: code = CodeResource.AcsRoleBase_RollBackThatBai; break;
                case Enum.AcsRoleUser_ThemMoiThatBai: code = CodeResource.AcsRoleUser_ThemMoiThatBai; break;
                case Enum.AcsRoleUser_RollBackThatBai: code = CodeResource.AcsRoleUser_RollBackThatBai; break;
                case Enum.AcsRole_ThemMoiThatBai: code = CodeResource.AcsRole_ThemMoiThatBai; break;
                case Enum.AcsRole_CapNhatThatBai: code = CodeResource.AcsRole_CapNhatThatBai; break;
                case Enum.AcsRole_XoaThatBai: code = CodeResource.AcsRole_XoaThatBai; break;

                case Enum.AcsUser_TaoRoleUserKhiTaoTaiKhoanHeThongTichHopThatBai: code = CodeResource.AcsUser_TaoRoleUserKhiTaoTaiKhoanHeThongTichHopThatBai; break;
                case Enum.AcsUser_TaoApplicationRoleKhiTaoTaiKhoanHeThongTichHopThatBai: code = CodeResource.AcsUser_TaoApplicationRoleKhiTaoTaiKhoanHeThongTichHopThatBai; break;
                case Enum.AcsUser_TaoRoleKhiTaoTaiKhoanHeThongTichHopThatBai: code = CodeResource.AcsUser_TaoRoleKhiTaoTaiKhoanHeThongTichHopThatBai; break;
                case Enum.AcsToken_PhienBanHienTaiDaCuPhaiNenPhienBanMoiHonDeHeThongTuongThichChayOnDinh: code = CodeResource.AcsToken_PhienBanHienTaiDaCuPhaiNenPhienBanMoiHonDeHeThongTuongThichChayOnDinh; break;

                case Enum.AcsToken_ThemMoiThatBai: code = CodeResource.AcsToken_ThemMoiThatBai; break;
                case Enum.AcsToken_CapNhatThatBai: code = CodeResource.AcsToken_CapNhatThatBai; break;
                case Enum.AcsOtp_ThemMoiThatBai: code = CodeResource.AcsOtp_ThemMoiThatBai; break;
                case Enum.AcsOtp_CapNhatThatBai: code = CodeResource.AcsOtp_CapNhatThatBai; break;

                case Enum.AcsAuthenRequest_ThemMoiThatBai: code = CodeResource.AcsAuthenRequest_ThemMoiThatBai; break;
                case Enum.AcsAuthenRequest_CapNhatThatBai: code = CodeResource.AcsAuthenRequest_CapNhatThatBai; break;
                case Enum.AcsAuthorSystem_ThemMoiThatBai: code = CodeResource.AcsAuthorSystem_ThemMoiThatBai; break;
                case Enum.AcsAuthorSystem_CapNhatThatBai: code = CodeResource.AcsAuthenRequest_ThemMoiThatBai; break;

                case Enum.AcsRoleAuthor_ThemMoiThatBai: code = CodeResource.AcsRoleAuthor_ThemMoiThatBai; break;
                case Enum.AcsRoleAuthor_CapNhatThatBai: code = CodeResource.AcsRoleAuthor_CapNhatThatBai; break;

                default: code = defaultViMessage; break;
            }
            return code;
        }
    }
}
