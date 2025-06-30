using CommonModel;

namespace ApiWeb.Midleware
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHelperHttpContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        InfoRequest GetInfoRequest(HttpContext request);
    }
}
