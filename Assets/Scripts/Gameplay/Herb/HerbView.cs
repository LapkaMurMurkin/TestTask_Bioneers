using UnityEngine;

namespace TestTask_Bioneers.Gameplay
{
    public class HerbView : MonoBehaviour
    {
        private Herb _herb;

        public void BindTo(Herb herb)
        {
            _herb = herb;
            this.transform.position = new Vector3(_herb.Position.x, _herb.Position.y, 0);
        }

        public void Unbind()
        {
            _herb = null;
        }
    }
}