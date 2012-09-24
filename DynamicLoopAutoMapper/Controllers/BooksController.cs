using System.Web.Mvc;
using AutoMapper;
using DynamicLoopAutoMapper.Data.Entities;
using DynamicLoopAutoMapper.Data.Repositories;
using DynamicLoopAutoMapper.Models;

namespace DynamicLoopAutoMapper.Controllers
{
    public class BooksController : Controller
    {
        public ActionResult Add()
        {
            var model = Mapper.Map<Book, BookModel>(new Book());
            model.IsEditMode = false;
            return View("Edit", model);
        }

        public ActionResult Edit(int id)
        {
            var book = new BookRepository().GetById(id);
            if (book == null)
                return RedirectToAction("Index", "Home");

            var model = Mapper.Map<Book, BookModel>(book);
            model.IsEditMode = true;
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(BookModel model)
        {
            if (ModelState.IsValid)
            {
                var book = Mapper.Map<BookModel, Book>(model);
                new BookRepository().Insert(book);
                return RedirectToAction("Index", "Home", new { message = (int)BooksListSuccessMessage.BookAddedSuccesfully });
            }
            return Add();
        }

        [HttpPost]
        public ActionResult Edit(BookModel model)
        {
            if (ModelState.IsValid)
            {
                var book = Mapper.Map<BookModel, Book>(model);
                new BookRepository().Save(book);
                return RedirectToAction("Index", "Home", new { message = (int)BooksListSuccessMessage.BookEditedSuccesfully });
            }
            return Edit(model.Id);
        }

        public ActionResult Delete(int id)
        {
            new BookRepository().Delete(id);
            return RedirectToAction("Index", "Home", new { message = (int)BooksListSuccessMessage.BookDeletedSuccesfully });
        }
    }
}
