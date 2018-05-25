using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SqliteHelper;
using System.Configuration;

namespace SensorflowDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            string dbpath = HttpContext.Server.MapPath("~/App_Data/DB/demo.db");
            int page = 1; //页码
            int rows = 10; //每页显示行数
            //获取分页信息
            if (Request.Form["page"] != null)
            {
                page = Convert.ToInt32(Request.Form["page"].ToString());
            }
            if (Request.Form["rows"] != null)
            {
                rows = Convert.ToInt32(Request.Form["rows"].ToString());
            }

            SqliteHelper.BLL.Asset bll = new SqliteHelper.BLL.Asset(dbpath);
            List<SqliteHelper.Model.Asset> list = new List<SqliteHelper.Model.Asset>();
            list = bll.GetModelList("1=1");
            List<SqliteHelper.Model.Asset> listPage;
            JsonResult js = new JsonResult();

            if (list == null || list.Count == 0)
            {
                list = new List<SqliteHelper.Model.Asset>();
                js = Json(new { total = list.Count, rows = list }, JsonRequestBehavior.AllowGet);
                return js;
            }

            //分页操作 没有进行排序，使用的默认排序
            listPage = list.Skip((page - 1) * rows).Take(rows).ToList();
            try
            {
                js = Json(new { total = list.Count, rows = listPage }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return js;
        }
    }
}