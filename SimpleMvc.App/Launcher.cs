namespace SimpleMvc.App
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using SimpleMcv.Framework;
    using SimpleMcv.Framework.Routers;
    using SimpleMvc.Data;
    using WebServer;

    public class Launcher
    {
        public static void Main()
        {
            var server = new WebServer(8000, new ControllerRouter());
            InitializeDatabase();

            MvcEngine.Run(server);
        }

        private static void InitializeDatabase()
        {
            using(var db = new SimpleAppDb())
            {
                db.Database.Migrate();
            }
        }
    }
}
