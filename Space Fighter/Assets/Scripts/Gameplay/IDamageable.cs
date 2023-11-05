namespace JGM.Game
{
    public interface IDamageable
    {
        int scorePoints { get; }

        void TakeDamage(float damageAmount);
    }
}