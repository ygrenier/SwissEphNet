using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwephNet
{
    /// <summary>
    /// Extension methods for dependency container
    /// </summary>
    public static class DependencyExtensions
    {

        /// <summary>
        /// Indicate if we can resolve a type
        /// </summary>
        public static bool CanResolve<T>(this IDependencyContainer container) where T : class
        {
            if (container == null) return false;
            return container.CanResolve(typeof(T));
        }

        /// <summary>
        /// Resolve a type
        /// </summary>
        public static T Resolve<T>(this IDependencyContainer container) where T : class
        {
            Check.ArgumentNotNull(container, "container");
            return (T)container.Resolve(typeof(T));
        }

        /// <summary>
        /// Try to resolve a type
        /// </summary>
        public static bool TryToResolve<T>(this IDependencyContainer container, out T resolved)
        {
            Check.ArgumentNotNull(container, "container");
            object res = null;
            var result = container.TryToResolve(typeof(T), out res);
            resolved = (T)res;
            return result;
        }

        /// <summary>
        /// Create a new instance of a type
        /// </summary>
        public static T Create<T>(this IDependencyContainer container) where T : class
        {
            Check.ArgumentNotNull(container, "container");
            return (T)container.Create(typeof(T));
        }

    }

}
