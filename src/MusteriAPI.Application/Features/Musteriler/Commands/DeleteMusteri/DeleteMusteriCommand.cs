using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using MediatR;

namespace MusteriAPI.Application.Features.Musteriler.Commands.DeleteMusteri
{
    public class DeleteMusteriCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteMusteriCommandHandler : IRequestHandler<DeleteMusteriCommand, bool>
    {
        private readonly IConfiguration _configuration;

        public DeleteMusteriCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> Handle(DeleteMusteriCommand request, CancellationToken cancellationToken)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            
            var affected = await connection.ExecuteAsync(
                "sp_MusteriSil",
                new { Id = request.Id },
                commandType: CommandType.StoredProcedure
            );

            return affected > 0;
        }
    }
} 