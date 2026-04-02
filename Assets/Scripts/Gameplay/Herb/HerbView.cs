using System;

using R3;

using UnityEngine;

namespace TestTask_Bioneers.Gameplay
{
    public class HerbView : MonoBehaviour
    {
        private Herb _food;

        private IDisposable _subscription;

        public void BindTo(Herb food)
        {
            _food = food;
            _subscription = _food.Position.Subscribe(value =>
            {
                this.transform.position = new Vector3(value.x, value.y, 0);
            }).AddTo(this);
        }

        public void Unbind()
        {
            _subscription.Dispose();
            _food = null;
        }
    }
}