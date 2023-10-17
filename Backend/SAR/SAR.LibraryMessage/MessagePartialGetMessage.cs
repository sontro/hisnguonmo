namespace SAR.LibraryMessage
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
                    case Enum.Common__ThieuThongTinBatBuoc: message = MessageViResource.Common__ThieuThongTinBatBuoc; break;
                    case Enum.Common__MaDaTonTaiTrenHeThong: message = MessageViResource.Common__MaDaTonTaiTrenHeThong; break;
                    case Enum.Common__DuLieuDangBiKhoa: message = MessageViResource.Common__DuLieuDangBiKhoa; break;
                    case Enum.Common__LoiCauHinhHeThong: message = MessageViResource.Common__LoiCauHinhHeThong; break;
                    case Enum.SdaGroup_ChaMoiCuaDonViKhongDuocLaConHienTai: message = MessageViResource.SdaGroup_ChaMoiCuaDonViKhongDuocLaConHienTai; break;
                    case Enum.SarReportType_BaoCaoChuaCoDuLieuThietLap: message = MessageViResource.SarReportType_BaoCaoChuaCoDuLieuThietLap; break;
                    case Enum.SarReportType_NguoiDungChuaCoDuLieuThietLap: message = MessageViResource.SarReportType_NguoiDungChuaCoDuLieuThietLap; break;

                    default: message = defaultViMessage; break;
                }
            }
            else if (Language == LanguageEnum.English)
            {
                switch (enumBC)
                {

                    default: message = defaultEnMessage; break;
                }
            }
            return message;
        }
    }
}
