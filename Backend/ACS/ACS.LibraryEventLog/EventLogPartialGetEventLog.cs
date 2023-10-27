namespace ACS.LibraryEventLog
{
    public partial class EventLog
    {
        private string GetEventLog(Enum enumBC)
        {
            string content = "";
            if (Language == LanguageEnum.VI)
            {
                switch (enumBC)
                {
                    case Enum.AcsApplicationRole_ThemQuyenTruyCap: content = EventLogViResource.AcsApplicationRole_ThemQuyenTruyCap; break;
                    case Enum.AcsApplicationRole_XoaQuyenTruyCap: content = EventLogViResource.AcsApplicationRole_XoaQuyenTruyCap; break;
                    case Enum.AcsRole_ThemVaiTro: content = EventLogViResource.AcsRole_ThemVaiTro; break;
                    case Enum.AcsModuleRole_ThemVaiTroChucNang: content = EventLogViResource.AcsModuleRole_ThemVaiTroChucNang; break;
                    case Enum.AcsRole_XoaVaiTro: content = EventLogViResource.AcsRole_XoaVaiTro; break;
                    case Enum.AcsRole_SuaVaiTro: content = EventLogViResource.AcsRole_SuaVaiTro; break;
                    case Enum.AcsRole_SuaVaiTroKeThua: content = EventLogViResource.AcsRole_SuaVaiTroKeThua; break;
                    case Enum.AcsRole_XoaVaiTroKeThua: content = EventLogViResource.AcsRole_XoaVaiTroKeThua; break;
                    case Enum.AcsRole_ThemListVaiTro: content = EventLogViResource.AcsRole_ThemListVaiTro; break;
                    case Enum.AcsModuleRole_XoaVaiTroChucNang: content = EventLogViResource.AcsModuleRole_XoaVaiTroChucNang; break;
                    case Enum.Co: content = EventLogViResource.Co; break;
                    case Enum.VaiTro: content = EventLogViResource.VaiTro; break;
                    case Enum.ChucNang: content = EventLogViResource.ChucNang; break;
                    case Enum.VaiTroKeThua: content = EventLogViResource.VaiTroKeThua; break;
                    case Enum.Khong: content = EventLogViResource.Khong; break;
                    case Enum.ApplicationName: content = EventLogViResource.ApplicationName; break;
                    case Enum.Icon: content = EventLogViResource.Icon; break;
                    case Enum.KhongPhanQuyen: content = EventLogViResource.KhongPhanQuyen; break;
                    case Enum.KhongMoHopThoai: content = EventLogViResource.KhongMoHopThoai; break;
                    case Enum.Menu: content = EventLogViResource.Menu; break;
                    case Enum.NhomChucNang: content = EventLogViResource.NhomChucNang; break;
                    case Enum.ModuleLink: content = EventLogViResource.ModuleLink; break;
                    case Enum.ModuleName: content = EventLogViResource.ModuleName; break;
                    case Enum.Url: content = EventLogViResource.Url; break;
                    case Enum.SoThuTu: content = EventLogViResource.SoThuTu; break;
                    case Enum.Cha: content = EventLogViResource.Cha; break;
                    case Enum.VideoUrls: content = EventLogViResource.VideoUrls; break;
                    case Enum.AcsModule_SuaDanhMucChucNang: content = EventLogViResource.AcsModule_SuaDanhMucChucNang; break;
                    case Enum.AcsModule_XoaDanhMucChucNang: content = EventLogViResource.AcsModule_XoaDanhMucChucNang; break;
                    case Enum.AcsModule_MoKhoaDanhMucChucNang: content = EventLogViResource.AcsModule_MoKhoaDanhMucChucNang; break;
                    case Enum.AcsModule_KhoaDanhMucChucNang: content = EventLogViResource.AcsModule_KhoaDanhMucChucNang; break;

                    default: content = defaultViEventLog; break;
                }
            }
            else if (Language == LanguageEnum.EN)
            {
                switch (enumBC)
                {
                    default: content = defaultEnEventLog; break;
                }
            }
            return content;
        }
    }
}
