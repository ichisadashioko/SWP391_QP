namespace QuizPractice.Domain.Common;

/// <summary>One page of a list screen plus the number of rows matching the criteria.</summary>
public sealed record Page<T>(IReadOnlyList<T> Items, int TotalCount, int PageNumber, int PageSize)
{
    public int TotalPages => PageSize == 0 ? 0 : (int)Math.Ceiling(TotalCount / (double)PageSize);

    public Page<TOut> Map<TOut>(Func<T, TOut> map) =>
        new(Items.Select(map).ToList(), TotalCount, PageNumber, PageSize);
}

/// <summary>Paging asked for by a list screen. Page numbers start at 1.</summary>
public sealed record PageRequest(int PageNumber = 1, int PageSize = 10)
{
    public const int MaxPageSize = 100;

    public int Skip => (PageNumber - 1) * PageSize;
}
