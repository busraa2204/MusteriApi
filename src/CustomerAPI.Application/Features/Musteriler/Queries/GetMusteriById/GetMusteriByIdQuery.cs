using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MediatR;
using Dapper;

namespace CustomerAPI.Application.Features.Musteriler.Queries.GetMusteriById
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