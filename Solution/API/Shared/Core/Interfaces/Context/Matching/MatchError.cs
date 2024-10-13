namespace API.Shared.Core.Interfaces.Context.Matching
{
    /// <summary>
    /// Match error types set from matchers.
    /// </summary>
    public enum MatchError
    {
        NoneFoundShouldCreate,
        NoneFoundDontCreate,
        MultipleFound
    }
}
