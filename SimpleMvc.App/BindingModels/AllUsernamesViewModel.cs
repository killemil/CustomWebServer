namespace SimpleMvc.App.BindingModels
{
    using System.Collections.Generic;

    public class AllUsernamesViewModel
    {
        public IList<string> Usernames { get; set; } = new List<string>();
        
    }
}
