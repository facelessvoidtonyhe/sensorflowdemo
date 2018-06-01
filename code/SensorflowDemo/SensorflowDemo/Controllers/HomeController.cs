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
        string websokect = ConfigurationManager.AppSettings["WebSocketServer"];
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAssetList()
        {
            string dbpath = HttpContext.Server.MapPath("~/App_Data/DB/demo.db");
            int page = 1; //页码
            int rows = 10; //每页显示行数
            //获取分页信息
            if (Request.QueryString["offset"] != null)
            {
                page = Convert.ToInt32(Request.QueryString["offset"].ToString());
            }
            if (Request.QueryString["pageSize"] != null)
            {
                rows = Convert.ToInt32(Request.QueryString["pageSize"].ToString());
            }
            if (rows > 0)
            {
                page = page / rows + 1;
            }
            SqliteHelper.BLL.Asset bll = new SqliteHelper.BLL.Asset(dbpath);
            List<SqliteHelper.Model.Asset> list = new List<SqliteHelper.Model.Asset>();
            list = bll.GetModelList("state='0' and cabinetNo='1'");
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

        public ActionResult GetCabinetModel()
        {
            string dbpath = HttpContext.Server.MapPath("~/App_Data/DB/demo.db");
            SqliteHelper.BLL.Cabinet cabintbll = new SqliteHelper.BLL.Cabinet(dbpath);
            var cabinetModel = cabintbll.GetModel(1);
            JsonResult js = new JsonResult();
            js = Json(cabinetModel, JsonRequestBehavior.AllowGet);
            return js;
        }

        /// <summary>
        /// 预占资产
        /// </summary>
        /// <returns></returns>
        public ActionResult PreholdAsset()
        {
            string dbpath = HttpContext.Server.MapPath("~/App_Data/DB/demo.db");
            string orderNo = Request.QueryString["OrderNo"];
            string cabinetNo = Request.QueryString["CabinetNo"];
            string layerCount = Request.QueryString["CabinetLayer"];
            SqliteHelper.BLL.Cabinet cabintbll = new SqliteHelper.BLL.Cabinet(dbpath);
            SqliteHelper.BLL.Asset assetbll = new SqliteHelper.BLL.Asset(dbpath);
            //获取待预占的资产
            var assetModel = assetbll.GetModel(int.Parse(orderNo));
            var assetListOfCabinet = assetbll.GetModelList(string.Format("CabinetNo='{0}' and State in ('1','2')", cabinetNo));
            assetModel.State = "1";
            assetModel.StartLayer = int.Parse(layerCount);
            assetbll.Update(assetModel);
            //预占完了之后，插入一条changelog
            SqliteHelper.BLL.ChangeLog changelogBll = new SqliteHelper.BLL.ChangeLog(dbpath);
            var model = new SqliteHelper.Model.ChangeLog();
            model.AssetNo = assetModel.OrderNo;
            model.CabinetNo = 1;
            model.CreateTime = DateTime.Now;
            model.OperationDetail = string.Format("{0},{1}预占到第{2}U", assetModel.Type, assetModel.BM, layerCount);
            model.OperationType = "1";
            changelogBll.Add(model);
            return Content("1");
        }

        /// <summary>
        /// 获取可用层位
        /// </summary>
        /// <returns></returns>
        public ActionResult GetUsefulLayer()
        {
            string dbpath = HttpContext.Server.MapPath("~/App_Data/DB/demo.db");
            string cabinetNo = Request.QueryString["CabinetNo"];
            //首先查询对应机柜内的资产情况，已经放了资产的，不能继续存放
            SqliteHelper.BLL.Cabinet cabintbll = new SqliteHelper.BLL.Cabinet(dbpath);
            SqliteHelper.BLL.Asset assetbll = new SqliteHelper.BLL.Asset(dbpath);
            var assetListOfCabinet = assetbll.GetModelList(string.Format("CabinetNo='{0}' and State in ('1','2')", cabinetNo));
            Dictionary<int, bool> cabinetLayerStateDic = new Dictionary<int, bool>();
            //先初始化cabinetLayerStateDic
            for (int i = 0; i < 42; i++)
            {
                cabinetLayerStateDic[i] = false;
            }
            foreach (var asset in assetListOfCabinet)
            {
                var layEnd = (int)asset.StartLayer + (int)asset.UsedLayer - 1;
                for (int i = ((int)asset.StartLayer); i <= layEnd; i++)
                {
                    cabinetLayerStateDic[i - 1] = true;
                }
            }
            List<int> resultLayer = new List<int>();
            foreach (var key in cabinetLayerStateDic.Keys)
            {
                if (!cabinetLayerStateDic[key])
                {
                    resultLayer.Add(key + 1);
                }
            }
            JsonResult js = Json(resultLayer, JsonRequestBehavior.AllowGet);
            return js;
        }

        /// <summary>
        /// 获取在架资产
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAssetOnLine()
        {
            string dbpath = HttpContext.Server.MapPath("~/App_Data/DB/demo.db");
            int page = 1; //页码
            int rows = 10; //每页显示行数
            //获取分页信息
            if (Request.QueryString["offset"] != null)
            {
                page = Convert.ToInt32(Request.QueryString["offset"].ToString());
            }
            if (Request.QueryString["pageSize"] != null)
            {
                rows = Convert.ToInt32(Request.QueryString["pageSize"].ToString());
            }
            if (rows > 0)
            {
                page = page / rows + 1;
            }
            SqliteHelper.BLL.Asset bll = new SqliteHelper.BLL.Asset(dbpath);
            List<SqliteHelper.Model.Asset> list = new List<SqliteHelper.Model.Asset>();
            list = bll.GetModelList("state='2' and cabinetNo='1'");
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

        /// <summary>
        /// 下架资产
        /// </summary>
        /// <returns></returns>
        public ActionResult OffLineAsset()
        {
            string dbpath = HttpContext.Server.MapPath("~/App_Data/DB/demo.db");
            string orderNo = Request.QueryString["OrderNo"];
            SqliteHelper.BLL.Asset assetbll = new SqliteHelper.BLL.Asset(dbpath);
            //获取待预占的资产
            var assetModel = assetbll.GetModel(int.Parse(orderNo));
            assetModel.State = "0";
            assetbll.Update(assetModel);
            //下架成功后，插入一条上架ChangeLog
            SqliteHelper.BLL.ChangeLog changelogBll = new SqliteHelper.BLL.ChangeLog(dbpath);
            var model = new SqliteHelper.Model.ChangeLog();
            model.AssetNo = assetModel.OrderNo;
            model.CabinetNo = 1;
            model.CreateTime = DateTime.Now;
            model.OperationDetail = string.Format("{0},{1}从第{2}U下架", assetModel.Type, assetModel.BM, assetModel.StartLayer);
            model.OperationType = "3";
            changelogBll.Add(model);
            return Content("1");
        }


        /// <summary>
        /// 获取定位资产
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAssetLocation()
        {
            string dbpath = HttpContext.Server.MapPath("~/App_Data/DB/demo.db");
            int page = 1; //页码
            int rows = 10; //每页显示行数
            //获取分页信息
            if (Request.QueryString["offset"] != null)
            {
                page = Convert.ToInt32(Request.QueryString["offset"].ToString());
            }
            if (Request.QueryString["pageSize"] != null)
            {
                rows = Convert.ToInt32(Request.QueryString["pageSize"].ToString());
            }
            if (rows > 0)
            {
                page = page / rows + 1;
            }
            SqliteHelper.BLL.Asset bll = new SqliteHelper.BLL.Asset(dbpath);
            List<SqliteHelper.Model.Asset> list = new List<SqliteHelper.Model.Asset>();
            list = bll.GetModelList("state in ('1','2') and cabinetNo='1'");
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
        /// <summary>
        /// 获取机柜的容量信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCapacityInfo()
        {
            CapacityInfo capacityInfo = new CapacityInfo();
            JsonResult js = new JsonResult();
            string dbpath = HttpContext.Server.MapPath("~/App_Data/DB/demo.db");
            SqliteHelper.BLL.Cabinet cabintbll = new SqliteHelper.BLL.Cabinet(dbpath);
            var cabinetModel = cabintbll.GetModel(1);
            SqliteHelper.BLL.Asset assetbll = new SqliteHelper.BLL.Asset(dbpath);
            var assetList = assetbll.GetModelList(string.Format("cabinetNo=1 and state in ('1','2')"));
            //层位
            var usedLayer = assetList.Sum(m => m.UsedLayer);
            double doubleRate = 100 * usedLayer.Value / cabinetModel.Space.Value;
            capacityInfo.progress1Content = string.Format("{0}% ({1}/{2})", Math.Round(doubleRate, 2), usedLayer, cabinetModel.Space);
            capacityInfo.progress1 = string.Format("{0}%", Math.Round(doubleRate, 2));
            if (doubleRate < 30)
            {
                capacityInfo.ColorStyle1 = "progress-bar progress-bar-success progress-bar-striped";
            }
            else if (doubleRate < 60)
            {
                capacityInfo.ColorStyle1 = "progress-bar progress-bar-info progress-bar-striped";
            }
            else if (doubleRate < 90)
            {
                capacityInfo.ColorStyle1 = "progress-bar progress-bar-warning progress-bar-striped";
            }
            else
            {
                capacityInfo.ColorStyle1 = "progress-bar progress-bar-danger progress-bar-striped";
            }
            //电力
            var usedPower = assetList.Sum(m => m.UsedPower);
            doubleRate = Convert.ToDouble(100 * usedPower.Value / cabinetModel.Power.Value);
            capacityInfo.progress2Content = string.Format("{0}% ({1}/{2})", Math.Round(doubleRate, 2), usedPower, cabinetModel.Power);
            capacityInfo.progress2 = string.Format("{0}%", Math.Round(doubleRate, 2));
            if (doubleRate < 30)
            {
                capacityInfo.ColorStyle2 = "progress-bar progress-bar-success progress-bar-striped";
            }
            else if (doubleRate < 60)
            {
                capacityInfo.ColorStyle2 = "progress-bar progress-bar-info progress-bar-striped";
            }
            else if (doubleRate < 90)
            {
                capacityInfo.ColorStyle2 = "progress-bar progress-bar-warning progress-bar-striped";
            }
            else
            {
                capacityInfo.ColorStyle2 = "progress-bar progress-bar-danger progress-bar-striped";
            }
            //承重
            var usedWeight = assetList.Sum(m => m.UsedWeight);
            doubleRate = Convert.ToDouble(100 * usedWeight.Value / cabinetModel.Weight.Value);
            capacityInfo.progress3Content = string.Format("{0}% ({1}/{2})", Math.Round(doubleRate, 2), usedWeight, cabinetModel.Weight);
            capacityInfo.progress3 = string.Format("{0}%", Math.Round(doubleRate, 2));
            if (doubleRate < 30)
            {
                capacityInfo.ColorStyle3 = "progress-bar progress-bar-success progress-bar-striped";
            }
            else if (doubleRate < 60)
            {
                capacityInfo.ColorStyle3 = "progress-bar progress-bar-info progress-bar-striped";
            }
            else if (doubleRate < 90)
            {
                capacityInfo.ColorStyle3 = "progress-bar progress-bar-warning progress-bar-striped";
            }
            else
            {
                capacityInfo.ColorStyle3 = "progress-bar progress-bar-danger progress-bar-striped";
            }
            //电口
            var usedPowerPort = assetList.Sum(m => m.UsedPowerPort);
            doubleRate = Convert.ToDouble(100 * usedPowerPort.Value / cabinetModel.PowerPort.Value);
            capacityInfo.progress4Content = string.Format("{0}% ({1}/{2})", Math.Round(doubleRate, 2), usedPowerPort, cabinetModel.PowerPort);
            capacityInfo.progress4 = string.Format("{0}%", Math.Round(doubleRate, 2));
            if (doubleRate < 30)
            {
                capacityInfo.ColorStyle4 = "progress-bar progress-bar-success progress-bar-striped";
            }
            else if (doubleRate < 60)
            {
                capacityInfo.ColorStyle4 = "progress-bar progress-bar-info progress-bar-striped";
            }
            else if (doubleRate < 90)
            {
                capacityInfo.ColorStyle4 = "progress-bar progress-bar-warning progress-bar-striped";
            }
            else
            {
                capacityInfo.ColorStyle4 = "progress-bar progress-bar-danger progress-bar-striped";
            }
            //网口
            var usedNetPort = assetList.Sum(m => m.UsedNetPort);
            doubleRate = Convert.ToDouble(100 * usedNetPort.Value / cabinetModel.NetPort.Value);
            capacityInfo.progress5Content = string.Format("{0}% ({1}/{2})", Math.Round(doubleRate, 2), usedNetPort, cabinetModel.NetPort);
            capacityInfo.progress5 = string.Format("{0}%", Math.Round(doubleRate, 2));
            if (doubleRate < 30)
            {
                capacityInfo.ColorStyle5 = "progress-bar progress-bar-success progress-bar-striped";
            }
            else if (doubleRate < 60)
            {
                capacityInfo.ColorStyle5 = "progress-bar progress-bar-info progress-bar-striped";
            }
            else if (doubleRate < 90)
            {
                capacityInfo.ColorStyle5 = "progress-bar progress-bar-warning progress-bar-striped";
            }
            else
            {
                capacityInfo.ColorStyle5 = "progress-bar progress-bar-danger progress-bar-striped";
            }
            js = Json(capacityInfo, JsonRequestBehavior.AllowGet);
            return js;
        }

        /// <summary>
        /// 获取机柜内的资产列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAssetInfo()
        {
            string dbpath = HttpContext.Server.MapPath("~/App_Data/DB/demo.db");
            string cabinetNo = Request.QueryString["CabinetNo"];
            SqliteHelper.BLL.Asset assetbll = new SqliteHelper.BLL.Asset(dbpath);
            var assetList = assetbll.GetModelList(string.Format("CabinetNo='{0}' and State in ('1','2')", cabinetNo));
            JsonResult js = Json(assetList, JsonRequestBehavior.AllowGet);
            return js;
        }

        private static DateTime lastDatetime = DateTime.MinValue;
        /// <summary>
        /// 获取机柜内的资产列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCabinetAssetInfo()
        {
            //if ((DateTime.Now - lastDatetime).TotalSeconds < 2)
            //{
            //    return Content("-1");
            //}
            //lastDatetime = DateTime.Now;
            string dbpath = HttpContext.Server.MapPath("~/App_Data/DB/demo.db");
            NLog.LogManager.GetCurrentClassLogger().Debug(dbpath);
            string cabinetNo = Request.QueryString["CabinetNo"];
            SqliteHelper.BLL.Asset assetbll = new SqliteHelper.BLL.Asset(dbpath);
            var assetList = assetbll.GetModelList(string.Format("CabinetNo='{0}' and State in ('1','2')", cabinetNo));
            var assetAll = assetbll.GetModelList("1=1");
            SqliteHelper.BLL.RealTimeData realDataBll = new SqliteHelper.BLL.RealTimeData(dbpath);
            var realTimeData = realDataBll.GetModelList(string.Format("cabinetNo='1'"));
            foreach (var asset in assetList)
            {
                //遍历每个资产，判断资产状态
                if (asset.State == "1")
                {
                    var list = realTimeData.Find(m => m.RFID == asset.RfidId && m.LayerIndex == asset.StartLayer);
                    if (list == null)
                    {
                        //如果资产是已预占，且没有检测到信息
                        asset.CabinetState = "预占";
                    }
                    else
                    {

                        //如果资产是已预占，检测到信息
                        asset.CabinetState = "在架";
                        //预占的资产，如果上架了，自动变为已上架
                        asset.State = "2";
                        assetbll.Update(asset);
                        //上架成功后，插入一条上架ChangeLog
                        var dtime = DateTime.Now;
                        SqliteHelper.BLL.ChangeLog changelogBll = new SqliteHelper.BLL.ChangeLog(dbpath);
                        var model = new SqliteHelper.Model.ChangeLog();
                        model.AssetNo = asset.OrderNo;
                        model.CabinetNo = 1;
                        model.CreateTime = dtime;
                        model.OperationDetail = string.Format("{0},{1}上架到第{2}U", asset.Type, asset.BM, asset.StartLayer);
                        model.OperationType = "2";
                        var datalist = changelogBll.GetModelList(string.Format("CreateTime>='{0}' and CreateTime<'{1}' and cabinetNo='1' and AssetNo='{2}' and OperationType='2'", dtime.AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss"),dtime.AddSeconds(1).ToString("yyyy-MM-dd HH:mm:ss"),model.AssetNo));
                        if (datalist.Count == 0)
                        {
                            changelogBll.Add(model);
                        }
                    }
                }
                else if (asset.State == "2")
                {
                    var list = realTimeData.Find(m => m.RFID == asset.RfidId && m.LayerIndex == asset.StartLayer);
                    if (list == null)
                    {
                        //如果资产是已预占，且没有检测到信息
                        asset.CabinetState = "遗失";
                    }
                    else
                    {
                        //如果资产是已预占，检测到信息
                        asset.CabinetState = "在架";
                    }
                }
            }
            foreach (var realItem in realTimeData)
            {
                var asset = assetList.Find(m => m.StartLayer == realItem.LayerIndex);
                //如果当前层位没有资产，
                if (asset == null)
                {
                    var otherAsset = assetAll.Find(m => m.RfidId == realItem.RFID);
                    otherAsset.CabinetState = "非法";
                    otherAsset.StartLayer = realItem.LayerIndex;
                    assetList.Add(otherAsset);
                }
                else
                {
                    if (asset.RfidId != realItem.RFID)
                    {
                        assetList.Remove(asset);//移除机柜内该层位的资产，显示非法的资产优先级高
                        var otherAsset = assetAll.Find(m => m.RfidId == realItem.RFID);
                        otherAsset.CabinetState = "非法";
                        otherAsset.StartLayer = realItem.LayerIndex;
                        assetList.Add(otherAsset);
                    }
                }
            }
            //从RealData中获取
            JsonResult js = Json(assetList, JsonRequestBehavior.AllowGet);
            return js;
        }

        /// <summary>
        /// 获取资产的类型
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAssetType()
        {
            string dbpath = HttpContext.Server.MapPath("~/App_Data/DB/demo.db");
            var resultList = new List<ChartItem>();
            SqliteHelper.BLL.Asset assetbll = new SqliteHelper.BLL.Asset(dbpath);
            var modellist = assetbll.GetModelList("CabinetNo='1' and State in ('1','2')");
            var typeList = modellist.Select(m => m.Type).Distinct();
            foreach (var type in typeList)
            {
                var deviceOfType = modellist.FindAll(m => m.Type == type);
                if (deviceOfType != null && deviceOfType.Count > 0)
                {
                    resultList.Add(new ChartItem()
                    {
                        ItemText = type,
                        ItemValue = deviceOfType.Count
                    });
                }
            }
            JsonResult js = new JsonResult() { JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            js.Data = resultList;
            return js;
        }

        /// <summary>
        /// 获取上架下架信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetChangeLog()
        {
            string dbpath = HttpContext.Server.MapPath("~/App_Data/DB/demo.db");
            SqliteHelper.BLL.ChangeLog changLogBll = new SqliteHelper.BLL.ChangeLog(dbpath);
            var logList = changLogBll.GetModelList(string.Format("CabinetNo='1'"));
            JsonResult js = new JsonResult() { JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            js.Data = logList;
            return js;
        }

        /// <summary>
        /// 获取WebSocket服务地址
        /// </summary>
        /// <returns></returns>
        public ActionResult GetServerIpPort()
        {
            return Content(websokect);
        }

        /// <summary>
        /// 获取资产盘点列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAssetInventory()
        {
            string dbpath = HttpContext.Server.MapPath("~/App_Data/DB/demo.db");
            int page = 1; //页码
            int rows = 10; //每页显示行数
            //获取分页信息
            if (Request.QueryString["offset"] != null)
            {
                page = Convert.ToInt32(Request.QueryString["offset"].ToString());
            }
            if (Request.QueryString["pageSize"] != null)
            {
                rows = Convert.ToInt32(Request.QueryString["pageSize"].ToString());
            }
            if (rows > 0)
            {
                page = page / rows + 1;
            }
            SqliteHelper.BLL.Asset bll = new SqliteHelper.BLL.Asset(dbpath);
            List<SqliteHelper.Model.Asset> list = new List<SqliteHelper.Model.Asset>();
            list = bll.GetModelList("state ='2' and cabinetNo='1'");
            SqliteHelper.BLL.RealTimeData realDataBll = new SqliteHelper.BLL.RealTimeData(dbpath);
            var realTimeData = realDataBll.GetModelList(string.Format("cabinetNo='1'"));
            foreach (var asset in list)
            {
                var realList = realTimeData.Find(m => m.RFID == asset.RfidId && m.LayerIndex == asset.StartLayer);
                if (realList != null)
                {
                    asset.CabinetState = "盘点到";
                }
                else
                {
                    asset.CabinetState = "未盘点到";
                }
            }
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

    public class CapacityInfo
    {
        public string progress1 { set; get; }
        public string progress2 { set; get; }
        public string progress3 { set; get; }
        public string progress4 { set; get; }
        public string progress5 { set; get; }
        public string progress1Content { set; get; }
        public string progress2Content { set; get; }
        public string progress3Content { set; get; }
        public string progress4Content { set; get; }
        public string progress5Content { set; get; }

        public string ColorStyle1 { set; get; }
        public string ColorStyle2 { set; get; }
        public string ColorStyle3 { set; get; }
        public string ColorStyle4 { set; get; }
        public string ColorStyle5 { set; get; }
    }

    public class ChartItem
    {
        public string ItemText { set; get; }
        public int ItemValue { set; get; }
    }
}