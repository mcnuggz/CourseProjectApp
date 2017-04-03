using CourseProjectApp.Models.CSharp;
using Microsoft.AspNetCore.Mvc;

namespace CourseProjectApp.Controllers
{
    public class MainController : Controller
    {
        //first instance
        FirstClass _firstClass = new FirstClass();

        //second instance
        private readonly FirstClass _secondClass;
        private readonly AccessModifiers _accessModifier;

        public MainController(FirstClass secondClass, AccessModifiers accessModifier)
        {
            _secondClass = secondClass;
            _accessModifier = accessModifier;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.value = "Hello There";
            _firstClass.MainValue = "Main Value";

            //third instance
            string value = "First Third Value";
            FirstClass thirdClass = new FirstClass(value);

            //Methods
            _secondClass.NoReturn("Great Value");
            var boolValue = _secondClass.TrueFalse(6);

            //accessors
            //_accessModifier.PublicString = "Zerg Rush";
            //_accessModifier.PublicMethod();

            return View();
        }
    }
}
