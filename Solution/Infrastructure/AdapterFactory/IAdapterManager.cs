namespace Domain.AdapterFactory
{
    
    /// An adapter manager maintains a registry of adapter factories that
    /// it uses to lookup a factory that can be used to get an adapter for a particular
    /// type of object.
    
    public interface IAdapterManager
    {
        
        /// Returns an adaptation of the given adaptee object to the type <see cref="TAdapter"/>,
        /// or <c>null</c> if that is not possible (i.e., if no adapter factory exists for the
        /// given adaptation or if the factory returns <c>null</c>).
        
        /// <typeparam name="TAdapter">Type to adapt the object to.</typeparam>
        /// <param name="adaptee"></param>
        /// <returns></returns>
        TAdapter? GetAdapter<TAdapter>(object? adaptee) where TAdapter : class;
    }
}