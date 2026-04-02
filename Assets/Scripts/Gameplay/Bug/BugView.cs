using UnityEngine;

namespace TestTask_Bioneers.Gameplay
{
    public class BugView : MonoBehaviour
    {
        private Bug _bug;

        private void Update()
        {
            this.transform.position = new Vector3(_bug.Position.x, _bug.Position.y, 0);
        }

        public void BindTo(Bug bug)
        {
            _bug = bug;
        }

        public void Unbind()
        {
            _bug = null;
        }
    }
}