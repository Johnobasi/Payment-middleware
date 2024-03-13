using GreenAfrica.DataAccess;
using GreenAfrica.DataAccess.Models;
using GreenAfricaAPI.Business.Abstract;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GreenAfricaAPI.Business
{
    public class BookingService : IBooking
    {
        private readonly GreenAPIDbContext _context;
        private readonly ILogger<BookingService> _logger;

        public BookingService(GreenAPIDbContext context, ILogger<BookingService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async  Task AddBook(Booking newBook)
        {
            try
            {
                if (newBook == null) throw new ArgumentNullException("parameter cannot be null");

                await _context.AddAsync(newBook);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message, ex);
                throw;
            }       
        }

        public async Task Delete(Booking deleteBook)
        {
            try
            {
                if (deleteBook.Id <= 0) throw new ArgumentNullException("Identifier cannot be null");
                var findRecordById = await _context.Books.FindAsync(deleteBook.Id);
                if (findRecordById != null)
                    _context.Books.Remove(findRecordById);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message, ex);
                throw;
            }
        }

        public async Task<IEnumerable<Booking>> GetAll()
        {
            try
            {
                return  await Task.FromResult(_context.Books.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message, ex);
                throw;
            }
        }

        public Booking GetById(int Id)
        {
            try
            {
                if (Id <= 0) throw new ArgumentNullException("parameter cannot be null");
                var getBooking = _context.Books.Find(Id);
                if (getBooking != null)
                    return getBooking;
                return new Booking();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message, ex);
                throw;
            }
        }

        public async Task Update(Booking updateBook)
        {
            try
            {
                if (updateBook.Id <= 0) throw new ArgumentNullException("Identifier cannot be null");
                var findRecordById = await _context.Books.FindAsync(updateBook.Id);
                if (findRecordById != null)
                {
                    //should use mapper here.
                    findRecordById.BookNumbers = updateBook.BookNumbers <= 0
                                                ? findRecordById.BookNumbers
                                                : updateBook.BookNumbers;
                    findRecordById.Description = !string.IsNullOrEmpty(updateBook.Description)
                                                ? updateBook.Description
                                                : findRecordById.Description;
                    findRecordById.Name = !string.IsNullOrEmpty(updateBook.Name)
                                        ? updateBook.Name
                                        : findRecordById.Name;
                    findRecordById.SerialNumber = !string.IsNullOrEmpty(updateBook.SerialNumber)
                                        ? updateBook.SerialNumber
                                        : findRecordById.SerialNumber;

                    _context.Books.Update(findRecordById);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message, ex);
                throw;
            }
        }
    }
}