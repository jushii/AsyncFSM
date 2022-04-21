using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;

namespace AsyncFSM
{
    public class StateMachine
    {
        private IState _currentState;
        private IState _previousState;
        private readonly Dictionary<Type, IState> _states = new();
        private readonly Queue<Transition> _pendingTransitions = new();
        private CancellationTokenSource _cancellationTokenSource;

        public void Run()
        {
            _cancellationTokenSource = new();
            Update();
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }

        public void RegisterState(IState state)
        {
            state.StateMachine = this;

            _states.Add(state.GetType(), state);
        }

        public void RequestTransition(Type stateType)
        {
            _pendingTransitions.Enqueue(new Transition(stateType, null));
        }

        public void RequestTransition<T>(Type stateType, T options) where T : Options
        {
            _pendingTransitions.Enqueue(new Transition(stateType, options));
        }

        private async UniTask ChangeTo<T>(Type stateType, T options) where T : Options
        {
            if (_currentState != null)
            {
                _previousState = _currentState;
                await _previousState.OnExit();
                _currentState = null;
            }

            if (_states.TryGetValue(stateType, out IState nextState))
            {
                nextState.SetOptions(options);
                _currentState = nextState;
                await nextState.OnEnter();
            }
            else
            {
                throw new Exception($"State: {stateType.Name} is not registered to state machine.");
            }
        }

        private async void Update()
        {
            await foreach (var _ in UniTaskAsyncEnumerable
                               .EveryUpdate()
                               .WithCancellation(_cancellationTokenSource.Token))
            {
                while (_pendingTransitions.Count > 0)
                {
                    var transition = _pendingTransitions.Dequeue();
                    await ChangeTo(transition.Type, transition.Options);
                }

                _currentState?.OnUpdate();
            }
        }
    }
}