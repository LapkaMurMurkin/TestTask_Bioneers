using Cysharp.Threading.Tasks;

using R3;

using Unity.Mathematics;

using UnityEngine;

namespace TestTask_Bioneers.Gameplay
{
    public class BugView : MonoBehaviour
    {
        private Bug _bug;

        private CompositeDisposable _disposables;

        public void BindTo(Bug bug)
        {
            _bug = bug;
            _disposables = new CompositeDisposable();
            ReadOnlyReactiveProperty<float2> bugPosition = Observable.EveryValueChanged(_bug, value => value.Position).ToReadOnlyReactiveProperty().AddTo(_disposables);
            bugPosition.Subscribe(position => this.transform.position = new Vector3(position.x, position.y, 0)).AddTo(_disposables);
        }

        public void Unbind()
        {
            if (_disposables != null)
                _disposables.Clear();
            _bug = null;
        }

        private void OnDestroy()
        {
            Unbind();
        }
    }
}