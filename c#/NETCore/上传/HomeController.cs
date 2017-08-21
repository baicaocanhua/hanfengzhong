using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp01.Extend;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace WebApp01.Controllers
{

    public class HomeController : Controller
    {
        readonly string SessionKey = "_MySession";
        readonly MoOptions _options;
        readonly ILogger<HomeController> _logger;
        readonly IMemoryCache _cache;

        public HomeController(IOptions<MoOptions> options, ILogger<HomeController> logger, IMemoryCache cache)
        {
            this._options = options.Value;
            _logger = logger;
            _cache = cache;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult FileUp()
        {

            return View();
        }

        /// <summary>
        /// form提交上传
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> FileUp(MoUser user)
        {
            if (user.MyPhoto == null || user.MyPhoto.Count <= 0) { MsgBox("请上传图片。"); return View(); }
            //var file = Request.Form.Files;
            foreach (var file in user.MyPhoto)
            {
                var fileName = file.FileName;
                var contentType = file.ContentType;
                var len = file.Length;

                var fileType = new string[] { "image/jpeg", "image/png" };
                if (!fileType.Any(b => b.Contains(contentType))) { MsgBox($"只能上传{string.Join(",", fileType)}格式的图片。"); return View(); }

                if (len > 1024 * 1024 * 4) { MsgBox("上传图片大小只能在4M以下。"); return View(); }

                var path = Path.Combine(@"D:\F\学习\vs2017\netcore\netcore01\WebApp01\wwwroot\myfile", fileName);
                using (var stream = System.IO.File.Create(path))
                {
                    await file.CopyToAsync(stream);
                }
            }
            MsgBox($"上传成功");

            return View();
        }

        /// <summary>
        /// ajax无上传进度效果上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> AjaxFileUp()
        {
            var data = new MoData { Msg = "上传失败" };
            try
            {
                var files = Request.Form.Files.Where(b => b.Name == "MyPhoto01");
                //非空限制
                if (files == null || files.Count() <= 0) { data.Msg = "请选择上传的文件。"; return Json(data); }

                //格式限制
                var allowType = new string[] { "image/jpeg", "image/png" };
                if (files.Any(b => !allowType.Contains(b.ContentType)))
                {
                    data.Msg = $"只能上传{string.Join(",", allowType)}格式的文件。";
                    return Json(data);
                }

                //大小限制
                if (files.Sum(b => b.Length) >= 1024 * 1024 * 4)
                {
                    data.Msg = "上传文件的总大小只能在4M以下。"; return Json(data);
                }

                //写入服务器磁盘
                foreach (var file in files)
                {

                    var fileName = file.FileName;
                    var path = Path.Combine(@"D:\F\学习\vs2017\netcore\netcore01\WebApp01\wwwroot\myfile", fileName);
                    using (var stream = System.IO.File.Create(path))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                data.Msg = "上传成功";
                data.Status = 2;

            }
            catch (Exception ex)
            {
                data.Msg = ex.Message;
            }
            return Json(data);
        }

        private string cacheKey = "UserId_UpFile";
        private string cacheKey03 = "UserId_UpFile03";
        /// <summary>
        /// ajax上传进度效果上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> AjaxFileUp02()
        {
            var data = new MoData { Msg = "上传失败" };
            try
            {
                var files = Request.Form.Files.Where(b => b.Name == "MyPhoto02");
                //非空限制
                if (files == null || files.Count() <= 0) { data.Msg = "请选择上传的文件。"; return Json(data); }

                //格式限制
                var allowType = new string[] { "image/jpeg", "image/png" };
                if (files.Any(b => !allowType.Contains(b.ContentType)))
                {
                    data.Msg = $"只能上传{string.Join(",", allowType)}格式的文件。";
                    return Json(data);
                }

                //大小限制
                if (files.Sum(b => b.Length) >= 1024 * 1024 * 4)
                {
                    data.Msg = "上传文件的总大小只能在4M以下。"; return Json(data);
                }

                //初始化上传多个文件的Bar，存储到缓存中，方便获取上传进度
                var listBar = new List<MoBar>();
                files.ToList().ForEach(b =>
                {
                    listBar.Add(new MoBar
                    {
                        FileName = b.FileName,
                        Status = 1,
                        CurrBar = 0,
                        TotalBar = b.Length
                    });
                });
                _cache.Set<List<MoBar>>(cacheKey, listBar);

                //写入服务器磁盘
                foreach (var file in files)
                {
                    //总大小
                    var totalSize = file.Length;
                    //初始化每次读取大小
                    var readSize = 1024L;
                    var bt = new byte[totalSize > readSize ? readSize : totalSize];
                    //当前已经读取的大小
                    var currentSize = 0L;

                    var fileName = file.FileName;
                    var path = Path.Combine(@"D:\F\学习\vs2017\netcore\netcore01\WebApp01\wwwroot\myfile", fileName);
                    using (var stream = System.IO.File.Create(path))
                    {
                        //await file.CopyToAsync(stream);
                        //进度条处理流程
                        using (var inputStream = file.OpenReadStream())
                        {
                            //读取上传文件流
                            while (await inputStream.ReadAsync(bt, 0, bt.Length) > 0)
                            {

                                //当前读取的长度
                                currentSize += bt.Length;

                                //写入上传流到服务器文件中
                                await stream.WriteAsync(bt, 0, bt.Length);

                                //获取每次读取的大小
                                readSize = currentSize + readSize <= totalSize ?
                                        readSize :
                                        totalSize - currentSize;
                                //重新设置
                                bt = new byte[readSize];

                                //设置当前上传的文件进度，并重新缓存到进度缓存中
                                var bars = _cache.Get<List<MoBar>>(cacheKey);
                                var currBar = bars.Where(b => b.FileName == fileName).SingleOrDefault();
                                currBar.CurrBar = currentSize;
                                currBar.Status = currentSize >= totalSize ? 2 : 1;
                                _cache.Set<List<MoBar>>(cacheKey, bars);

                                System.Threading.Thread.Sleep(1000 * 1);
                            }
                        }
                    }
                }
                data.Msg = "上传完成";
                data.Status = 2;
            }
            catch (Exception ex)
            {
                data.Msg = ex.Message;
            }
            return Json(data);
        }

        [HttpPost]
        public JsonResult ProgresBar02()
        {
            var bars = new List<MoBar>();
            try
            {
                bars = _cache.Get<List<MoBar>>(cacheKey);
            }
            catch (Exception ex)
            {
            }
            return Json(bars);
        }


        /// <summary>
        /// ajax上传进度效果上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AjaxFileUp03()
        {
            var data = new MoData { Msg = "上传失败" };
            try
            {
                var files = Request.Form.Files.Where(b => b.Name == "MyPhoto03");
                //非空限制
                if (files == null || files.Count() <= 0) { data.Msg = "请选择上传的文件。"; return Json(data); }

                //格式限制
                var allowType = new string[] { "image/jpeg", "image/png" };
                if (files.Any(b => !allowType.Contains(b.ContentType)))
                {
                    data.Msg = $"只能上传{string.Join(",", allowType)}格式的文件。";
                    return Json(data);
                }

                //大小限制
                if (files.Sum(b => b.Length) >= 1024 * 1024 * 4)
                {
                    data.Msg = "上传文件的总大小只能在4M以下。"; return Json(data);
                }

                //初始化上传多个文件的Bar，存储到缓存中，方便获取上传进度
                var listBar = new List<MoBar>();
                files.ToList().ForEach(b =>
                {
                    listBar.Add(new MoBar
                    {
                        FileName = b.FileName,
                        Status = 1,
                        CurrBar = 0,
                        TotalBar = b.Length
                    });
                });
                _cache.Set<List<MoBar>>(cacheKey03, listBar);

                var len = files.Count();
                Task[] tasks = new Task[len];
                //写入服务器磁盘
                for (int i = 0; i < len; i++)
                {
                    var file = files.Skip(i).Take(1).SingleOrDefault();
                    tasks[i] = Task.Factory.StartNew((p) =>
                    {
                        var item = p as IFormFile;

                        //总大小
                        var totalSize = item.Length;
                        //初始化每次读取大小
                        var readSize = 1024L;
                        var bt = new byte[totalSize > readSize ? readSize : totalSize];
                        //当前已经读取的大小
                        var currentSize = 0L;

                        var fileName = item.FileName;
                        var path = Path.Combine(@"D:\F\学习\vs2017\netcore\netcore01\WebApp01\wwwroot\myfile", fileName);
                        using (var stream = System.IO.File.Create(path))
                        {
                            //进度条处理流程
                            using (var inputStream = item.OpenReadStream())
                            {
                                //读取上传文件流
                                while (inputStream.Read(bt, 0, bt.Length) > 0)
                                {

                                    //当前读取的长度
                                    currentSize += bt.Length;

                                    //写入上传流到服务器文件中
                                    stream.Write(bt, 0, bt.Length);

                                    //获取每次读取的大小
                                    readSize = currentSize + readSize <= totalSize ?
                                           readSize :
                                           totalSize - currentSize;
                                    //重新设置
                                    bt = new byte[readSize];

                                    //设置当前上传的文件进度，并重新缓存到进度缓存中
                                    var bars = _cache.Get<List<MoBar>>(cacheKey03);
                                    var currBar = bars.Where(b => b.FileName == fileName).SingleOrDefault();
                                    currBar.CurrBar = currentSize;
                                    currBar.Status = currentSize >= totalSize ? 2 : 1;
                                    _cache.Set<List<MoBar>>(cacheKey03, bars);

                                    System.Threading.Thread.Sleep(1000 * 1);
                                }
                            }
                        }

                    }, file);
                }

                //任务等待 ，这里必须等待，不然会丢失上传文件流
                Task.WaitAll(tasks);

                data.Msg = "上传完成";
                data.Status = 2;
            }
            catch (Exception ex)
            {
                data.Msg = ex.Message;
            }
            return Json(data);
        }

        [HttpPost]
        public JsonResult ProgresBar03()
        {
            var bars = new List<MoBar>();
            try
            {
                bars = _cache.Get<List<MoBar>>(cacheKey03);
            }
            catch (Exception ex)
            {
            }
            return Json(bars);
        }

        public void MsgBox(string msg, string key = "MsgBox")
        {
            ViewData[key] = msg;
        }

        public FileResult GetCode()
        {
            var data = HttpContext.Items["codedata"].ToString();
            var code = JsonConvert.DeserializeObject<MoValidateCodeResponse>(data);
            return File(code.CodeStream, "image/jpeg");
        }

        public async Task<FileResult> GetCodeAsync()
        {
            var code = await HttpContext.WenZiCode();

            return File(code.CodeStream, "image/jpeg");
        }

        //public IActionResult About()
        //{
        //    _logger.LogInformation("这里是About");

        //    //var userInfo = "我的NetCore之Session";
        //    //HttpContext.Session.Set(SessionKey, System.Text.Encoding.UTF8.GetBytes(userInfo));

        //    MoUser user = new MoUser();
        //    HttpContext.Session.Set<MoUser>(SessionKey, user);
        //    ViewData["Message"] = $"读取配置文件Option1节点值：{this._options.Option1}，添加session";
        //    return View();
        //}

        public IActionResult Contact()
        {
            //var userInfo = string.Empty;
            //if (HttpContext.Session.TryGetValue(SessionKey, out var bt))
            //{
            //    userInfo = System.Text.Encoding.UTF8.GetString(bt);
            //}

            //ViewData["Message"] = string.IsNullOrWhiteSpace(userInfo) ? "Session获取为空" : userInfo;

            var user = HttpContext.Session.Get<MoUser>(SessionKey);
            ViewData["Message"] = user == null ? "Session获取为空" : $"昵称：{user.UserName}";
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }

    public class MoData
    {
        /// <summary>
        /// 0：失败 1：上传中 2:成功
        /// </summary>
        public int Status { get; set; }

        public string Msg { get; set; }
    }

    public class MoBar : MoData
    {
        /// <summary>
        /// 文件名字
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 当前上传大小
        /// </summary>
        public long CurrBar { get; set; }

        /// <summary>
        /// 总大小
        /// </summary>
        public long TotalBar { get; set; }

        /// <summary>
        /// 进度百分比
        /// </summary>
        public string PercentBar
        {
            get
            {
                return $"{(this.CurrBar * 100 / this.TotalBar)}%";
            }
        }
    }

    public class MoUser
    {
        public int UserId { get; set; } = 1;
        public string UserName { get; set; } = "神牛步行3";

        public List<IFormFile> MyPhoto { get; set; }
    }
}
