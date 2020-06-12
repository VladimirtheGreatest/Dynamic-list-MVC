using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EditableListMvc.Controllers
{
    public class InvitationListController : Controller
    {
        //this will be extracted from the excel file, I will populate the list based on that
        private static List<Models.Person> MockDb = new List<Models.Person>
            {
                new Models.Person { PrimaryKey = 1, FirstName = "Fred", LastName="Than", DateOfBirth = new DateTime(1990, 07, 17), Email = "vlad@gmail.com" },
                new Models.Person { PrimaryKey = 2, FirstName = "Erin", LastName="Saavedra", DateOfBirth = new DateTime(1990, 07, 17), Email = "vlad@gmail.com"},
                new Models.Person { PrimaryKey = 4, FirstName = "Abdul", LastName = "Banas", DateOfBirth = new DateTime(1990, 07, 17), Email = "vlad@gmail.com"},
                new Models.Person { PrimaryKey = 4, FirstName = "Abdul", LastName = "Banas", DateOfBirth = new DateTime(1990, 07, 17), Email = "vlad@gmail.com"},
                new Models.Person { PrimaryKey = 4, FirstName = "Abdul", LastName = "Banas", DateOfBirth = new DateTime(1990, 07, 17), Email = "vlad@gmail.com"},
                new Models.Person { PrimaryKey = 4, FirstName = "Abdul", LastName = "Banas", DateOfBirth = new DateTime(1990, 07, 17), Email = "vlad@gmail.com"}
            };

        private List<Models.Person> _personList;
        private List<Models.Person> PersonList
        {
            get
            {
                if (_personList == null)
                    _personList = Session["PersonList"] as List<Models.Person>;
                return _personList;
            }
            set
            {
                _personList = value;
                Session["PersonList"] = _personList;
            }
        }

        // GET: PersonList
        public ActionResult Index()
        {
            if (PersonList == null)
            {

                PersonList = MockDb;
            }
            return View(PersonList);
        }

        [HttpPost]
        public ActionResult Index(List<Models.Person> personList, string command)
        {
            try
            {
                if (command == "Add Item")
                {
                    personList.Add(new Models.Person { PrimaryKey = -1 });
                    PersonList = personList;
                }
                else if (command == "Remove Selected")
                {
                    int pos = personList.Count();
                    while (pos > 0)
                    {
                        pos--;
                        if (personList[pos].Remove)
                            personList.RemoveAt(pos);
                    }
                    PersonList = personList;
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        return RedirectToAction("About", "Home");
                        //redirect to action updatebulk data controller
                    }
                }
                return View(personList);

            }
            catch
            {
                return View();
            }
        }

        public ActionResult CheckUserNameExists([Bind(Prefix = "[0].FirstName")]string FirstName)

        {
            bool UserExists = false;
            try

            {
                //call to the database or endpoint api here
                var nameexits = MockDb.Where(x => x.FirstName == FirstName.Trim());

                if (nameexits.Count() > 0)
                {
                    UserExists = true;
                }
                else
                {
                    UserExists = false;
                }

                return Json(!UserExists, JsonRequestBehavior.AllowGet);

            }

            catch (Exception)

            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
