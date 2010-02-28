// Copyright 2007-2008 The Apache Software Foundation.
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit.Reactive
{
    using System;

    public class ServiceBusSubscription<T> : 
        IDisposable where T : class
    {
        readonly UnsubscribeAction _unsubscribeAction;

        public ServiceBusSubscription(IServiceBus bus, IObserver<T> observer, Predicate<T> condition)
        {
            _unsubscribeAction = condition == null ?
                bus.Subscribe<T>(observer.OnNext) :
                bus.Subscribe(observer.OnNext, condition);

            // TODO: Hook for observer.OnError?
        }


        public void Dispose()
        {
            _unsubscribeAction();
        }
    }
}