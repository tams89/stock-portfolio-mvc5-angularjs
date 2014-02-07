using System;

namespace Core.Repository
{
    /// <summary>
    /// Contract to entities.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        Guid Id { get; set; }
    }
}
