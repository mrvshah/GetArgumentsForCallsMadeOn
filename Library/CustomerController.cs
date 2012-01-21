namespace Library
{
	public class CustomerController
	{
		public ICustomerService CustomerService { get; set; }

		public IView View { get; set; }

		public CustomerController(ICustomerService customerService)
		{
			CustomerService = customerService;
		}

		public void Refresh(int id)
		{
			var customer = CustomerService.GetCustomer(id);

			View.BindCustomerData(new CustomerDTO { Id = customer.Id });
		}
	}
}
