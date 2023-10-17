namespace MOS.MANAGER.Config
{
    class HisAntibioticRequestCFG
    {
        private const string ALLOW_TO_UPDATE_APPROVED_REQUEST_CFG = "MOS.HIS_ANTIBIOTIC_REQUEST.ALLOW_TO_UPDATE_APPROVED_REQUEST";

        private static bool? isAllowToUpdateApprovedRequest;
        public static bool IS_ALLOW_TO_UPDATE_APPROVED_REQUEST
        {
            get
            {
                if (!isAllowToUpdateApprovedRequest.HasValue)
                {
                    isAllowToUpdateApprovedRequest = ConfigUtil.GetIntConfig(ALLOW_TO_UPDATE_APPROVED_REQUEST_CFG) == 1;
                }
                return isAllowToUpdateApprovedRequest.Value;
            }
        }

        public static void Reload()
        {
            isAllowToUpdateApprovedRequest = ConfigUtil.GetIntConfig(ALLOW_TO_UPDATE_APPROVED_REQUEST_CFG) == 1;
        }
    }
}
