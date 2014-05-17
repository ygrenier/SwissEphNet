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
        /// Indicate if we can resolve a type
        /// </summary>
        bool CanResolve(Type type);

        /// <summary>
        /// Resolve a type
        /// </summary>
        object Resolve(Type type);

        /// <summary>
        /// Create a new instance of a type
        /// </summary>
        object Create(Type type);

    }

}
