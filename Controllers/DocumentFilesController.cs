using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Reflection.Metadata;

namespace WebApplication1.Controllers
{
    public class DocumentFilesController : Controller
    {
        private readonly WebApplication1Context _context;

        private bool changed = false;

        public DocumentFilesController(WebApplication1Context context)
        {
            _context = context;
        }

        // GET: DocumentFiles
        public async Task<IActionResult> Index(int? id)
        {
            var fromDocument = _context.Documents.FirstOrDefault(d => d.Id == id);
            if (fromDocument == null)
            {
                return NotFound();
            }
            else
            {
                ViewData["DocumentSubject"] = fromDocument.DocSubject;
                ViewData["DocumentTitle"] = fromDocument.DocTitle;
            }
            if (!changed)
            {
                return View(await _context.DocumentFile.Where(e => e.DocumentID == id).ToListAsync());
            }
            else
            {
                return View(_context.DocumentFile.Local.Where(e => e.DocumentID == id).ToList());
            }
        }

        // GET: DocumentFiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentFile = await _context.DocumentFile
                .FirstOrDefaultAsync(m => m.Id == id);
            if (documentFile == null)
            {
                return NotFound();
            }

            return View(documentFile);
        }

        // GET: DocumentFiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DocumentFiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FileUploader,FormFile")] DocumentFile documentFile, int id)
        {
            //System.Diagnostics.Debug.WriteLine(string.Join("; ", ModelState.Values
            //                            .SelectMany(x => x.Errors)
            //                            .Select(x => x.ErrorMessage)));
            if (ModelState.IsValid)
            {
                documentFile.FileName = documentFile.FormFile.FileName;
                documentFile.FileMIMEType = documentFile.FormFile.ContentType;
                using (var memoryStream = new MemoryStream())
                {
                    await documentFile.FormFile.CopyToAsync(memoryStream);
                    documentFile.FileContent = memoryStream.ToArray();
                }
                documentFile.DocumentID = id;
                _context.DocumentFile.Add(documentFile);
                changed = true;
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = id });
            }
            return View(documentFile);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Route("FileHandling")]
        //public void FileHandling(IFormFile file)
        //{

        //}
        // GET: DocumentFiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentFile = await _context.DocumentFile.FindAsync(id);
            if (documentFile == null)
            {
                return NotFound();
            }
            return View(documentFile);
        }

        // POST: DocumentFiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FileUploader,FormFile")] DocumentFile documentFile)
        {
            if (id != documentFile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(documentFile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentFileExists(documentFile.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = id });
            }
            return View(documentFile);
        }

        // GET: DocumentFiles/Delete/5
        public async Task<IActionResult> Delete(int? id, int docID)
        {
            Console.Write("GET Delete: " + docID);
            if (id == null)
            {
                return NotFound();
            }

            var documentFile = await _context.DocumentFile
                .FirstOrDefaultAsync(m => m.Id == id);
            if (documentFile == null)
            {
                return NotFound();
            }

            return View(documentFile);
        }

        // POST: DocumentFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int docID)
        {
            var documentFile = await _context.DocumentFile.FindAsync(id);
            if (documentFile != null)
            {
                _context.DocumentFile.Remove(documentFile);
                changed = true;
            }

            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = docID });
        }
        [HttpGet]
        public async Task<FileResult> DocumentDownload(int id)
        {
            var FileNeedsToDownload = await _context.DocumentFile.FindAsync(id);
            return File(FileNeedsToDownload.FileContent, FileNeedsToDownload.FileMIMEType, FileNeedsToDownload.FileName);
        }
        private bool DocumentFileExists(int id)
        {
            return _context.DocumentFile.Any(e => e.Id == id);
        }
        [ActionName("SaveDocument")]
        private async void SaveDocument()
        {
            changed = false;
            await _context.SaveChangesAsync();
        }
        [ActionName("DiscardDocument")]
        private async void DiscardDocument()
        {
            var list = _context.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged).ToList();
            foreach (var item in list)
            {
                switch(item.State)
                {
                    case EntityState.Modified:
                        item.CurrentValues.SetValues(item.OriginalValues);
                        item.State = EntityState.Unchanged;    
                        break;
                    case EntityState.Deleted:
                        item.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        item.State = EntityState.Detached;
                        break;
                }
            }
            changed = false;
            await _context.SaveChangesAsync();
        }
    }
}