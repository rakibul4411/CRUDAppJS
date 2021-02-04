using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CRUDAppAJS
{
    public class EmpBasicInfoController : Controller
    {
        private EmployeeDB db = new EmployeeDB();

        [HttpGet]
        public async Task<ActionResult> GetAllEmployee()
        {
            try
            {
                var data = await db.EmpBasicInfo.ToListAsync();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception EX)
            {
                Dictionary<string, object> content = new Dictionary<string, object>();
                content.Add("ErrorMessage", $"Data Can't be loaded. {EX.Message}");
                return Json(content);
            }
        }
        [HttpPost]
        public async Task<ActionResult> AddEmployee([Bind] EmpBasicInfoVM evm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmpBasicInfo ebi = new EmpBasicInfo();
                    ebi.EmpID = evm.EmpID;
                    ebi.Name = evm.Name;
                    ebi.Email = evm.Email;
                    ebi.Address = evm.Address;
                    ebi.Phone = evm.Phone;
                    db.EmpBasicInfo.Add(ebi);
                    await db.SaveChangesAsync();
                }
                return Json(new { Message = "Create sucessfully" });
            }
            catch (Exception EX)
            {
                Dictionary<string, object> content = new Dictionary<string, object>();
                content.Add("ErrorMessage", $"Data Can't be Added. {EX.Message}");
                return Json(content);
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetEmployeeByID(int id)
        {
            try
            {
                EmpBasicInfoVM evm = await db.EmpBasicInfo.Select(p => new EmpBasicInfoVM
                {
                    EmpID = p.EmpID,
                    Name = p.Name,
                    Email = p.Email,
                    Phone = p.Phone,
                    Address = p.Address
                }).Where(i => i.EmpID == id).FirstOrDefaultAsync();
                return Json(evm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception EX)
            {
                Dictionary<string, object> content = new Dictionary<string, object>();
                content.Add("ErrorMessage", $"Data Can't be loaded. {EX.Message}");
                return Json(content);
            }
        }
        [HttpPost]
        public async Task<ActionResult> EditEmp([Bind] EmpBasicInfoVM evm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmpBasicInfo ebi = new EmpBasicInfo();
                    ebi.EmpID = evm.EmpID;
                    ebi.Name = evm.Name;
                    ebi.Email = evm.Email;
                    ebi.Phone = evm.Phone;
                    ebi.Address = evm.Address;
                    db.Entry(ebi).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
                return Json(new { Message = "Create sucessfully" });
            }
            catch (Exception EX)
            {
                Dictionary<string, object> content = new Dictionary<string, object>();
                content.Add("ErrorMessage", $"Data Can't be Edit. {EX.Message}");
                return Json(content);
            }
        }
        [HttpPost]
        public async Task<ActionResult> DeleteEmp([Bind] EmpBasicInfoVM ebi)
        {
            try
            {
                var emp = await db.EmpBasicInfo.FindAsync(ebi.EmpID);
                db.EmpBasicInfo.Remove(emp);
                await db.SaveChangesAsync();
                return Json(new { message = "Delete successful" });
            }
            catch (Exception EX)
            {
                Dictionary<string, object> content = new Dictionary<string, object>();
                content.Add("ErrorMessage", $"Data Can't be loaded. {EX.Message}");
                return Json(content);
            }
        }
        [HttpPost]
        public async Task<ActionResult> BulkDeleteEmp([Bind] List<EmpBasicInfoVM> ebi)
        {
            try
            {
                foreach (var data in ebi)
                {
                    var emp = await db.EmpBasicInfo.FindAsync(data.EmpID);
                    db.EmpBasicInfo.Remove(emp);
                    await db.SaveChangesAsync();
                }
                return Json(new { message = "Delete successful" });
            }
            catch (Exception EX)
            {
                Dictionary<string, object> content = new Dictionary<string, object>();
                content.Add("ErrorMessage", $"Bulk Delete can't be completed.{EX.Message}");
                return Json(content);
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
