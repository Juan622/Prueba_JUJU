using Business;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using CustomerEntity = DataAccess.Data.Customer;

namespace API.Controllers.Customer
{
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private BaseService<CustomerEntity> CustomerService;
        public CustomerController(BaseService<CustomerEntity> customerService)
        {
            CustomerService = customerService;
        }


        [HttpGet()]
        public IQueryable<CustomerEntity> GetCustomers(CustomerEntity entity)
        {
            return CustomerService.GetAll();
        }


        [HttpPost()]
        public async Task<CustomerEntity> Create([FromBodyAttribute] CustomerEntity entity)
        {
            return await CreateCustomer(entity);
        }


        private async Task<CustomerEntity> CreateCustomer(CustomerEntity entity)
        {   
                   await CustomerService.FindByColumnName("Name", entity.Name);
            return  CustomerService.Create(entity);
        }

        [HttpPut()]
        public CustomerEntity Update(CustomerEntity entity)
        {
            return CustomerService.Update(entity.CustomerId, entity, out bool changed);
        }

        [HttpDelete()]
        public async Task<CustomerEntity> Delete([FromBodyAttribute] CustomerEntity entity)
        {
                    await CustomerService.DeletePostAssociated(0, entity.CustomerId);
            return  CustomerService.Delete(entity);
        }
    }
}
