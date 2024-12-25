namespace Domain.AdapterFactory
{
    /// <summary>
    /// An adapter factory defines behavioral extensions for one or more classes.
    /// Adapter factories are used indirectly via an <see cref="IAdapterManager"/>.
    ///
    /// Implementations should determine if a given object of type <see cref="TAdaptee"/> can
    /// be adapted by the type <see cref="TAdapter"/>, thereby extending its behavior, and if
    /// so build an instance of that type.
    /// </summary>
    /// <typeparam name="TAdaptee"></typeparam>
    /// <typeparam name="TAdapter"></typeparam>
    public interface IAdapterFactory<in TAdaptee, out TAdapter>
    {
        /// <summary>
        /// Returns an adapter for the given <see cref="adaptee"/> instance or <c>null</c> if no adapter
        /// applies to the given object instance.
        ///
        /// This method returns the same instance when called multiple times.
        /// </summary>
        /// <param name="adaptee">Object instance of type <see cref="TAdaptee"/> to be adapted</param>
        /// <returns><see cref="TAdapter"/> instance for the given object or <c>null</c></returns>
        public TAdapter? GetAdapter(TAdaptee adaptee);
    }
}