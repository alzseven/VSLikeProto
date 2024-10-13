// 0.0.1
namespace Core.InputSystem
{
    public interface IInputAction<T>
    {
        public bool IsActionInvoked();
        public T GetInputValue();
    }
}