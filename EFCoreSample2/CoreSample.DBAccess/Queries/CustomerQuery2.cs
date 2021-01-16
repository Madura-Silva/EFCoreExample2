using CoreSample.DBAccess.Cust.Commands;
using CoreSample.DBAccess.Cust.Queries;
using CoreSample.DBAccess.Ord.Commands;
using CoreSample.Infrastructure.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreSample.DBAccess.Queries
{
    public class CustomerQuery2 : ICustomerQuery
    {
        private readonly IMediator _mediator;

        public CustomerQuery2(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<bool> DeleteCustomerAsync(int id)
        {
            //Delete Orders
            //var query1 = new DeleteAllOrdersForCustomer(id);
            //var result =  await _mediator.Send(query1);

            //if (result)
            //{
            //    var query2 = new DeleteACustomerCommand(id);
            //    return await _mediator.Send(query2);
            //}
            //return false;

            var query2 = new DeleteACustomerCommand(id);
            return await _mediator.Send(query2);

        }

        public async Task<List<CustomerDTO>> GetAllCustomersAsync()
        {
            var query = new GetAllCustomersQuery();
            return await _mediator.Send(query);
        }

        public async Task<CustomerDTO> GetCustomerAsync(int id)
        {
            var query = new GetCustomerByIdQuery(id);
            return await _mediator.Send(query);
        }

        public async Task<CustomerDTO> InsertCustomerAsync(CustomerDTO customerDTO)
        {
            var query = new CreateACustomerCommand(customerDTO);
            return await _mediator.Send(query);
        }

        public async Task<CustomerDTO> UpdateCustomerAsync(CustomerDTO customerDTO)
        {
            var query = new UpdateACustomerCommand(customerDTO);
            return await _mediator.Send(query);
        }
    }
}
