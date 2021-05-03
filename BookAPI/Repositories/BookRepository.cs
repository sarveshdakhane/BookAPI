using BookAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookAPI.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BooksContext context;

        public BookRepository(BooksContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Books>> Get()
        {
            return await context.Books.ToListAsync();
        }

        public async Task<Books> Get(int id)
        {
            return await context.Books.FindAsync(id);
        }

        public async Task<Books> Create(Books book)
        {
            context.Books.Add(book);
            await context.SaveChangesAsync();

            return book;
        }
        public async Task Update(Books book)
        {
            context.Entry(book).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var bookToDelete = await context.Books.FindAsync(id);
            context.Books.Remove(bookToDelete);
            await context.SaveChangesAsync();
        }
    }
}