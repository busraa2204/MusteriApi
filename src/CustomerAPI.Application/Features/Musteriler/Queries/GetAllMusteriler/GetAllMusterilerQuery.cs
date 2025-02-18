using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using MediatR;

namespace CustomerAPI.Application.Features.Musteriler.Queries.GetAllMusteriler
{
    public class GetAllMusterilerQuery : IRequest<IEnumerable<Musteri>>
    {
    }

    public class GetAllMusterilerQueryHandler : IRequestHandler<GetAllMusterilerQuery, IEnumerable<Musteri>>
    {
        private readonly IConfiguration _configuration;

        public GetAllMusterilerQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Musteri>> Handle(GetAllMusterilerQuery request, CancellationToken cancellationToken)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            
            var musteriler = await connection.QueryAsync<Musteri>(
                "sp_TumMusterileriGetir",
                commandType: CommandType.StoredProcedure
            );

            return musteriler;
        }
    }
} 