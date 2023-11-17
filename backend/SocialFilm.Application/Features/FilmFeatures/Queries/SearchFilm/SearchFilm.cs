using AutoMapper;

using MediatR;

using SocialFilm.Application.ApiClients;
using SocialFilm.Application.Models;
using SocialFilm.Domain.DTOs;
using SocialFilm.Domain.RequestFeatures;

namespace SocialFilm.Application.Features.FilmFeatures.Queries.SearchFilm;

public sealed record SearchFilmQuery(string Name, string? ReleaseYear, int Page = 1) : IRequest<PaginationResult<ReadFilmDetailDTO>>;

public sealed class SearchFilmQueryHandler : IRequestHandler<SearchFilmQuery, PaginationResult<ReadFilmDetailDTO>>
{
    private readonly ITMDBApiClient _apiClient;
    private readonly IMapper _mapper;

    public SearchFilmQueryHandler(ITMDBApiClient apiClient, IMapper mapper)
    {
        _apiClient = apiClient;
        _mapper = mapper;
    }

    public async Task<PaginationResult<ReadFilmDetailDTO>> Handle(SearchFilmQuery request, CancellationToken cancellationToken)
    {
        SearchFilmResponseModel searchFilmResponseModel = await _apiClient.SearchFilmsByQueryAsync(request);

        List<ReadFilmDetailDTO> mappedSearchFilmResponseModelData = _mapper.Map<List<ReadFilmDetailDTO>>(searchFilmResponseModel.Results);

        MetaData metaData = new MetaData()
        {
            CurrentPage = searchFilmResponseModel.Page,
            TotalPages = searchFilmResponseModel.Total_Pages,
            TotalRecords = searchFilmResponseModel.Total_Results,
            IsFirstPage = searchFilmResponseModel.Page == 1,
            IsLastPage = searchFilmResponseModel.Total_Pages == searchFilmResponseModel.Page
        };

        PaginationResult<ReadFilmDetailDTO> paginationResult =
            new PaginationResult<ReadFilmDetailDTO>
            (
                Data: mappedSearchFilmResponseModelData,
                MetaData: metaData
            );

        return paginationResult;
    }

}




