using PlasticGui.Configuration.CloudEdition.Welcome;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;

namespace RTCube.Extensions
{
    public class SignalBus
    {
        public void Fire<TSignal>()
        {
            //MessageBroker.Default.Publish<TSignal>();
        }
        public void Fire<TSignal>(TSignal signal)
        {
            MessageBroker.Default.Publish<TSignal>(signal);
        }

        public IDisposable Subscribe<T>(Action<T> value)
        {
            return MessageBroker.Default.Receive<T>().Subscribe(value);
        }
    }
}