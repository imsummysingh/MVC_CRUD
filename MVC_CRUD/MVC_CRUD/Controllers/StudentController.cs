using MVC_CRUD.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_CRUD.Controllers
{
    public class StudentController : Controller
    {
        db_testEntities dbObj = new db_testEntities();      //database ka object banaya hai
        // GET: Student
        public ActionResult Student(tbl_Student obj)
        {
            if (obj != null)
            {
                return View(obj);
            }
            else
            {
                return View();
            }           
        }
        [HttpPost]
        public ActionResult AddStudent(tbl_Student model)    //data layega kaun?
        {
            if (ModelState.IsValid)
            {
                tbl_Student obj = new tbl_Student();    //yeh wo obj hai jisme data recieve hoga
                obj.ID = model.ID;
                obj.Name = model.Name;
                obj.Fname = model.Fname;
                obj.Email = model.Email;
                obj.Mobile = model.Mobile;
                obj.Description = model.Description;

                if (model.ID == 0)
                {
                    dbObj.tbl_Student.Add(obj);
                    dbObj.SaveChanges();
                }
                else
                {
                    dbObj.Entry(obj).State = EntityState.Modified;
                    dbObj.SaveChanges();
                }
            }

            ModelState.Clear();

            return View("Student");
        }

        public ActionResult StudentList()
        {
            var res = dbObj.tbl_Student.ToList();
            return View(res);
        }
        
        
        public ActionResult Delete(int id)
        {
            var res = dbObj.tbl_Student.Where(x => x.ID == id).First();
            dbObj.tbl_Student.Remove(res);
            dbObj.SaveChanges();

            var list = dbObj.tbl_Student.ToList();
            return View("StudentList",list);
        }
    }
}