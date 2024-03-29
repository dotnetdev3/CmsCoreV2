using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CmsCoreV2.Data;
using CmsCoreV2.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using SaasKit.Multitenancy;
using Z.EntityFramework.Plus;
using Microsoft.AspNetCore.Authorization;

namespace CmsCoreV2.Areas.CmsCore.Controllers
{
    [Authorize(Roles = "ADMIN,MEDIA")]
    [Area("CmsCore")]
    public class MediasController : ControllerBase
    {
        private IHostingEnvironment env;

        public MediasController(IHostingEnvironment _env, ITenant<AppTenant> tenant,ApplicationDbContext context) :base(context,tenant)
        {
             this.env = _env;
         
        }

        // GET: CmsCore/Medias
        public async Task<IActionResult> Index()
        {
            return View(await _context.SetFiltered<Media>().Where(x => x.AppTenantId == tenant.AppTenantId).ToListAsync());
        }

        // GET: CmsCore/Medias/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var media = await _context.Medias
                .SingleOrDefaultAsync(m => m.Id == id);
            if (media == null)
            {
                return NotFound();
            }

            return View(media);
        }

        // GET: CmsCore/Medias/Create
        public IActionResult Create(string element = "")
        {
            if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                ViewBag.Element = element;
                return View("ModalCreate");
            }
            return View();
        }

        public JsonResult ModalCreate(string Title, string Description, IFormFile uploadFile)
        {
            //IFormFileCollection uploadedFiles = Request.Form.Files;
            //IFormFile uploadedFile = uploadedFiles[0];
            IFormFile file = Request.Form.Files[0];
            if (ModelState.IsValid)
            {
                if (uploadFile != null)
                {
                    Media media = new Media();
                    media.Title = Title;
                    media.Description = Description;
                    media.FileName = uploadFile.FileName;
                    media.Size = (uploadFile.Length / 1024);
                    media.CreatedBy = User.Identity.Name ?? "username";
                    media.CreateDate = DateTime.Now;
                    media.UpdatedBy = User.Identity.Name ?? "username";
                    media.UpdateDate = DateTime.Now;
                    media.AppTenantId = tenant.AppTenantId;

                    if (Path.GetExtension(uploadFile.FileName) == ".jpg" || Path.GetExtension(uploadFile.FileName) == ".jpeg" || Path.GetExtension(uploadFile.FileName) == ".png")
                    {
                        media.FileType = "Image";
                    }
                    else if (Path.GetExtension(uploadFile.FileName) == ".mp4" || Path.GetExtension(uploadFile.FileName) == ".gif")
                    {
                        media.FileType = "Video";
                    }
                    else
                    {
                        media.FileType = "Document";
                    }

                    if (Path.GetExtension(uploadFile.FileName) == ".doc"
                    || Path.GetExtension(uploadFile.FileName) == ".pdf"
                    || Path.GetExtension(uploadFile.FileName) == ".rtf"
                    || Path.GetExtension(uploadFile.FileName) == ".docx"
                    || Path.GetExtension(uploadFile.FileName) == ".jpg"
                    || Path.GetExtension(uploadFile.FileName) == ".gif"
                    || Path.GetExtension(uploadFile.FileName) == ".png"
                    || Path.GetExtension(uploadFile.FileName) == ".mp4"
                    || Path.GetExtension(uploadFile.FileName) == ".mp4"
                     )
                    {
                        string category = DateTime.Now.Month + "-" + DateTime.Now.Year;
                        string FilePath = UploadPath + tenant.Folder + "\\" + category + "\\";
                        string dosyaismi = Path.GetFileName(uploadFile.FileName);
                        var yuklemeYeri = Path.Combine(FilePath, dosyaismi);
                        media.FileUrl =  "tenant/" + category + "/";
                        try
                        {
                            if (!Directory.Exists(FilePath))
                            {
                                Directory.CreateDirectory(FilePath);//E�er klas�r yoksa olu�tur    
                            }
                            using (var stream = new FileStream(yuklemeYeri, FileMode.Create))
                            {
                                uploadFile.CopyToAsync(stream);
                            }


                            _context.Add(media);
                            _context.SaveChangesAsync();
                            return Json(new { result = AssetsUrl + media.FileUrl + media.FileName });
                        }
                        catch (Exception exc) { ModelState.AddModelError("FileName", "Hata: " + exc.Message); }
                    }
                    else
                    {
                        ModelState.AddModelError("FileName", "Dosya uzant�s� izin verilen uzant�lardan olmal�d�r.");
                    }
                }
                else { ModelState.AddModelError("FileExist", "L�tfen bir dosya se�iniz!"); }
            }
            return Json(new { result = "false" });
        }
        
        public IEnumerable<Media> MediaGallery(string word, int? year, int? month, string category)
        {
            var mediagallery = _context.Medias.Where(w => w.CreateDate.Year == year && w.CreateDate.Month == month && w.FileType == category).ToList();

            if (!string.IsNullOrEmpty(word))
            {
                mediagallery = mediagallery.Where(w => w.Title.Contains(word) || w.Description.Contains(word) || w.FileName.Contains(word)).ToList();
            }
            return mediagallery;
        }
    

    public JsonResult ModalGallery(string word, int year, int month, string category)
        {
            var mediagallery= MediaGallery(word, year, month, category);
            return Json(new { result = mediagallery });
        }



        // POST: CmsCore/Medias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,FileName,Description,Size,FilePath,FileType,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Media media , IFormFile uploadFile)
        {
            if (uploadFile != null  && ".mp4,.gif,.jpg,.jpeg,.png,.pdf,.doc,.docx".Contains(Path.GetExtension(uploadFile.FileName)) == false)
            {
                ModelState.AddModelError("ImageUpload", "Dosyan�n uzant�s� .doc, .docx, .pdf, .rtf, .jpg, .gif, .mp4 ya da .png olmal�d�r.");
            }
            else if (ModelState.IsValid)
            {
                if (uploadFile != null)
                {                               
                    
                    media.FileName = uploadFile.FileName;
                    media.Size = (uploadFile.Length / 1024);
                    media.CreatedBy = User.Identity.Name ?? "username";
                    media.CreateDate = DateTime.Now;
                    media.UpdatedBy = User.Identity.Name ?? "username";
                    media.UpdateDate = DateTime.Now;
                    media.AppTenantId = tenant.AppTenantId;

                    if (Path.GetExtension(uploadFile.FileName) == ".jpg" || Path.GetExtension(uploadFile.FileName) == ".jpeg" || Path.GetExtension(uploadFile.FileName) == ".png")
                    {
                        media.FileType = "Image";
                    }
                    else if (Path.GetExtension(uploadFile.FileName) == ".mp4" || Path.GetExtension(uploadFile.FileName) == ".gif")
                    {
                        media.FileType = "Video";
                    }
                    else
                    {
                        media.FileType = "Document";
                    }

                    if (Path.GetExtension(uploadFile.FileName) == ".doc"
                    || Path.GetExtension(uploadFile.FileName) == ".pdf"
                    || Path.GetExtension(uploadFile.FileName) == ".rtf"
                    || Path.GetExtension(uploadFile.FileName) == ".docx"
                    || Path.GetExtension(uploadFile.FileName) == ".jpg"
                    || Path.GetExtension(uploadFile.FileName) == ".gif"
                    || Path.GetExtension(uploadFile.FileName) == ".png"
                    || Path.GetExtension(uploadFile.FileName) == ".mp4"
                    || Path.GetExtension(uploadFile.FileName) == ".mp4"
                     )
                    {
                        string category = DateTime.Now.Month + "-" + DateTime.Now.Year;
                        string FilePath = UploadPath + tenant.Folder + "\\" + category + "\\";
                        string dosyaismi = Path.GetFileName(uploadFile.FileName);
                        var yuklemeYeri = Path.Combine(FilePath, dosyaismi);
                        media.FileUrl = "tenant/" + category + "/";

                        try
                        {
                            if (!Directory.Exists(FilePath))
                            {
                                Directory.CreateDirectory(FilePath);//E�er klas�r yoksa olu�tur    
                            }
                            using (var stream = new FileStream(yuklemeYeri, FileMode.Create))
                            {
                                await uploadFile.CopyToAsync(stream);
                            }


                            _context.Add(media);
                            await _context.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }
                        catch (Exception exc) { ModelState.AddModelError("FileName", "Hata: " + exc.Message); }
                    }
                    else
                    {
                        ModelState.AddModelError("FileName", "Dosya uzant�s� izin verilen uzant�lardan olmal�d�r.");
                    }
                }
                else { ModelState.AddModelError("FileExist", "L�tfen bir dosya se�iniz!"); }
            }
            return View(media);
        }
     
        // GET: CmsCore/Medias/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var media = await _context.Medias.SingleOrDefaultAsync(m => m.Id == id);
            if (media == null)
            {
                return NotFound();
            }
            return View(media);
        }

        // POST: CmsCore/Medias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Title,FileName,Description,Size,FilePath,FileUrl,FileType,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Media media, IFormFile uploadFile)
        {
            if (ModelState.IsValid)
            {
                if (uploadFile != null)
                {
                    media.Id = id;
                    media.UpdatedBy = User.Identity.Name ?? "username";
                    media.UpdateDate = DateTime.Now;
                    media.AppTenantId = tenant.AppTenantId;
                    if (Path.GetExtension(uploadFile.FileName) == ".jpg" || Path.GetExtension(uploadFile.FileName) == ".jpeg" || Path.GetExtension(uploadFile.FileName) == ".png")
                    {
                        media.FileType = "Image";
                    }
                    else if (Path.GetExtension(uploadFile.FileName) == ".mp4" || Path.GetExtension(uploadFile.FileName) == ".gif")
                    {
                        media.FileType = "Video";
                    }
                    else
                    {
                        media.FileType = "Document";
                    }
                    if (Path.GetExtension(uploadFile.FileName) == ".doc"
                    || Path.GetExtension(uploadFile.FileName) == ".pdf"
                    || Path.GetExtension(uploadFile.FileName) == ".rtf"
                    || Path.GetExtension(uploadFile.FileName) == ".docx"
                    || Path.GetExtension(uploadFile.FileName) == ".jpg"
                    || Path.GetExtension(uploadFile.FileName) == ".gif"
                    || Path.GetExtension(uploadFile.FileName) == ".png"
                    || Path.GetExtension(uploadFile.FileName) == ".jpeg"
                    || Path.GetExtension(uploadFile.FileName) == ".mp4"
                     )
                    {
                        string category = DateTime.Now.Month + "-" + DateTime.Now.Year;
                        string FilePath = UploadPath + tenant.Folder + "\\" + category + "\\";
                        string dosyaismi = Path.GetFileName(uploadFile.FileName);
                        var yuklemeYeri = Path.Combine(FilePath, dosyaismi);
                        media.FileUrl = "tenant/" + category + "/";

                        try
                        {
                            if (!Directory.Exists(FilePath))
                            {
                                Directory.CreateDirectory(FilePath);//E�er klas�r yoksa olu�tur
                                
                            }
                            using (var stream = new FileStream(yuklemeYeri, FileMode.Create))
                            {
                                await uploadFile.CopyToAsync(stream);
                            }
                            media.FileName = uploadFile.FileName;
                            media.Size = (uploadFile.Length / 1024);
                            _context.Update(media);
                            await _context.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }
                        catch (Exception ex) { ModelState.AddModelError("FileName", "Hata: " + ex.Message); }
                    }
                    else
                    {
                        ModelState.AddModelError("FileName", "Dosya uzant�s� izin verilen uzant�lardan olmal�d�r.");
                    }
                }
                else {

                    media.UpdatedBy = User.Identity.Name ?? "username";
                    media.UpdateDate = DateTime.Now;
                    media.AppTenantId = tenant.AppTenantId;
                    

         

                    _context.Update(media);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return View(media);
        }
     

        // GET: CmsCore/Medias/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var media = await _context.Medias
                .SingleOrDefaultAsync(m => m.Id == id);
            if (media == null)
            {
                return NotFound();
            }

            return View(media);
        }

        // POST: CmsCore/Medias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var media = await _context.Medias.SingleOrDefaultAsync(m => m.Id == id);
            _context.Medias.Remove(media);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MediaExists(long id)
        {
            return _context.Medias.Any(e => e.Id == id);
        }
    }
}
