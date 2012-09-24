using System.Collections.Generic;

namespace DynamicLoopAutoMapper.Models
{
    public class BooksListModel
    {
        public List<BookListItemModel> Books { get; set; }
        public string SuccessMessage { get; set; }
    }
}
