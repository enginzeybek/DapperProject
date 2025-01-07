using Dapper;
using DapperProject.DTOs.GuideDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace DapperProject.Controllers
{
    public class GuideController : Controller
    {
        SqlConnection sqlConnection = new SqlConnection("Server=LAPTOP-2S32U8I2;initial Catalog=DapperDb;integrated Security=true;");
        public async Task<IActionResult> Index()
        {
            string query = "select * from guide";

            var value = await sqlConnection.QueryAsync<ResultGuideDto>(query);

            return View(value);
        }

        [HttpGet]
        public IActionResult CreateGuide()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> CreateGuide(CreateGuideDto createGuideDto)
        {
            string query = "insert into guide (guideName) values (@GuideName)";

            var parametre = new DynamicParameters();

            parametre.Add("@GuideName", createGuideDto.guideName);

            await sqlConnection.ExecuteAsync(query, parametre);

            return RedirectToAction("Index");
        }

        [HttpGet]

        public IActionResult UpdateGuide(int id)
        {
            string query = "select * from guide where guideId = @GuideId";

            var parametre = new DynamicParameters();

            parametre.Add("@GuideId", id);

            var value = sqlConnection.QueryFirstOrDefault<UpdateGuideDto>(query, parametre);

            return View(value);
        }

        [HttpPost]

        public async Task<IActionResult> UpdateGuide(UpdateGuideDto updateGuideDto)
        {
            string query = "update guide set guideName = @GuideName where guideId = @guideId";

            var parametre = new DynamicParameters();

            parametre.Add("@GuideName", updateGuideDto.guideName);
            parametre.Add("@guideId", updateGuideDto.guideId);

            await sqlConnection.ExecuteAsync(query, parametre);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteGuide(int id)
        {
            string query = "delete from guide where guideId = @GuideId";

            var parametre = new DynamicParameters();

            parametre.Add("@GuideId", id);

            await sqlConnection.ExecuteAsync(query, parametre);

            return RedirectToAction("Index");
        }
    }
}
