using Unity.Mathematics;

namespace TestTask_Bioneers.Interfaces
{
    public interface IFood
    {
        public float2 Position { get; }
        public void Eat();
    }
}