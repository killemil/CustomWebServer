namespace SimpleMvc.App.Views.Users
{
    using System.Text;
    using SimpleMcv.Framework.Interfaces.Generic;
    using SimpleMvc.App.BindingModels;

    public class All : IRenderable<AllUsernamesViewModel>
    {
        public AllUsernamesViewModel Model { get; set; }

        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<a href=\"/home/index\">Home</a>");
            sb.AppendLine("<h2>All Users</h2>");
            sb.AppendLine("<ul>");

            int index = 1;
            foreach (var username in this.Model.Usernames)
            {
                sb.AppendLine($"<li><a href=\"/users/profile?id={index}\">{username}</li></li>");
                index++;
            }

            sb.AppendLine("<ul>");

            return sb.ToString();
        }
    }
}
