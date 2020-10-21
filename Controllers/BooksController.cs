using System.Collections.Generic;
using BooksApi.Models_;
using BooksApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi.Controllers
{

    [Route("api/[controller]")] //anotaciones -ejecutar condigo interno que reduce instrucciones y hace varias operaciones
    [ApiController]
    public class BooksController: ControllerBase
    {
        private readonly BookService _bookService;
        //el contructor, metodo interno , que se carga primero, cuando se llama a la clase, o cuando se instancia la clase

        public BooksController(BookService bookService)
        {
            _bookService = bookService;

        }

        //los metodos CRUD. 
        //cnsultar lista o un colleccion determinado
        [HttpGet]
        public ActionResult<List<Book>> Get()=> _bookService.GetBook();

    
        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ActionResult<Book> GetBookId(string Id)
        {
            var book= _bookService.GetBookById(Id);

            if(book==null)
               return NotFound();
            
            return book;
            
        }

        [HttpPost]
        public ActionResult<Book> Create(Book book){

            _bookService.Create(book);
                    return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);


        }

        [HttpPut("{id:length(24)}")]
        public IActionResult update(string id, Book bookIn)
        {

            var book = _bookService.GetBookById(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookService.Update(id, bookIn);

            return NoContent();


        }
        
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var book = _bookService.GetBookById(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookService.Remove(book.Id);

            return NoContent();
        }


    }
}