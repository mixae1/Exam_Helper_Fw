using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exam_Helper.ViewsModel;
using System.Web.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exam_Helper.Controllers
{
    public class QuestionsLibController : Controller
    {
        //private readonly 
        DbContext _context = new DbContext();

        //public QuestionsLibController(DbContext context)
        //{
        //    _context = context;
        //}

        public QuestionsLibController()
        {
        }

        // GET: QuestionsLib
        public async Task<ActionResult> Index(string SearchString)
        {

            //var ques = from que in _context.Question
            //           select que; //await _context.Question.ToListAsync();
            var ques = _context.Question;
            if (!string.IsNullOrEmpty(SearchString))
                return View(await ques.Where(x => x.Title.Contains(SearchString)
                                 || x.Proof.Contains(SearchString)
                                 || x.TagIds.Contains(SearchString)
                                 || x.Definition.Contains(SearchString)).ToListAsync());
            else
                return View(await ques.ToListAsync());
        }

       

        // GET: QuestionsLib/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

           

            var question = await _context.Question
                .FirstOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return HttpNotFound();
            }

            return View(question);
        }

        // GET: QuestionsLib/Create
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var tags = await _context.Tags.Select(x => new TagForQuestionCreatingModel()
            { Id = x.Id, Name = x.Title, IsSelected = false }).ToListAsync();
            
            return View(new ClassForQuestionCreatingModel() { question = new Question(), tags = tags});
        }

        // POST: QuestionsLib/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ClassForQuestionCreatingModel obj)
        {
            if (ModelState.IsValid)
            {
                var StringTags = string.Join(";", obj.tags.Where(x=>x.IsSelected).Select(x=>x.Id));

                obj.question.CreationDate = DateTime.Now;
                obj.question.UpdateDate = DateTime.Now;
                obj.question.Author = "Admin";
                obj.question.TagIds = StringTags;
                _context.Add(obj.question);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(obj.question);
        }

        // GET: QuestionsLib/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var question = await _context.Question.FindAsync(id);
            if (question == null)
            {
                return HttpNotFound();
            }

            return View(question);
        }

        // POST: QuestionsLib/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Question question)
        {
            if (id != question.Id)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var old = await _context.Question.AsNoTracking().FirstAsync(x => x.Id == id);
                    question.CreationDate = old.CreationDate;
                    question.UpdateDate = DateTime.Now;
                    _context.Update(question);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.Id))
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(question);
        }

        // GET: QuestionsLib/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var question = await _context.Question
                .FirstOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return HttpNotFound();
            }

            return View(question);
        }

        // POST: QuestionsLib/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var question = await _context.Question.FindAsync(id);
            _context.Question.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult RedirectToTest(int id) //RedirectToActionResult
        {
            TempData["question_id"] = id;
            return RedirectToAction(nameof(Index), nameof(Tests));
        }

        private bool QuestionExists(int id)
        {
            return _context.Question.Any(e => e.Id == id);
        }
    }
}
