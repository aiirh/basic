namespace Aiirh.WebTools.Cache
{
    public enum CacheExpirationType
    {
        /// <summary>
        /// No caching, default state
        /// </summary>
        None = 0,

        /// <summary>
        /// Absolute expiration.
        /// For example-> We have Absolute expiration in 1 day,
        /// it means that it will definitly expire in 1 day.
        /// </summary>
        Absolute = 1,

        /// <summary>
        /// Sliding cache expiration
        /// For example-> We have Sliding expiration in 1 hour,
        /// it will expire in 1 hour only in a case when no calls to
        /// cacheManager.Get() with the same key will be made
        /// </summary>
        Relative = 2,

        /// <summary>
        /// Only restarting APP helps
        /// </summary>
        Immortal = 3
    }
}
