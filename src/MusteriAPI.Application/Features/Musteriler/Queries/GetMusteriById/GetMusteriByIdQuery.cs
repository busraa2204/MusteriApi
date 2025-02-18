using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using MediatR;
using MusteriAPI.Domain.Entities;

namespace MusteriAPI.Application.Features.Musteriler.Queries.GetMusteriById
{
    public class GetMusteriByIdQuery : IRequest<Musteri>
    {
        public int Id { get; set; }
    }

    public class GetMusteriByIdQueryHandler : IRequestHandler<GetMusteriByIdQuery, Musteri>
    {
        private readonly IConfiguration _configuration;

        public GetMusteriByIdQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Musteri> Handle(GetMusteriByIdQuery request, CancellationToken cancellationToken)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            
            var musteri = await connection.QueryFirstOrDefaultAsync<Musteri>(
                "sp_MusteriGetirById",
                new { Id = request.Id },
                commandType: CommandType.StoredProcedure
            );

            return musteri;
        }
    }
} 