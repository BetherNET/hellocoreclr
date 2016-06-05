﻿using Microsoft.AspNetCore.Mvc;
using NLog;

namespace HelloWorldApp
{
    [Controller]
    [Route("api")]
    public class HelloWorldController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        IActionFactory actionFactory;
        
        public HelloWorldController(IActionFactory actionFactory)
        {
            this.actionFactory = actionFactory;
        }
        
        [Route("helloworld/{name}")]
        [HttpGet]
        public IActionResult GetHelloWorld(string name)
        {
            logger.Info(string.Format("'HelloWorld' Request received with '{0}'.",name));
            
            var action = actionFactory.CreateGetHelloWorldAction();
            var response = action.Execute(name);
            return new OkObjectResult(response);
        }
    }
}
