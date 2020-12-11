using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTestStudent
{
    public class ModelStateTestController : ControllerBase
    {
        public ModelStateTestController()
        {
            ControllerContext = (new Mock<ControllerContext>()).Object;
        }

        public bool TestTryValidateModel(object model)
        {
            return TryValidateModel(model);
        }
    }
}
