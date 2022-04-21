using Cysharp.Threading.Tasks;

namespace AsyncFSM
{
    public interface IState
    {
        StateMachine StateMachine { get; set; }
        UniTask OnEnter();
        UniTask OnExit();
        void SetOptions(Options options);
        void OnUpdate();
    }
}