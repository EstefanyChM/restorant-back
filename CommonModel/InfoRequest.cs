namespace CommonModel
{
    public class InfoRequest
    {
        public TokenClaims Claims { get; set; } = new TokenClaims();
        public ApiRequestContext RequestHttp { get; set; } = new ApiRequestContext();
    }
}