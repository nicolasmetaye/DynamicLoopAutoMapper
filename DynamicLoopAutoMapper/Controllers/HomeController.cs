using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using DynamicLoopAutoMapper.Data.Entities;
using DynamicLoopAutoMapper.Data.Repositories;
using DynamicLoopAutoMapper.Extensions;
using DynamicLoopAutoMapper.Models;

namespace DynamicLoopAutoMapper.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(BooksListSuccessMessage message = BooksListSuccessMessage.None)
        {
            var books = new BookRepository().GetAll();
            var model = Mapper.Map<IEnumerable<Book>, BooksListModel>(books);
            if (message != BooksListSuccessMessage.None)
                model.SuccessMessage = message.GetDescriptionAttributeValue();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(string search)
        {
            var isbnFiltered = ISBNFilter.Filter(search);
            var books = new BookRepository().SearchFor(book => book.ISBN.Contains(isbnFiltered) || book.Title.Contains(search)).ToList();
            var model = Mapper.Map<IEnumerable<Book>, BooksListModel>(books);
            return View(model);
        }
    }
}
