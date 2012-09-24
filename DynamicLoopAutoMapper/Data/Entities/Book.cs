using DynamicLoopAutoMapper.Data.Repositories;

namespace DynamicLoopAutoMapper.Data.Entities
{
    public class Book : Entity
    {
        private int _authorId = -1;
        private Author _author = null;

        public string Title { get; set; }
        public string ISBN { get; set; }

        public int AuthorId
        {
            get { return _authorId; }
            set
            {
                _authorId = value;
                _author = null;
            }
        }

        public Author Author
        {
            get
            {
                if (AuthorId > 0)
                {
                    _author = new AuthorRepository().GetById(AuthorId);
                    if (_author == null)
                        _authorId = -1;
                }
                return _author;
            }
        }

    }
}