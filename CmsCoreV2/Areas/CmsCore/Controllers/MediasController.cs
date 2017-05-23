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

namespace CmsCoreV2.Areas.CmsCore.Controllers
{
    [Area("CmsCore")]
    public class MediasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment env;
        protected readonly AppTenant tenant;

        public MediasController(IHostingEnvironment _env, ITenant<AppTenant> tenant,ApplicationDbContext context) 
        {
            _context = context;
            this.env = _env;
            this.tenant = tenant?.Value;
        }

        // GET: CmsCore/Medias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Medias.ToListAsync());
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: CmsCore/Medias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,FileName,Description,Size,FilePath,FileType,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Media media , IFormFile uploadFile)
        {
            if (uploadFile != null  && ".gif,.jpg,.jpeg,.png,.pdf,.doc,.docx".Contains(Path.GetExtension(uploadFile.FileName)) == false)
            {
                ModelState.AddModelError("ImageUpload", "Dosyanýn uzantýsý .doc, .docx, .pdf, .rtf, .jpg, .gif ya da .png olmalýdýr.");
            }
            else if (ModelState.IsValid)
            {
                if (uploadFile != null)
                {                               
                    media.FileName = uploadFile.FileName;
                    media.Size = (uploadFile.Length / 1024) / 1024;
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
                     )
                    {
                        string FilePath = ViewBag.UploadPath + DateTime.Now.Month + "-" + DateTime.Now.Year + "\\";
                        string dosyaismi = Path.GetFileName(uploadFile.FileName);
                        var yuklemeYeri = Path.Combine(FilePath, dosyaismi);
                        media.FilePath = "uploads/media/" + DateTime.Now.Month + "-" + DateTime.Now.Year + "/";
                        try
                        {
                            if (!Directory.Exists(FilePath))
                            {
                                Directory.CreateDirectory(FilePath);//Eðer klasör yoksa oluþtur
                                uploadFile.CopyTo(new FileStream(yuklemeYeri, FileMode.Create));
                            }
                            else
                            {
                                uploadFile.CopyTo(new FileStream(yuklemeYeri, FileMode.Create));
                            }
                       
                        
                            _context.Add(media);
                            await _context.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }
                        catch (Exception exc) { }
                    }
                    else
                    {
                        ModelState.AddModelError("FileName", "Dosya uzantýsý izin verilen uzantýlardan olmalýdýr.");
                    }
                }
                else { ModelState.AddModelError("FileExist", "Lütfen bir dosya seçiniz!"); }
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
        public async Task<IActionResult> Edit(long id, [Bind("Title,FileName,Description,Size,FilePath,FileType,Id,CreateDate,CreatedBy,UpdateDate,UpdatedBy,AppTenantId")] Media media, IFormFile uploadFile)
        {
            if (ModelState.IsValid)
            {
                if (uploadFile != null)
                {

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
                     )
                    {
                        string FilePath = ViewBag.UploadPath + DateTime.Now.Month + DateTime.Now.Year + "\\";
                        string dosyaismi = Path.GetFileName(uploadFile.FileName);
                        var yuklemeYeri = Path.Combine(FilePath, dosyaismi);
                        media.FilePath = "uploads/media/" + DateTime.Now.Month + "-" + DateTime.Now.Year + "/";
                        try
                        {
                            if (!Directory.Exists(FilePath))
                            {
                                Directory.CreateDirectory(FilePath);//Eðer klasör yoksa oluþtur
                                uploadFile.CopyTo(new FileStream(yuklemeYeri, FileMode.Create));
                            }
                            else
                            {
                                uploadFile.CopyTo(new FileStream(yuklemeYeri, FileMode.Create));
                                media.FileName = uploadFile.FileName;
                            }
                            _context.Update(media);
                            await _context.SaveChangesAsync();
                            return RedirectToAction("Index");
                        }
                        catch (Exception ex) { }
                    }
                    else
                    {
                        ModelState.AddModelError("FileName", "Dosya uzantýsý izin verilen uzantýlardan olmalýdýr.");
                    }
                }
                else { ModelState.AddModelError("FileExist", "Lütfen bir dosya seçiniz!"); }
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
