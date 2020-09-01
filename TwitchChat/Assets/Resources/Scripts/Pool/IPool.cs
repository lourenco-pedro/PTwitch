namespace PoolSystem
{
    public interface IPool
    {
        void Reset();
        bool IsBeenUsed { get; set; }
    }
}