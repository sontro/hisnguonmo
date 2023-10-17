namespace SDA.LibraryEventLog
{
    public partial class EventLog
    {
        private string GetMessage(Enum enumBC)
        {
            string message = "";
            if (Language == LanguageEnum.Vietnamese)
            {
                switch (enumBC)
                {
                    case Enum.Action_ThemDuLieuThanhCong: message = MessageViResource.Action_ThemDuLieuThanhCong; break;
                    case Enum.Action_SuaDuLieuThanhCong: message = MessageViResource.Action_SuaDuLieuThanhCong; break;
                    case Enum.Action_XoaDuLieuThanhCong: message = MessageViResource.Action_XoaDuLieuThanhCong; break;
                    case Enum.Action_KhoaDuLieuThanhCong: message = MessageViResource.Action_KhoaDuLieuThanhCong; break;
                    case Enum.Action_BaoCaoDuLieuThanhCong: message = MessageViResource.Action_BaoCaoDuLieuThanhCong; break;
                    case Enum.Action_InDuLieuThanhCong: message = MessageViResource.Action_InDuLieuThanhCong; break;
                    case Enum.Action_ThemDuLieuThatBai: message = MessageViResource.Action_ThemDuLieuThatBai; break;
                    case Enum.Action_SuaDuLieuThatBai: message = MessageViResource.Action_SuaDuLieuThatBai; break;
                    case Enum.Action_XoaDuLieuThatBai: message = MessageViResource.Action_XoaDuLieuThatBai; break;
                    case Enum.Action_KhoaDuLieuThatBai: message = MessageViResource.Action_KhoaDuLieuThatBai; break;
                    case Enum.Action_BaoCaoDuLieuThatBai: message = MessageViResource.Action_BaoCaoDuLieuThatBai; break;
                    case Enum.Action_InDuLieuThatBai: message = MessageViResource.Action_InDuLieuThatBai; break;
                    case Enum.Term_SarReport: message = MessageViResource.Term_SarReport; break;
                    case Enum.Term_SarReportCalendar: message = MessageViResource.Term_SarReportCalendar; break;
                    case Enum.Term_SarReportStt: message = MessageViResource.Term_SarReportStt; break;
                    case Enum.Term_SarReportTemplate: message = MessageViResource.Term_SarReportTemplate; break;
                    case Enum.Term_SarReportType: message = MessageViResource.Term_SarReportType; break;
                    case Enum.Term_SdaCommune: message = MessageViResource.Term_SdaCommune; break;
                    case Enum.Term_SdaConfig: message = MessageViResource.Term_SdaConfig; break;
                    case Enum.Term_SdaCurrency: message = MessageViResource.Term_SdaCurrency; break;
                    case Enum.Term_SdaDbLog: message = MessageViResource.Term_SdaDbLog; break;
                    case Enum.Term_SdaDistrict: message = MessageViResource.Term_SdaDistrict; break;
                    case Enum.Term_SdaEthnic: message = MessageViResource.Term_SdaEthnic; break;
                    case Enum.Term_SdaEventLog: message = MessageViResource.Term_SdaEventLog; break;
                    case Enum.Term_SdaEventLogType: message = MessageViResource.Term_SdaEventLogType; break;
                    case Enum.Term_SdaFeedback: message = MessageViResource.Term_SdaFeedback; break;
                    case Enum.Term_SdaGroup: message = MessageViResource.Term_SdaGroup; break;
                    case Enum.Term_SdaGroupType: message = MessageViResource.Term_SdaGroupType; break;
                    case Enum.Term_SdaHashtag: message = MessageViResource.Term_SdaHashtag; break;
                    case Enum.Term_SdaJsonRedundancy: message = MessageViResource.Term_SdaJsonRedundancy; break;
                    case Enum.Term_SdaMessage: message = MessageViResource.Term_SdaMessage; break;
                    case Enum.Term_SdaMessageBroadcast: message = MessageViResource.Term_SdaMessageBroadcast; break;
                    case Enum.Term_SdaNational: message = MessageViResource.Term_SdaNational; break;
                    case Enum.Term_SdaProvince: message = MessageViResource.Term_SdaProvince; break;
                    case Enum.Term_SdaReligion: message = MessageViResource.Term_SdaReligion; break;
                    case Enum.Term_SdaLicense: message = MessageViResource.Term_SdaLicense; break;
                    case Enum.Term_SdaTrouble: message = MessageViResource.Term_SdaTrouble; break;
                    case Enum.Action_DangNhapThanhCong: message = MessageViResource.Action_DangNhapThanhCong; break;

                    default: message = defaultViMessage; break;
                }
            }
            else if (Language == LanguageEnum.English)
            {
                switch (enumBC)
                {
                    case Enum.Action_ThemDuLieuThanhCong: message = MessageEnResource.Action_ThemDuLieuThanhCong; break;
                    case Enum.Action_SuaDuLieuThanhCong: message = MessageEnResource.Action_SuaDuLieuThanhCong; break;
                    case Enum.Action_XoaDuLieuThanhCong: message = MessageEnResource.Action_XoaDuLieuThanhCong; break;
                    case Enum.Action_KhoaDuLieuThanhCong: message = MessageEnResource.Action_KhoaDuLieuThanhCong; break;
                    case Enum.Action_BaoCaoDuLieuThanhCong: message = MessageEnResource.Action_BaoCaoDuLieuThanhCong; break;
                    case Enum.Action_InDuLieuThanhCong: message = MessageEnResource.Action_InDuLieuThanhCong; break;
                    case Enum.Action_ThemDuLieuThatBai: message = MessageEnResource.Action_ThemDuLieuThatBai; break;
                    case Enum.Action_SuaDuLieuThatBai: message = MessageEnResource.Action_SuaDuLieuThatBai; break;
                    case Enum.Action_XoaDuLieuThatBai: message = MessageEnResource.Action_XoaDuLieuThatBai; break;
                    case Enum.Action_KhoaDuLieuThatBai: message = MessageEnResource.Action_KhoaDuLieuThatBai; break;
                    case Enum.Action_BaoCaoDuLieuThatBai: message = MessageEnResource.Action_BaoCaoDuLieuThatBai; break;
                    case Enum.Action_InDuLieuThatBai: message = MessageEnResource.Action_InDuLieuThatBai; break;
                    case Enum.Term_SarReport: message = MessageEnResource.Term_SarReport; break;
                    case Enum.Term_SarReportCalendar: message = MessageEnResource.Term_SarReportCalendar; break;
                    case Enum.Term_SarReportStt: message = MessageEnResource.Term_SarReportStt; break;
                    case Enum.Term_SarReportTemplate: message = MessageEnResource.Term_SarReportTemplate; break;
                    case Enum.Term_SarReportType: message = MessageEnResource.Term_SarReportType; break;
                    case Enum.Term_SdaCommune: message = MessageEnResource.Term_SdaCommune; break;
                    case Enum.Term_SdaConfig: message = MessageEnResource.Term_SdaConfig; break;
                    case Enum.Term_SdaCurrency: message = MessageEnResource.Term_SdaCurrency; break;
                    case Enum.Term_SdaDbLog: message = MessageEnResource.Term_SdaDbLog; break;
                    case Enum.Term_SdaDistrict: message = MessageEnResource.Term_SdaDistrict; break;
                    case Enum.Term_SdaEthnic: message = MessageEnResource.Term_SdaEthnic; break;
                    case Enum.Term_SdaEventLog: message = MessageEnResource.Term_SdaEventLog; break;
                    case Enum.Term_SdaEventLogType: message = MessageEnResource.Term_SdaEventLogType; break;
                    case Enum.Term_SdaFeedback: message = MessageEnResource.Term_SdaFeedback; break;
                    case Enum.Term_SdaGroup: message = MessageEnResource.Term_SdaGroup; break;
                    case Enum.Term_SdaGroupType: message = MessageEnResource.Term_SdaGroupType; break;
                    case Enum.Term_SdaHashtag: message = MessageEnResource.Term_SdaHashtag; break;
                    case Enum.Term_SdaJsonRedundancy: message = MessageEnResource.Term_SdaJsonRedundancy; break;
                    case Enum.Term_SdaMessage: message = MessageEnResource.Term_SdaMessage; break;
                    case Enum.Term_SdaMessageBroadcast: message = MessageEnResource.Term_SdaMessageBroadcast; break;
                    case Enum.Term_SdaNational: message = MessageEnResource.Term_SdaNational; break;
                    case Enum.Term_SdaProvince: message = MessageEnResource.Term_SdaProvince; break;
                    case Enum.Term_SdaReligion: message = MessageEnResource.Term_SdaReligion; break;
                    case Enum.Term_SdaLicense: message = MessageEnResource.Term_SdaLicense; break;
                    case Enum.Term_SdaTrouble: message = MessageEnResource.Term_SdaTrouble; break;

                    default: message = defaultEnMessage; break;
                }
            }
            return message;
        }
    }
}
