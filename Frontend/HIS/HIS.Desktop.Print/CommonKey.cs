
namespace HIS.Desktop.Print
{
    /// <summary>
    /// Danh sach cac the du lieu thuong gap va khong phu thuoc vao nghiep vu dac thu cua tung bieu mau.
    /// Luu y tat ca cac the common phai set vao day de tien theo doi & quan ly, tuyet doi nghiem cam hard code string trong khoi xu ly (processor).
    /// Ten hang so quy dinh dat trung voi ten key.
    /// Cac lop ...SingleKey trong Core deu phai ke thua CommonKey, luu y tranh trung key.
    /// </summary>
    class CommonKey
    {
        internal const string _ORGANIZATION_NAME = "ORGANIZATION_NAME";
        internal const string _PARENT_ORGANIZATION_NAME = "PARENT_ORGANIZATION_NAME";
        internal const string _CURRENT_TIME_STR = "CURRENT_TIME_STR";
        internal const string _CURRENT_DATE_STR = "CURRENT_DATE_STR";
        internal const string _CURRENT_MONTH_STR = "CURRENT_MONTH_STR";
        internal const string _CURRENT_DATE_SEPARATE_STR = "CURRENT_DATE_SEPARATE_STR";
        internal const string _CURRENT_TIME_SEPARATE_STR = "CURRENT_TIME_SEPARATE_STR";
        internal const string _CURRENT_TIME_SEPARATE_BEGIN_TIME_STR = "CURRENT_TIME_SEPARATE_BEGIN_TIME_STR";
        internal const string _CURRENT_MONTH_SEPARATE_STR = "CURRENT_MONTH_SEPARATE_STR";
        internal const string _CURRENT_TIME_SEPARATE_WITHOUT_SECOND_STR = "CURRENT_TIME_SEPARATE_WITHOUT_SECOND_STR";
    }
}
