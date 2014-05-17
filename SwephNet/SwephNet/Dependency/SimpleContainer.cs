using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SwephNet.Dependency
{

    /// <summary>
    /// Simple dependency container
    /// </summary>
    class SimpleContainer : IDependencyContainer
    {
        interface ITypeCreator
        {
            object Create(SimpleContainer container);
        }
        interface IResolver
        {
            object Resolve(SimpleContainer container);
        }

        List<object> _ExternalInstances = new List<object>();
        List<object> _Instances = new List<object>();
        Dictionary<Type, object> _InstanceIndex = new Dictionary<Type, object>();
        Dictionary<Type, ITypeCreator> _Creators = new Dictionary<Type, ITypeCreator>();
        Dictionary<Type, IResolver> _Resolvers = new Dictionary<Type, IResolver>();

        /// <summary>
        /// Release resources
        /// </summary>
        public void Dispose() {
            foreach (var instance in _Instances) {
                if (instance is IDisposable)
                    ((IDisposable)instance).Dispose();
            }
            _InstanceIndex.Clear();
            _ExternalInstances.Clear();
            _Instances.Clear();
            _Resolvers.Clear();
            _Creators.Clear();
        }

        /// <summary>
        /// Check if we can resolve a type
        /// </summary>
        public bool CanResolve(Type type) {
            if (type == null) return false;
            return _Resolvers.ContainsKey(type);
        }

        /// <summary>
        /// Resolve a type
        /// </summary>
        public object Resolve(Type type) {
            Check.ArgumentNotNull(type, "type");
            IResolver resolver = null;
            if (_Resolvers.TryGetValue(type, out resolver))
                return resolver.Resolve(this);
            throw new InvalidOperationException(String.Format("Resolver not found for type '{0}'", type.FullName));
        }

        /// <summary>
        /// Create a new instance of a type
        /// </summary>
        public object Create(Type type) {
            Check.ArgumentNotNull(type, "type");
            ITypeCreator creator;
            if (_Creators.TryGetValue(type, out creator))
                return creator.Create(this);
            return IoCCreate(type);
        }

        /// <summary>
        /// Create a new isntance of type by reflection
        /// </summary>
        protected object IoCCreate(Type type) {
            ConstructorInfo constructor = type.GetConstructors().FirstOrDefault();
            if (constructor == null) return Activator.CreateInstance(type);
            // Build parameters
            List<object> parameters = new List<object>();
            foreach (var param in constructor.GetParameters()) {
                if (CanResolve(param.ParameterType))
                    parameters.Add(Resolve(param.ParameterType));
                else
                    parameters.Add(Create(param.ParameterType));
            }
            // Create instance
            return Activator.CreateInstance(type, parameters.ToArray());
        }

    }

}
