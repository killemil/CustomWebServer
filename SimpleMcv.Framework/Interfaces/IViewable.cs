namespace SimpleMcv.Framework.Interfaces
{
    public interface IViewable : IActionResult
    {
        IRenderable View { get; set; }
    }
}