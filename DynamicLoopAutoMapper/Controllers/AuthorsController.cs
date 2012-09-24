using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using DynamicLoopAutoMapper.Data.Entities;
using DynamicLoopAutoMapper.Data.Repositories;
using DynamicLoopAutoMapper.Extensions;
using DynamicLoopAutoMapper.Models;

namespace DynamicLoopAutoMapper.Controllers
{
    public class AuthorsController : Controller
    {
        public ActionResult Add()
        {
            return View("Edit", new AuthorModel
                                    {
                                        IsEditMode = false
                                    });
        }

        public ActionResult Edit(int id)
        {
            var author = new AuthorRepository().GetById(id);
            if (author == null)
                return RedirectToAction("Index");
            
            var model = Mapper.Map<Author, AuthorModel>(author);
            model.IsEditMode = true;
            return View(model);
        }

        public ActionResult Index(AuthorsListSuccessMessage message = AuthorsListSuccessMessage.None)
        {
            var authors = new AuthorRepository().GetAll();

            var model = Mapper.Map<IEnumerable<Author>, AuthorsListModel>(authors);
            if (message != AuthorsListSuccessMessage.None)
                model.SuccessMessage = message.GetDescriptionAttributeValue();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(AuthorModel model)
        {
            if (ModelState.IsValid)
            {
                var author = Mapper.Map<AuthorModel, Author>(model);
                new AuthorRepository().Insert(author);
                return RedirectToAction("Index", new { message = (int)AuthorsListSuccessMessage.AuthorAddedSuccesfully });
            }
            model.IsEditMode = false;
            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(AuthorModel model)
        {
            if (ModelState.IsValid)
            {
                var author = Mapper.Map<AuthorModel, Author>(model);
                new AuthorRepository().Save(author);
                return RedirectToAction("Index", new { message = (int)AuthorsListSuccessMessage.AuthorEditedSuccesfully });
            }
            model.IsEditMode = true;
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            if (new BookRepository().Any(book => book.AuthorId == id))
                return RedirectToAction("Index", new { message = (int)AuthorsListSuccessMessage.AuthorNotDeletedSuccesfully });
            new AuthorRepository().Delete(id);
            return RedirectToAction("Index", new { message = (int)AuthorsListSuccessMessage.AuthorDeletedSuccesfully });
        }
    }
}
