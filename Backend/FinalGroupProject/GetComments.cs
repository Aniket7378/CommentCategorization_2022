using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Web;
using System.Collections.Generic;
using GenericLibrary.Model;
using FinalGroupProject.SQLRepository;
using GenericLibrary.Database;

namespace FinalGroupProject
{
    public class GetComments
    {
        private ISqlRepository _sqlRepository;

        public GetComments(ISqlRepository sqlRepository, ISqlDbConnection databaseConnection)
        {
            _sqlRepository = sqlRepository;
            _sqlRepository.DatabaseConnection = databaseConnection;
        }
        [FunctionName("GetComments")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "commentTag_mapping")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                string orderBy = "", checkBox = "", comment = "", name = "", city = "", label = "";
                if (req.Query.ContainsKey("order"))
                {
                    orderBy = req.Query["order"];
                }
                if (req.Query.ContainsKey("check-box"))
                {
                    checkBox = req.Query["check-box"];
                }
                if (req.Query.ContainsKey("comment"))
                {
                    comment = req.Query["comment"];
                }
                if (req.Query.ContainsKey("name"))
                {
                    name = req.Query["name"];
                }
                if (req.Query.ContainsKey("city"))
                {
                    city = req.Query["city"];
                }
                if (req.Query.ContainsKey("label"))
                {
                    label = req.Query["label"];
                }



                List<CommentTag> comments = _sqlRepository.GetComments(orderBy, checkBox, comment, name, city, label);

                if (comments == null)
                {
                    return new StatusCodeResult(StatusCodes.Status204NoContent);
                }

                return new OkObjectResult(comments);

            }
            catch (Exception ex)
            {
                log.LogError(ex, ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
