using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwephNet
{

    /// <summary>
    /// Interface for dependency container
    /// </summary>
    public interface IDependencyContainer : IDisposable
    {
        /// <summary>
        /// Register a type resolver
        /// </summary>
        IDependencyContainer Register<TToResolved>(Func<IDependencyContainer, TToResolved> creator, bool asSingleton = true);

        /// <summary>
        /// Register a type resolved by another type
        /// </summary>
        IDependencyContainer Register<TToResolved, TConcreteType>(bool asSingleton = true)
            where TConcreteType : class, TToResolved;

        /// <summary>
        /// Register an instance to resolved a type
        /// </summary>
        IDependencyContainer RegisterInstance<TToResolved>(TToResolved instance, bool ownInstance = false);

        /// <summary>
        /// Indicate if we can resolve a registered type
        /// </summary>
        bool CanResolve(Type type);

        /// <summary>
        /// Resolve a type, or raise exception if we can't resolve it
        /// </summary>
        object Resolve(Type type);

        /// <summary>
        /// Try to resolve a registered type. If not registered, try to create a new instance
        /// </summary>
        /// <param name="type">Type to resolved</param>
        /// <param name="resolved">Resolved object</param>
        /// <returns>True is type can be resolved</returns>
        /// <remarks>
        /// If the type is not registered, this method returns false. 
        /// But if the type can be created, <paramref name="resolved"/> contains the 
        /// instance created, otherwise <paramref name="resolved"/> contains null.
        /// </remarks>
        bool TryToResolve(Type type, out object resolved);

        /// <summary>
        /// Create a new instance of a registered type or if not registered 
        /// try to create by reflection or null if we can't create it.
        /// </summary>
        object Create(Type type);

    }

}
