using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwephNet
{

    /// <summary>
    /// Interface for dependency resolver
    /// </summary>
    public interface IDependencyResolver
    {

        /// <summary>
        /// Indicate if we can resolve a type
        /// </summary>
        bool CanResolve<T>();

        /// <summary>
        /// Resolve a type
        /// </summary>
        T Resolve<T>();

        /// <summary>
        /// Create a new instance of a type
        /// </summary>
        T Create<T>();

    }

}
