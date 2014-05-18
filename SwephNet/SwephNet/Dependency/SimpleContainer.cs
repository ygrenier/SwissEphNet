using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SwephNet.Locales;

namespace SwephNet.Dependency
{

    /// <summary>
    /// Simple dependency container
    /// </summary>
    class SimpleContainer : IDependencyContainer
    {
        interface ICreator
        {
            object Create(SimpleContainer container);
        }
        interface IResolver
        {
            object Resolve(SimpleContainer container);
        }
        class InstanceResolver : IResolver, IDisposable
        {
            public InstanceResolver()
            {
                OwnInstance = true;
            }
            public virtual void Dispose()
            {
                if (OwnInstance && Instance is IDisposable)
                {
                    ((IDisposable)Instance).Dispose();
                }
                Instance = null;
            }
            public virtual object Resolve(SimpleContainer container) { return Instance; }
            public object Instance { get; set; }
            public bool OwnInstance { get; set; }
        }
        class CreatorResolver<TToResolved> : InstanceResolver, ICreator
        {
            public CreatorResolver()
            {
                IsSingleton = true;
            }
            public object Create(SimpleContainer container)
            {
                return Creator(container);
            }
            public override object Resolve(SimpleContainer container)
            {
                if (IsSingleton && Instance != null) return Instance;
                object result = Create(container);
                if (IsSingleton)
                    Instance = result;
                return result;
            }
            public Func<IDependencyContainer, TToResolved> Creator { get; set; }
            public bool IsSingleton { get; set; }
        }
        Dictionary<Type, IResolver> _Resolvers = new Dictionary<Type, IResolver>();

        /// <summary>
        /// Register a resolver by creator
        /// </summary>
        public IDependencyContainer Register<TToResolved>(Func<IDependencyContainer, TToResolved> creator, bool asSingleton = true)
        {
            Check.ArgumentNotNull(creator, "creator");
            _Resolvers[typeof(TToResolved)] = new CreatorResolver<TToResolved>() {
                Creator = creator,
                Instance = null,
                IsSingleton = asSingleton
            };
            return this;
        }

        /// <summary>
        /// Register a type
        /// </summary>
        /// <typeparam name="TToResolved"></typeparam>
        /// <typeparam name="TConcreteType"></typeparam>
        /// <param name="asSingleton"></param>
        /// <returns></returns>
        public IDependencyContainer Register<TToResolved, TConcreteType>(bool asSingleton = true)
            where TConcreteType : class, TToResolved
        {
            return Register<TToResolved>(cnt => (TToResolved)DefaultCreateInstance(typeof(TConcreteType)), asSingleton);
        }

        /// <summary>
        /// Register an instance to resolved a type
        /// </summary>
        public IDependencyContainer RegisterInstance<TToResolved>(TToResolved instance, bool ownInstance = false)
        {
            Check.ArgumentNotNull(instance, "instance");
            _Resolvers[typeof(TToResolved)] = new InstanceResolver() {
                Instance = instance,
                OwnInstance = ownInstance
            };
            return this;
        }

        /// <summary>
        /// Release resources
        /// </summary>
        public void Dispose()
        {
            foreach (var resolver in _Resolvers.Values)
            {
                if (resolver is IDisposable)
                    ((IDisposable)resolver).Dispose();
            }
            _Resolvers.Clear();
        }

        /// <summary>
        /// Check if we can resolve a type
        /// </summary>
        public bool CanResolve(Type type)
        {
            if (type == null) return false;
            return _Resolvers.ContainsKey(type);
        }

        /// <summary>
        /// Resolve a type
        /// </summary>
        public object Resolve(Type type)
        {
            Check.ArgumentNotNull(type, "type");
            IResolver resolver = null;
            if (!_Resolvers.TryGetValue(type, out resolver))
                throw new InvalidOperationException(String.Format(LSR.Dependency_Error_CantResolveType, type.FullName));
            return resolver.Resolve(this);
        }

        /// <summary>
        /// Try to resolve a type
        /// </summary>
        public bool TryToResolve(Type type, out object resolved)
        {
            Check.ArgumentNotNull(type, "type");
            if (CanResolve(type))
            {
                resolved = Resolve(type);
                return true;
            }
            // Create
            resolved = Create(type);
            return false;
        }

        /// <summary>
        /// Create a new instance of a type
        /// </summary>
        public object Create(Type type)
        {
            Check.ArgumentNotNull(type, "type");
            IResolver resolver;
            if (_Resolvers.TryGetValue(type, out resolver))
            {
                ICreator creator = resolver as ICreator;
                if (creator != null)
                    return creator.Create(this);
            }
            try
            {
                return DefaultCreateInstance(type);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Build constructor parameters
        /// </summary>
        protected List<object> BuildConstructorParameter(Type type, ConstructorInfo constructor)
        {
            var result = new List<object>();
            foreach (var parameterInfo in constructor.GetParameters())
            {
                object pValue;
                if (!TryToResolve(parameterInfo.ParameterType, out pValue))
                {
                    if (parameterInfo.IsOptional)
                    {
                        pValue = Type.Missing;
                    }
                    else
                    {
                        throw new InvalidOperationException(String.Format(
                            LSR.Dependency_Error_CantResolveParameterForConstruct,
                            parameterInfo.Name,
                            parameterInfo.ParameterType.Name,
                            type.FullName));
                    }
                }
                result.Add(pValue);
            }
            return result;
        }

        /// <summary>
        /// Create a new instance of type by reflection
        /// </summary>
        protected object DefaultCreateInstance(Type type)
        {
            // Check type
            if (type.IsInterface || type.IsAbstract || type.IsByRef || type.IsGenericType)
                throw new InvalidOperationException(String.Format(LSR.Dependency_Error_CantInstanciateType, type.FullName));
            ConstructorInfo constructor = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault();
            if (constructor == null)
                throw new InvalidOperationException(String.Format(LSR.Dependency_Error_CantFindConstructor, type.FullName));
            // Build parameters
            List<object> parameters = BuildConstructorParameter(type, constructor);
            // Create instance
            return Activator.CreateInstance(type, parameters.ToArray());
        }

    }

}
