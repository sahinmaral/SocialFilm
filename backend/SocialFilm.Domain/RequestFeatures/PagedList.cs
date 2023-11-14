namespace SocialFilm.Domain.RequestFeatures;

public class PagedList<T> : List<T>
{
    public MetaData MetaData { get; set; }
    public PagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        int totalPages = (int)Math.Ceiling(count / (double)pageSize);

        MetaData = new MetaData()
        {
            TotalRecords = count,
            PageSize = pageSize,
            CurrentPage = pageNumber,
            IsFirstPage = pageNumber == 1,
            IsLastPage = pageNumber == totalPages,
            TotalPages = totalPages
        };

        AddRange(items);
    }

    public static PagedList<T> ToPagedList(IOrderedQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = source.Count();
        var items = source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}
