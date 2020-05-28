namespace App
{
    public interface IActionInvokerFactory
    {
        IActionInvoker CreateInvoker(ActionContext actionContext);
    }
}
