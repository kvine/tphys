using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using AuthServer.PhysxVerify;
using com.golf.proto;
using LitJson;
namespace VerifyServerWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VerifyController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<VerifyController> _logger;

        public VerifyController(ILogger<VerifyController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            // byte[] t = { 8, 0, 16, 188, 25, 24, 0, 32, 0, 40, 62, 48, 0, 58, 6, 8, 0, 16, 0, 24, 0, 66, 6, 8, 0, 16, 0, 24, 0, 74, 17, 8, 244, 243, 252, 255, 255, 255, 255, 255, 255, 1, 16, 0, 24, 172, 193, 29, 82, 26, 8, 145, 145, 247, 255, 255, 255, 255, 255, 255, 1, 16, 154, 138, 3, 24, 236, 162, 200, 255, 255, 255, 255, 255, 255, 1, 90, 9, 8, 0, 16, 0, 24, 0, 32, 144, 78, 96, 0, 104, 250, 1, 104, 100, 104, 90, 104, 0, 104, 0, 104, 200, 1, 120, 35 };
            byte[] t = {8,0,16,188,25,24,0,32,0,40,62,48,0,58,6,8,0,16,0,24,0,66,6,8,0,16,0,24,0,74,9,8,224,43,16,0,24,172,193,29,82,26,8,145,145,247,255,255,255,255,255,255,1,16,154,138,3,24,236,162,200,255,255,255,255,255,255,1,90,9,8,0,16,0,24,0,32,144,78,96,0,104,250,1,104,100,104,90,104,0,104,0,104,200,1,120,35};
            // byte[] t = {8,1,16,213,23,24,203,181,10,32,160,144,230,1,40,56,48,0,58,6,8,0,16,0,24,0,66,6,8,0,16,0,24,0,74,26,8,208,207,254,255,255,255,255,255,255,1,16,241,163,253,255,255,255,255,255,255,1,24,228,196,50,82,26,8,176,186,254,255,255,255,255,255,255,1,16,250,170,253,255,255,255,255,255,255,1,24,135,216,51,90,28,8,181,182,255,255,255,255,255,255,255,1,16,240,251,255,255,255,255,255,255,255,1,24,227,1,32,234,25,96,0,104,20,104,100,104,30,104,30,104,100,104,200,1,120,0};
            //byte[] t = {8,1,16,163,27,24,220,242,255,255,255,255,255,255,255,1,32,151,92,40,56,48,0,58,6,8,0,16,0,24,0,66,6,8,0,16,0,24,0,74,26,8,208,207,254,255,255,255,255,255,255,1,16,241,163,253,255,255,255,255,255,255,1,24,228,196,50,82,26,8,176,186,254,255,255,255,255,255,255,1,16,250,170,253,255,255,255,255,255,255,1,24,135,216,51,90,28,8,181,182,255,255,255,255,255,255,255,1,16,240,251,255,255,255,255,255,255,255,1,24,227,1,32,234,25,96,0,104,20,104,100,104,30,104,30,104,100,104,200,1,120,0};
            // byte[] t = {8,1,16,178,20,24,188,105,32,132,107,40,56,48,0,58,6,8,0,16,0,24,0,66,6,8,0,16,0,24,0,74,26,8,208,207,254,255,255,255,255,255,255,1,16,241,163,253,255,255,255,255,255,255,1,24,228,196,50,82,26,8,176,186,254,255,255,255,255,255,255,1,16,250,170,253,255,255,255,255,255,255,1,24,135,216,51,90,28,8,181,182,255,255,255,255,255,255,255,1,16,240,251,255,255,255,255,255,255,255,1,24,227,1,32,234,25,96,0,104,20,104,100,104,30,104,30,104,100,104,200,1,120,0};
            
            CCStrickData ccStrickData = CCStrickData.ParseFrom(t);
            int sceneId = 1;
            string sceneName = "3DGolf_XCJ_xinShou_02";
            string filePath = string.Format("./SceneDatas/{0}.xml",sceneName);

            VerifyServerWebApp.UnityEngine.Vector3 golfHolePos = new VerifyServerWebApp.UnityEngine.Vector3(-2.257f, -4.496f, 82.8f);

            CCRoundResultStatus ccResultStatus = Verify.CalculateResult(sceneId, sceneName, filePath, ccStrickData, golfHolePos);

            if(ccResultStatus == null)
            {
                return "verify failed!";
            }

            byte[] resData = ccResultStatus.ToByteArray();
            string resDataStr = Convert.ToBase64String(resData);
            string res = "{\"data\":\"" + resDataStr + "\"}";
            return res;
        }

        // // GET api/verify
        // [HttpGet]
        // public ActionResult<IEnumerable<string>> Get()
        // {
        //     Console.WriteLine("----------get verify");
        //     return new string[] { "value1", "value2" };
        
        // }

        // GET api/verify/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "000";
        }

         // POST api/verify
        [HttpPost]
        public String  Id(Object value)
        {
            VerifyRequest request = JsonMapper.ToObject<VerifyRequest>(value.ToString());
            byte[] data = Convert.FromBase64String(request.data);
            CCGetVerifyServerRoundResultRequest verifyRqt = CCGetVerifyServerRoundResultRequest.ParseFrom(data);
            
            CCStrickData ccStrickData = verifyRqt.StrickData;
            int sceneId = verifyRqt.SceneId;
            string sceneName = verifyRqt.SceneName;
            string filePath = string.Format("./SceneDatas/{0}.xml",sceneName);
            float x = MyMathUtils.ConvertLongToFloat(verifyRqt.HolePos.X);
            float y = MyMathUtils.ConvertLongToFloat(verifyRqt.HolePos.Y);
            float z = MyMathUtils.ConvertLongToFloat(verifyRqt.HolePos.Z);
            VerifyServerWebApp.UnityEngine.Vector3 golfHolePos = new VerifyServerWebApp.UnityEngine.Vector3(x, y, z);

            CCRoundResultStatus ccResultStatus = Verify.CalculateResult(sceneId, sceneName, filePath, ccStrickData, golfHolePos);

            if(ccResultStatus == null)
            {
                return "verify failed!";
            }

            byte[] resData = ccResultStatus.ToByteArray();
            string resDataStr = Convert.ToBase64String(resData);
            string res = "{\"data\":\"" + resDataStr + "\"}";
            return res;
        }

// PUT api/verify/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/verify/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class VerifyRequest{
        public string data;
    }
}
