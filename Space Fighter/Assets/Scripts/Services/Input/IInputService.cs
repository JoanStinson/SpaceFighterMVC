namespace JGM.Game
{
    public interface IInputService
    {
        float vertical { get; }
        bool shootProjectile { get; }

        void ReadInput();
    }
}