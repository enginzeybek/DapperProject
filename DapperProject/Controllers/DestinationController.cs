using Dapper;
using DapperProject.DTOs.DestinationDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;


namespace DapperProject.Controllers
{
    public class DestinationController : Controller
    {
        SqlConnection sqlConnection = new SqlConnection("Server=LAPTOP-2S32U8I2;initial Catalog=DapperDb;integrated Security=true;");
        public async Task<IActionResult> Index()
        {
            string query = "select * from destination";

            var value = await sqlConnection.QueryAsync<ResultDestinationDto>(query);

            return View(value);

            
        }

        [HttpGet]

        public IActionResult CreateDestination()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDestination(CreateDestinationDto createDestinationDto)
        {
            string query = "insert into destination (destinationCity,guideId) values (@DestinationCity,@GuideId)";

            var parametre = new DynamicParameters();

            parametre.Add("@DestinationCity", createDestinationDto.destinationCity);
            parametre.Add("@GuideId", createDestinationDto.guideId);

            await sqlConnection.ExecuteAsync(query, parametre);

            return RedirectToAction("Index");
            
        }

        [HttpGet]
        public IActionResult UpdateDestination(int id)
        {
            string query = "select * from destination where destinationId = @DestinationId";

            var parametre = new DynamicParameters();

            parametre.Add("@DestinationId", id);

            var value = sqlConnection.QueryFirstOrDefault<UpdateDestinationDto>(query, parametre);

            if (value == null)
            {
                return NotFound();
            }
           
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDestination(UpdateDestinationDto updateDestinationDto)
        {
            string query = "update destination set destinationCity = @DestinationCity, guideId = @GuideId where destinationId = @DestinationId";

            var parametre = new DynamicParameters();

            parametre.Add("@DestinationCity", updateDestinationDto.destinationCity);
            parametre.Add("@GuideId", updateDestinationDto.guideId);
            parametre.Add("@DestinationId", updateDestinationDto.destinationId);

            await sqlConnection.ExecuteAsync(query, parametre);

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> DeleteDestination(int id)
        {
            string query = "delete from destination where destinationId = @DestinationId";

            var parametre = new DynamicParameters();

            parametre.Add("@DestinationId", id);

            await sqlConnection.ExecuteAsync(query,parametre);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ResultDestinationWithGuideName()
        {
            string query = "select d.destinationCity,g.guideName from destination as d inner join guide as g on d.guideId = g.guideId";

            var value = await sqlConnection.QueryAsync<ResultDestinationWithGuideName>(query);

            return View(value);
        }
    }
}
