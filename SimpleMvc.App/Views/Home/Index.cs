namespace SimpleMvc.App.Views.Home
{
    using System.Text;
    using SimpleMcv.Framework.Interfaces;

    public class Index : IRenderable
    {
        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<h1>Note App</h1>");
            sb.AppendLine("<a href=\"/users/all\">All Users</a>");
            sb.AppendLine("<a href=\"/users/register\">Register User</a>");

            return sb.ToString();
        }
    }
}
