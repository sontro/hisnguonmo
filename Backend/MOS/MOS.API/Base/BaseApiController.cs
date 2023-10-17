using System.Web.Http;
using System.Web.Http.Cors;

namespace MOS.API.Base
{
    [Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    //[LicenseFilter]
    //[AllowAnonymous]
    public class BaseApiController : ApiController
    {
        
    }
}
