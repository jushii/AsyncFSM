using Cysharp.Threading.Tasks;

namespace AsyncFSM
{
    public abstract class State : State<Options>
    {
    }

    public abstract class State<T> : IState where T : Options
    {
        public StateMachine StateMachine { get; set; }

        protected T Options { get; private set; }

        public virtual async UniTask OnEnter()
        {
            await UniTask.Yield();
        }

        public virtual async UniTask OnExit()
        {
            await UniTask.Yield();
        }

        public void SetOptions(Options options)
        {
            if (options is T stateOptions)
            {
                Options = stateOptions;
            }
        }

        public virtual void OnUpdate()
        {
        }
    }
}