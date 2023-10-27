namespace ACS.LibraryConfig
{
    public class StandardConstant
    {
        public const short IS_ACTIVE = (short)1;
        public const short IS_INACTIVE = (short)0;
        public const string COMBO_ALL = "Tất cả";
        public const long COMBO_ALL_VALUE = -1;
        public const char DELIMITER = ';';
        /// <summary>
        /// 1:kích hoạt
        /// </summary>
        public const short OTP_TYPE_ACTIVE = (short)1;
        /// <summary>
        /// 2:đổi mật khẩu
        /// </summary>
        public const short OTP_TYPE_CHANGEPASS = (short)2;

        public const short OTP_TYPE_OTHER = (short)3;
    }
}
