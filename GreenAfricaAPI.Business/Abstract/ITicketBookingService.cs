﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using GreenAfrica.DataAccess.Models;

namespace GreenAfricaAPI.Business.Abstract
{
    public interface ITicketBookingService
    {
        Task AddAsync(TicketBooking ticketBooking);

        Task<TicketBooking> GetTicketBookingAsyncById(int ticketBookingId);

        Task<IEnumerable<TicketBooking>> GetTicketBookingsAsync(Func<TicketBooking, bool> predicate);

        Task<bool> SaveChangesAsync();
    }
}