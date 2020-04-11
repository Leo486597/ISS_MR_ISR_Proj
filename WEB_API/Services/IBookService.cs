using System.Collections.Generic;
using System.Threading.Tasks;
using WEB_API.Models;

namespace BooksApi.Services
{
    public interface IBookService
    {
        Task InsertDemo();
        List<Book> Get();
        Book Get(string id);
        Book Create(Book book);
        void Update(string id, Book bookIn);
        void Remove(Book bookIn);
        void Remove(string id);
    }
}