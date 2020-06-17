using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Assertions;

namespace Flux {
    public class Dispatcher<TPayload> where TPayload: class{
        public delegate void CallbackDelegate(TPayload payload);
        class Callback {
            public CallbackDelegate callback;
            public bool isHandled;
            public bool isPending;
            public Callback(CallbackDelegate _callback){
                callback = _callback;
                isHandled = false;
                isPending = false;
            }
        }
        
        Dictionary<string, Callback> _callbacks;
        bool _isDispatching = false;
        uint _lastId = 1;
        TPayload _pendingPayload;
        string _prefix = "ID_";
        StringBuilder sb;
        public Dispatcher(){
            _callbacks = new Dictionary<string, Callback>();
            sb = new StringBuilder();
        }

        public string register(CallbackDelegate _callback){
            Assert.IsFalse(_isDispatching, "Dispatcher.register(...): Cannot register in the middle of a dispatch.");
            sb.Remove(0,sb.Length)
                .Append(_prefix)
                .Append(_lastId++);
            var id = sb.ToString();
            _callbacks[id] = new Callback(_callback);
            return id;
        }

        public void dispatch(TPayload payload){
            Assert.IsFalse(_isDispatching,"Dispatch.dispatch(...): Cannot dispatch in the middle of a dispatch.");
            _startDispatching(payload);
            try {
                foreach (var cb in _callbacks){
                    if (cb.Value.isPending){ continue; }
                    _invokeCallback(cb.Key);
                }
            } finally {
                _stopDispatching();
            }
        }

        public bool isDispatching{
            get{ return _isDispatching; }
        }
        void _invokeCallback(string id){
            var cb = _callbacks[id];
            cb.isPending = true;
            cb.callback(_pendingPayload);
            cb.isHandled = true;
        }
        void _startDispatching(TPayload payload){
            foreach(var cb in _callbacks.Values){
                cb.isHandled = false;
                cb.isPending = false;
            }
            _pendingPayload = payload;
            _isDispatching = true;
        }
        void _stopDispatching(){
            _pendingPayload = null;
            _isDispatching = false;
        }
    }
    
    // if you want processing in order
    public class QueueDispatcher<TPayload> : Dispatcher<TPayload> where TPayload: class{
        Queue<TPayload> dispatchQueue = new Queue<TPayload>();

        public new void dispatch(TPayload payload){
            if(isDispatching){
                dispatchQueue.Enqueue(payload);
                return;
            }
            while(true){
                base.dispatch(payload);
                if (dispatchQueue.Count == 0) break;
                payload = dispatchQueue.Dequeue();
            }
        }
        public new bool isDispatching => base.isDispatching || dispatchQueue.Count>0;
    }
}