using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using MediatR;
using Newtonsoft.Json;

namespace MusteriAPI.Application.Features.Musteriler.Commands.UpdateMusteri
{
    public class UpdateMusteriCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Eposta { get; set; }
        public string TelefonNumarasi { get; set; }
        public string Adres { get; set; }
    }

    public class UpdateMusteriCommandHandler : IRequestHandler<UpdateMusteriCommand, bool>
    {
        private readonly IConfiguration _configuration;

        public UpdateMusteriCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> Handle(UpdateMusteriCommand request, CancellationToken cancellationToken)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            
            var musteriJson = JsonConvert.SerializeObject(request);
            
            var parameters = new DynamicParameters();
            parameters.Add("@Id", request.Id);
            parameters.Add("@Ad", request.Ad);
            parameters.Add("@Soyad", request.Soyad);
            parameters.Add("@Eposta", request.Eposta);
            parameters.Add("@TelefonNumarasi", request.TelefonNumarasi);
            parameters.Add("@Adres", request.Adres);
            parameters.Add("@MusteriVerisi", musteriJson);

            var affected = await connection.ExecuteAsync(
                "sp_MusteriGuncelle",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return affected > 0;
        }
    }
} 