using Microsoft.AspNetCore.Mvc;
using Moq;

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
