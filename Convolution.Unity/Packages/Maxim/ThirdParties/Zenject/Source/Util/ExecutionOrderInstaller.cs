using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Zenject
{
    public class ExecutionOrderInstaller : Installer<List<Type>, ExecutionOrderInstaller>
    {
        List<Type> _typeOrder;

        public ExecutionOrderInstaller(List<Type> typeOrder)
        {
            _typeOrder = typeOrder;
        }

        public override UniTask InstallBindings()
        {
            // All tickables without explicit priorities assigned are given order of zero,
            // so put all of these before that (ie. negative)
            int order = -1 * _typeOrder.Count;

            foreach (var type in _typeOrder)
            {
                Container.BindExecutionOrder(type, order);
                order++;
            }
            
            return UniTask.CompletedTask;
        }
    }
}

