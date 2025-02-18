using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using MediatR;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace MusteriAPI.Application.Features.Musteriler.Commands.CreateMusteri
{
    public class CreateMusteriCommand : IRequest<int>
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Eposta { get; set; }
        public string TelefonNumarasi { get; set; }
        public string Adres { get; set; }
    }

    public class CreateMusteriCommandHandler : IRequestHandler<CreateMusteriCommand, int>
    {
        private readonly IConfiguration _configuration;

        public CreateMusteriCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> Handle(CreateMusteriCommand request, CancellationToken cancellationToken)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            
            var musteriJson = JsonConvert.SerializeObject(request);
            
            var parameters = new DynamicParameters();
            parameters.Add("@Ad", request.Ad);
            parameters.Add("@Soyad", request.Soyad);
            parameters.Add("@Eposta", request.Eposta);
            parameters.Add("@TelefonNumarasi", request.TelefonNumarasi);
            parameters.Add("@Adres", request.Adres);
            parameters.Add("@MusteriVerisi", musteriJson);

            var id = await connection.ExecuteScalarAsync<int>(
                "sp_MusteriEkle",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return id;
        }
    }
} 