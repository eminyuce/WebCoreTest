using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebCoreTest.Services;

namespace WebCoreTest.Controllers
{
    [Produces("application/json")]
    [Route("api/Calculator")]
    public class DefaultController : BaseController
    {
        private readonly ITestService _testService;

        public DefaultController(ITestService testService)
        {
            _testService = testService;
        }
        //http://emin.fastcraft.com/api/Calculator/TestSum/1/2
        /// <summary>
        ///  Case Sensitive
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [HttpGet("TestSum/{x}/{y}")]
        public IActionResult TestSum(int x, int y)
        {
            var result = _testService.TestSum(x, y);
            return new JsonResult(result);
        }
        [HttpGet("TestSum2/{x}/{y}")]
        public IActionResult TestSum2(int x, int y)
        {
            var result = _testService.TestSum(x, y) * 10;
            return new JsonResult(result);
        }
    }
}