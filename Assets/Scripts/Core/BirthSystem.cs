using System;

using Unity.Mathematics;

namespace TestTask_Bioneers.Core
{
    public class BirthSystem
    {
        private readonly Action<float2> _reproduceWorkerAction;
        private readonly Action<float2> _reproducePredartorAction;

        public BirthSystem(Action<float2> reproduceWorkerAction, Action<float2> reproducePredartorAction)
        {
            _reproduceWorkerAction = reproduceWorkerAction;
            _reproducePredartorAction = reproducePredartorAction;
        }

        public void ReproduceWorker(float2 parentPosition) => _reproduceWorkerAction?.Invoke(parentPosition);
        public void ReproducePredator(float2 parentPosition) => _reproducePredartorAction?.Invoke(parentPosition);
    }
}