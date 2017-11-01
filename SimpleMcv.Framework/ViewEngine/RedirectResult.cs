namespace SimpleMcv.Framework.ViewEngine
{
    using SimpleMcv.Framework.Interfaces;

    public class RedirectResult : IRedirectable
    {
        public RedirectResult(string redirectUrl)
        {
            this.RedirectUrl = redirectUrl;
        }

        public string RedirectUrl { get;}

        public string Invoke()
        {
            return this.RedirectUrl;
        }
    }
}
