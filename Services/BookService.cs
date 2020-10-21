using System.Collections.Generic;
using BooksApi.Models_;
using MongoDB.Driver;

namespace BooksApi.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;
    
        // contructor de la clase, se usa us para inicializar parametros 
        public BookService(IBookstoreDatabaseSettings settings){

        var client= new MongoClient(settings.ConnectionString);

        var database = client.GetDatabase(settings.DatabaseName);

        _books = database.GetCollection<Book>(settings.BooksCollectionName);
        }

        //Tipo < lo que retorna> + nombre + (paramtetro){}
        public List<Book> GetBook() =>
            _books.Find(book => true).ToList();

     
        public Book GetBookById(string id) =>
            _books.Find<Book>(m => m.Id == id).FirstOrDefault();

        
        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        public void Update(string id, Book bookIn) =>
            _books.ReplaceOne(book => book.Id == id, bookIn);

        public void Remove(Book bookIn) =>
            _books.DeleteOne(book => book.Id == bookIn.Id);

        public void Remove(string id) => 
            _books.DeleteOne(book => book.Id == id);


    }
}