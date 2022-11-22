namespace Lnco.Unity.Module.EventSystems
{
    public interface IDropable<out T>
    {
        public T Data { get; }
    }
}